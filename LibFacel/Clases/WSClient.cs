
#region Referencias

using DBE.Net.Facel.LibFacel.WSCarvajal;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    public class WSClient : IDisposable
    {

        #region Miembros privados

        private bool m_isDisposed = false;
        private string m_Excepcion_Code = "901";
        private string m_Url = string.Empty;
        private string m_Username = string.Empty;
        private string m_Password = string.Empty;
        private string m_Account = string.Empty;
        private string m_Company = string.Empty;
        private invoiceServiceClient m_Proxy = null;

        #endregion

        #region Constructor

        public WSClient(string url, string username, string password, string company, string account)
        {
            this.m_Url = url;
            this.m_Password = password;
            this.m_Username = username;
            this.m_Account = account;
            this.m_Company = company;
        }

        #endregion

        #region Dispose

        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
            if (!m_isDisposed)
            {
                if (disposing)
                {
                    this.m_Proxy = null;
                }
            }
            m_isDisposed = true;
        }

        #endregion

        #region Metodos publicos

        #region Upload

        public Dictionary<string, string> Upload(string fileName, string fileData, string docType, string docNUmber)
        {

            #region Variables de trabajo

            string step = "ConfigWS";
            string methodName = "Upload";
            string errorMessage = string.Empty;
            UploadResponse wsResp = new UploadResponse();
            UploadRequest wsReq = new UploadRequest();

            Dictionary<string, string> dictResult = this.FaultDictionary();

            #endregion

            try
            {

                #region Diccionario de respuesta

                dictResult.Add("methodName", methodName);
                dictResult.Add("fileName", fileName);
                dictResult.Add("fileData", fileData);
                dictResult.Add("documentType", docType);
                dictResult.Add("documentNumber", docNUmber);
                dictResult.Add("transactionId", "");
                dictResult.Add("status", "");

                #endregion

                #region Inicializacion WS 

                this.Initialize();
                wsReq.fileName = fileName;
                wsReq.fileData = fileData;
                wsReq.companyId = this.m_Company;
                wsReq.accountId = this.m_Account;

                #endregion

                #region Llamado al WS

                try
                {
                    step = "BeforeWS";
                    wsResp = this.m_Proxy.Upload(wsReq);
                    step = "AfterWS";
                    if (wsResp == null)
                    {
                        this.SetException(dictResult, "UploadResponse nulo", "Se realizó correctamente el llamado al metodo Upload y devolvio respuesta null");
                    }
                    else
                    {
                        step = "BeforeDict";
                        dictResult["transactionId"] = wsResp.transactionId;
                        dictResult["status"] = wsResp.status;
                        step = "AfterDict";
                    }
                }
                catch (FaultException<InvoiceServiceFault> faultException)
                {
                    var detail = faultException.Detail;
                    this.SetException(dictResult, detail.errorMessage, detail.statusCode, detail.reasonPhrase);
                }
                catch (ProtocolException protocolExc)
                {
                    this.SetException(dictResult, protocolExc.Message, protocolExc.StackTrace);
                }

                #endregion

            }
            catch (Exception excepcion)
            {
                this.SetException(dictResult, excepcion.Message, excepcion.StackTrace);
            }
            finally
            {
                dictResult["Step"] = step;
            }

            return dictResult;
        }

        #endregion

        #region DocumentStatus

        public Dictionary<string, string> DocumentStatus(string transactionId)
        {

            #region Variables de trabajo

            string step = "ConfigWS";
            string methodName = "DocumentStatus";
            DocumentStatusResponse wsResp = null;
            DocumentStatusRequest wsReq = new DocumentStatusRequest();

            Dictionary<string, string> dictResult = this.FaultDictionary();

            #endregion

            try
            {

                #region Diccionario de respuesta

                dictResult.Add("methodName", methodName);
                dictResult.Add("transactionId", transactionId);
                dictResult.Add("processName", "");
                dictResult.Add("processStatus", "");
                dictResult.Add("processDate", "");
                dictResult.Add("messageType", "");

                #endregion

                #region Inicializacion WS 

                this.Initialize();
                wsReq.transactionId = transactionId;
                wsReq.companyId = this.m_Company;
                wsReq.accountId = this.m_Account;

                #endregion

                #region Llamado al WS

                try
                {
                    step = "BeforeWS";
                    wsResp = this.m_Proxy.DocumentStatus(wsReq);
                    step = "AfterWS";
                    if (wsResp == null)
                    {
                        this.SetException(dictResult, "DocumentStatusResponse nulo", "Se realizó correctamente el llamado al metodo DocumentStatus y devolvio respuesta null");
                    }
                    else
                    {
                        step = "BeforeDict";
                        dictResult["processName"] = wsResp.processName;
                        dictResult["processStatus"] = wsResp.processStatus;
                        dictResult["processDate"] = wsResp.processDate.ToString("yyyy-MM-ddTHH:mm:ss.fff-05:00");
                        dictResult["messageType"] = wsResp.messageType;
                        dictResult["errorMessage"] = wsResp.errorMessage;
                        step = "AfterDict";
                    }
                }
                catch (FaultException<InvoiceServiceFault> faultException)
                {
                    var detail = faultException.Detail;
                    this.SetException(dictResult, detail.errorMessage, detail.statusCode, detail.reasonPhrase);
                }
                catch (ProtocolException protocolExc)
                {
                    this.SetException(dictResult, protocolExc.Message, protocolExc.StackTrace);
                }

                #endregion

            }
            catch (Exception excepcion)
            {
                this.SetException(dictResult, excepcion.Message, excepcion.StackTrace);
            }
            finally
            {
                dictResult["Step"] = step;
            }

            return dictResult;
        }

        #endregion

        #region Download

        public Dictionary<string, string> Download(string documentType, string documentNumber, string documentPrefix, string resourceType)
        {

            #region Variables de trabajo

            string step = "ConfigWS";
            string methodName = "Download";
            DownloadResponse wsResp = null;
            DownloadRequest wsReq = new DownloadRequest();

            Dictionary<string, string> dictResult = this.FaultDictionary();

            #endregion

            try
            {

                #region Diccionario de respuesta

                dictResult.Add("methodName", methodName);
                dictResult.Add("downloadData", "");
                dictResult.Add("status", "");

                #endregion

                #region Inicializacion WS 

                this.Initialize();
                wsReq.documentType = documentType;
                wsReq.documentNumber = documentNumber;
                wsReq.documentPrefix = documentPrefix;
                wsReq.resourceType = resourceType;
                wsReq.companyId = this.m_Company;
                wsReq.accountId = this.m_Account;

                #endregion

                #region Llamado al WS

                try
                {
                    step = "BeforeWS";
                    wsResp = this.m_Proxy.Download(wsReq);
                    step = "AfterWS";
                    if (wsResp == null)
                    {
                        this.SetException(dictResult, "DownloadResponse nulo", "Se realizó correctamente el llamado al metodo Download y devolvio respuesta null");
                    }
                    else
                    {
                        step = "BeforeDict";
                        dictResult["downloadData"] = wsResp.downloadData;
                        dictResult["status"] = wsResp.status;
                        step = "AfterDict";
                    }
                }
                catch (FaultException<InvoiceServiceFault> faultException)
                {
                    var detail = faultException.Detail;
                    this.SetException(dictResult, detail.errorMessage, detail.statusCode, detail.reasonPhrase);
                }
                catch (ProtocolException protocolExc)
                {
                    this.SetException(dictResult, protocolExc.Message, protocolExc.StackTrace);
                }

                #endregion

            }
            catch (Exception excepcion)
            {
                string tipo = excepcion.GetType().ToString();
                this.SetException(dictResult, excepcion.Message, excepcion.StackTrace);
            }
            finally
            {
                dictResult["Step"] = step;
            }

            return dictResult;
        }

        #endregion

        #region CheckAvailableDocuments

        public Dictionary<string, string> CheckAvailableDocuments(DateTime initialDate, DateTime finalDate, string resourceType)
        {

            #region Variables de trabajo

            string step = "ConfigWS";
            string methodName = "CheckAvailableDocuments";
            CheckAvailableDocumentsRequest wsReq = new CheckAvailableDocumentsRequest();
            availableDocument[] docs = null;
            Dictionary<string, string> dictResult = this.FaultDictionary();

            #endregion

            try
            {

                #region Diccionario de respuesta

                dictResult.Add("methodName", methodName);
                dictResult.Add("AvailableDocument", "");

                #endregion

                #region Inicializacion WS 

                this.Initialize();
                wsReq.initialDate = initialDate;
                wsReq.finalDate = finalDate;
                wsReq.resourceType = resourceType;
                wsReq.companyId = this.m_Company;

                #endregion

                #region Llamado al WS

                try
                {
                    step = "BeforeWS";
                    docs = this.m_Proxy.CheckAvailableDocuments(wsReq);
                    step = "AfterWS";
                    if (docs == null)
                    {
                        this.SetException(dictResult, "CheckAvailableDocuments nulo", "Se realizó correctamente el llamado al metodo CheckAvailableDocuments y devolvio respuesta null");
                    }
                    else
                    {
                        step = "BeforeDict";

                        if ((docs != null) && (docs.Length > 0))
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (availableDocument doc in docs)
                            {
                                sb.Append(doc.ToXml());
                            }

                            dictResult["AvailableDocument"] = string.Format(Constantes.TEMPLATE_XML_NODE, "AvailableDocument", sb.ToString());
                        }

                        step = "AfterDict";
                    }
                }
                catch (FaultException<InvoiceServiceFault> faultException)
                {
                    var detail = faultException.Detail;
                    this.SetException(dictResult, detail.errorMessage, detail.statusCode, detail.reasonPhrase);
                }
                catch (ProtocolException protocolExc)
                {
                    this.SetException(dictResult, protocolExc.Message, protocolExc.StackTrace);
                }

                #endregion

            }
            catch (Exception excepcion)
            {
                this.SetException(dictResult, excepcion.Message, excepcion.StackTrace);
            }
            finally
            {
                dictResult["Step"] = step;
            }

            return dictResult;
        }

        #endregion

        #endregion

        #region Metodos privados

        #region Initialize

        private void Initialize()
        {

            this.m_Proxy = new WSCarvajal.invoiceServiceClient("InvoiceServiceImplPort", this.m_Url);

            var behavior = new PasswordDigestBehavior(this.m_Username, Sha256(this.m_Password));
            this.m_Proxy.Endpoint.EndpointBehaviors.Add(behavior);
        }

        #endregion

        #region Sha256

        private string Sha256(string text)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        #endregion

        #region FaultDictionary

        private Dictionary<string, string> FaultDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("faultcode", "");
            dict.Add("faultstring", "");
            dict.Add("statusCode", "");
            dict.Add("reasonPhrase", "");
            dict.Add("errorMessage", "");

            return dict;
        }

        #endregion

        #region SetException

        private void SetException(Dictionary<string, string> dict, string Message, string StackTrace)
        {
            dict["datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dict["url"] = this.m_Url;
            dict["username"] = this.m_Username;
            dict["companyId"] = this.m_Company;
            dict["accountId"] = this.m_Account;
            dict["statusCode"] = this.m_Excepcion_Code;
            dict["errorMessage"] = Message;
            dict["reasonPhrase"] = StackTrace;
        }

        private void SetException(Dictionary<string, string> dict, string Message, string statusCode, string reasonPhrase)
        {
            dict["datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dict["url"] = this.m_Url;
            dict["username"] = this.m_Username;
            dict["companyId"] = this.m_Company;
            dict["accountId"] = this.m_Account;
            dict["statusCode"] = statusCode;
            dict["errorMessage"] = Message;
            dict["reasonPhrase"] = reasonPhrase;
        }

        #endregion

        #endregion

    }
}
