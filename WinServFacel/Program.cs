using DBE.Net.Facel.LibFacel.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinServFacel
{
    class Program
    {
        private static string emailDBEError = "julio.arango@gmail.com";
        private static string emailHCEError = "julio.arango@gmail.com";
        private static string emailSubject = "Error WSCarvajal";

        static void Main(string[] args)
        {
            //TestUpload();
            DocumentStatusConsume();
            Console.ReadLine();
        }

        #region DocumentStatus

        private static void DocumentStatusConsume() {
            string nro = "1235552";
            string tid = "3a80f28a369c46cbabe4ab7e315a836d";
            DocumentStatus(tid, "FACTURA", nro);
        }

        static void DocumentStatus(string transactionId, string docType, string docNumber)
        {

            #region Variables de trabajo

            string errorMessage = string.Empty;
            string statusCode = string.Empty;
            string fechaText = string.Empty;
            string fileResp = string.Empty;
            string xmlContent = string.Empty;
            string methodName = "DocumentStatus";
            string pathData = ConfigurationManager.AppSettings["pathData"];
            string url = ConfigurationManager.AppSettings["urlSoap"];
            string user = ConfigurationManager.AppSettings["username"];
            string pwd = ConfigurationManager.AppSettings["password"];
            string company = ConfigurationManager.AppSettings["company"];
            string account = ConfigurationManager.AppSettings["account"];
            string connection = ConfigurationManager.AppSettings["connection"];
            DateTime fechaHoy = DateTime.Now;
            Dictionary<string, string> dictResult = null;

            #endregion

            fechaText = fechaHoy.ToString("yyyyMMdd");
            pathData = Path.Combine(pathData, fechaText);
            errorMessage = FileHelper.CreateDirectory(pathData);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                if (IsConected())
                {
                    EmailHelper.SendEmailDBExperts(emailHCEError, emailSubject, errorMessage);
                }
            }
            else
            {

                #region Llamado al WS

                using (WSClient ws = new WSClient(url, user, pwd, company, account))
                {
                    dictResult = ws.DocumentStatus(transactionId);
                }                

                statusCode = dictResult["statusCode"];
                errorMessage = dictResult["errorMessage"];

                #endregion

                #region Archivo XML de respuesta

                fileResp = Path.Combine(pathData, string.Format("{0}_{1}_{2}.xml", docType, docNumber, methodName));
                xmlContent = XmlHelper.ToXml(methodName, dictResult);

                xmlContent = string.Format(@"<?xml version=""1.0"" encoding=""iso-8859-1""?>{0}{1}", Environment.NewLine, xmlContent);
                FileHelper.PutFileContent(fileResp, xmlContent);

                #endregion

                #region Mensaje de error

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    if (IsConected())
                    {
                        if (statusCode.Equals("901"))
                        {
                            EmailHelper.SendEmailDBExperts(emailDBEError, emailSubject, xmlContent);
                        }
                        else
                        {
                            EmailHelper.SendEmailDBExperts(emailHCEError, emailSubject, xmlContent);
                        }
                    }
                }

                #endregion

                #region Acceso a DB

                string plantilla = "EXEC [wsc].[FilesDetailStatusXml] @companyId='{0}', @accountId='{1}', @xmlContent='{2}';";
                DataSet dsDatos = new DataSet();
                DataTable dtStatus = new DataTable("dtStatus");

                dsDatos.ExtendedProperties["Conexion"] = connection;
                dtStatus.ExtendedProperties["Metodo"] = string.Format(plantilla, company, account, xmlContent);
                dsDatos.Tables.Add(dtStatus);

                dsDatos = DataHelper.GetData(dsDatos);
                errorMessage = dsDatos.ExtendedProperties["Error"].ToString();
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = dtStatus.ExtendedProperties["Error"].ToString();
                }

                #endregion
            }
        }

        #endregion

        #region UploadConsume

        private static void TestUpload()
        {

            #region Variables de trabajo

            string docType = string.Empty;
            string docNro = string.Empty;
            string fileContent = string.Empty;
            string fileName = "1235552_empresa.xml";
            string pathOrigen = Path.Combine(ConfigurationManager.AppSettings["pathData"], "XmlOrigen");
            byte[] fileData = null;

            #endregion

            if (IsConected())
            {
                fileContent = System.IO.File.ReadAllText(Path.Combine(pathOrigen, fileName));
                fileData = System.IO.File.ReadAllBytes(Path.Combine(pathOrigen, fileName));
                UploadConsume(fileName, fileContent, Convert.ToBase64String(fileData));
            }
        }

        static void UploadConsume(string fileName, string contentText, string contentBytes)
        {

            #region Variables de trabajo

            string errorMessage = string.Empty;
            string statusCode = string.Empty;
            string fechaText = string.Empty;
            string methodName = "Upload";
            string fileData = string.Empty;
            string docType = string.Empty;
            string docNumber = string.Empty;
            string fileResp = string.Empty;
            string xmlContent = string.Empty;
            string pathData = ConfigurationManager.AppSettings["pathData"];
            string url = ConfigurationManager.AppSettings["urlSoap"];
            string user = ConfigurationManager.AppSettings["username"];
            string pwd = ConfigurationManager.AppSettings["password"];
            string company = ConfigurationManager.AppSettings["company"];
            string account = ConfigurationManager.AppSettings["account"];
            string connection = ConfigurationManager.AppSettings["connection"];
            DateTime fechaHoy = DateTime.Now;
            Dictionary<string, string> dictResult = new Dictionary<string, string>();

            #endregion

            fechaText = fechaHoy.ToString("yyyyMMdd");
            pathData = Path.Combine(pathData, fechaText);
            errorMessage = FileHelper.CreateDirectory(pathData);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                if (IsConected())
                {
                    EmailHelper.SendEmailDBExperts(emailHCEError, emailSubject, errorMessage);
                }
            }
            else
            {
                errorMessage = XmlHelper.XmlValidate(contentText, ref docType, ref docNumber);

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    if (IsConected())
                    {
                        EmailHelper.SendEmailDBExperts(emailHCEError, emailSubject, errorMessage);
                    }
                }
                else
                {

                    #region Acceso al WS

                    using (WSClient ws = new WSClient(url, user, pwd, company, account))
                    {
                        dictResult = ws.Upload(fileName, contentBytes, docType, docNumber);
                    }

                    statusCode = dictResult["statusCode"];
                    errorMessage = dictResult["errorMessage"];

                    #endregion

                    #region Archivo XML de respuesta

                    fileResp = Path.Combine(pathData, string.Format("{0}_{1}_{2}.xml", docType, docNumber, methodName));
                    xmlContent = XmlHelper.ToXml(methodName, dictResult);

                    xmlContent = string.Format(@"<?xml version=""1.0"" encoding=""iso-8859-1""?>{0}{1}", Environment.NewLine, xmlContent);
                    FileHelper.PutFileContent(fileResp, xmlContent);

                    #endregion

                    #region Mensaje de error

                    if (!string.IsNullOrWhiteSpace(errorMessage))
                    {
                        if (IsConected())
                        {
                            if (statusCode.Equals("901"))
                            {
                                EmailHelper.SendEmailDBExperts(emailDBEError, emailSubject, xmlContent);
                            }
                            else
                            {
                                EmailHelper.SendEmailDBExperts(emailHCEError, emailSubject, xmlContent);
                            }
                        }
                    }

                    #endregion

                    #region Acceso a DB

                    string plantilla = "EXEC [wsc].[FilesDetailUploadXml] @companyId='{0}', @accountId='{1}', @xmlContent='{2}';";
                    DataSet dsDatos = new DataSet();
                    DataTable dtUpload = new DataTable("dtUpload");

                    dsDatos.ExtendedProperties["Conexion"] = connection;
                    dtUpload.ExtendedProperties["Metodo"] = string.Format(plantilla, company, account, xmlContent);
                    dsDatos.Tables.Add(dtUpload);

                    dsDatos = DataHelper.GetData(dsDatos);
                    errorMessage = dsDatos.ExtendedProperties["Error"].ToString();
                    if (string.IsNullOrWhiteSpace(errorMessage))
                    {
                        errorMessage = dtUpload.ExtendedProperties["Error"].ToString();
                    }

                    #endregion

                }

            }
        }

        #endregion


        #region Metodos estaticos privados

        #region IsConected

        private static bool IsConected()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}
