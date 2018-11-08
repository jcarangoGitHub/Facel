
#region Referencias

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    public class XmlHelper
    {

        #region Metodos publicos estaticos

        #region ExtractText

        public static string ExtractText(string textContent, string tagIni, string tagEnd, bool includeTags = true)
        {

            #region Variables de trabajo

            int posIni = 0;
            int posFin = 0;
            string result = string.Empty;

            #endregion

            posIni = textContent.IndexOf(tagIni, 0);
            if (posIni >= 0)
            {
                posFin = textContent.IndexOf(tagEnd, posIni);

                if (posFin > 0)
                {
                    if (includeTags)
                    {
                        posFin = 1 + textContent.IndexOf(">", posFin);
                    }
                    else
                    {
                        posIni = 1 + textContent.IndexOf(">", posIni);
                    }

                    if ((posIni >= 0) && (posFin > posIni))
                    {
                        result = textContent.Substring(posIni, posFin - posIni);
                    }
                }
            }

            return result;
        }

        #endregion

        #region XmlValidate

        public static string XmlValidate(string xmlContent, ref string docType, ref string docNumber)
        {

            #region Variables de trabajo

            string errorMessage = string.Empty;
            string numberPath = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode nodoRaiz = null;
            XmlNode nodoNro = null;

            #endregion

            try
            {
                xmlDoc.LoadXml(xmlContent);
                nodoRaiz = xmlDoc.FirstChild;
                docType = nodoRaiz.Name;

                #region PathXml basado en el tipo

                switch (docType)
                {
                    case "FACTURA": numberPath = "//FACTURA/ENC/ENC_6"; break;
                    case "NOTAD": numberPath = "//NOTAD/ENC/ENC_9"; break; //Verificar mejor, esto solo es la version preliminar
                    case "NOTAC": numberPath = "//NOTAC/ENC/ENC_8"; break; //Verificar mejor, esto solo es la version preliminar
                }

                #endregion

                if (string.IsNullOrWhiteSpace(numberPath))
                {
                    errorMessage = Constantes.XML_XMLPATH_NRODOC_NOT_FOUND;
                }
                else
                {
                    nodoNro = nodoRaiz.SelectSingleNode(numberPath);
                    if (nodoNro == null)
                    {
                        errorMessage = string.Format(Constantes.XML_NODE_NRODOC_NOT_FOUND, numberPath);
                    }
                    else
                    {
                        docNumber = nodoNro.InnerText;
                    }
                }
            }
            catch (Exception)
            {
                errorMessage = Constantes.XML_NOT_FORMAT;
            }

            return errorMessage;
        }

        #endregion

        #region ToXml

        /// <summary>
        /// Convierte Dictionary<string, string> en cadena XML
        /// </summary>
        /// <param name="root">Nombre del elemento Raiz</param>
        /// <param name="dict">Diccionario a convertir</param>
        /// <returns>Retorna Cadena de texto XML</returns>
        public static string ToXml(string root, Dictionary<string, string> dict)
        {
            StringBuilder sbResult = new StringBuilder();

            foreach (var item in dict.Keys)
            {
                string value = dict[item];

                if (value == null) value = string.Empty;

                sbResult.AppendFormat(Constantes.TEMPLATE_XML_NODE, item, value);
                sbResult.AppendLine();
            }

            return string.Format(Constantes.TEMPLATE_XML_NODE, root, sbResult.ToString());
        }

        #endregion

        #endregion  

    }
}
