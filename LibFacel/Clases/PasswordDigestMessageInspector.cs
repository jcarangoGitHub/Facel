
#region Referencias

using Microsoft.Web.Services3.Security.Tokens;
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    internal class PasswordDigestMessageInspector : IClientMessageInspector
    {
        #region IClientMessageInspector Members

        internal string Username { get; set; }
        internal string Password { get; set; }

        internal PasswordDigestMessageInspector(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState) { }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            string search = "<wsse:Nonce";
            string newTag = "<wsse:Nonce EncodingType='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary'";

            // Use the WSE 3.0 security token class
            UsernameToken token = new UsernameToken(this.Username, this.Password, PasswordOption.SendPlainText);

            // Serialize the token to XML
            XmlElement securityToken = token.GetXml(new XmlDocument());
            securityToken.InnerXml = securityToken.InnerXml.Replace(search, newTag);

            // Adding UserName token
            MessageHeader securityHeader = MessageHeader.CreateHeader("Security",
                "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, false);

            request.Headers.Add(securityHeader);

            return Convert.DBNull;
        }

        #endregion
    }
}
