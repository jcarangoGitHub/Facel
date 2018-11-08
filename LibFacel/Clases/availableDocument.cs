
#region Referencias

using DBE.Net.Facel.LibFacel.Clases;
using System.Text;

#endregion

namespace DBE.Net.Facel.LibFacel.WSCarvajal
{
    public partial class availableDocument
    {
        public string ToXml()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(Constantes.TEMPLATE_XML_NODE, "documentNumber", this.documentNumber);
            sb.AppendFormat(Constantes.TEMPLATE_XML_NODE, "documentPrefix", this.documentPrefix);
            sb.AppendFormat(Constantes.TEMPLATE_XML_NODE, "documentType", this.documentType);
            sb.AppendFormat(Constantes.TEMPLATE_XML_NODE, "downloadData", this.downloadData);
            sb.AppendFormat(Constantes.TEMPLATE_XML_NODE, "senderIdentification", this.senderIdentification);

            return string.Format(Constantes.TEMPLATE_XML_NODE, "availableDocument", sb.ToString());
        }
    }
}
