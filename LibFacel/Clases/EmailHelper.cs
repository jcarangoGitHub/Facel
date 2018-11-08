
#region Referencias

using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

//Ejemplo https://www.codeproject.com/Tips/520998/Send-Email-from-Yahoo-GMail-Hotmail-Csharp
// GMail	smtp.gmail.com	587	Yes

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    public class EmailHelper
    {

        #region Metodos publicos estaticos

        #region SendEmailHotmail

        public static string SendEmailHotmail(string emailTo, string emailSubject, string emailBody, string filesAttach = "")
        {

            #region Variables de trabajo

            bool enableSSL = true;
            int portNumber = 587;
            string errorMessage = string.Empty;
            string smtpAddress = "smtp.live.com";
            string emailFrom = "jcarango@hotmail.com";
            //string emailTo = "julio.arango@gmail.com";

            #endregion

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string password = "pwdDB678703.*";
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = emailSubject;
                    mail.Body = emailBody;
                    mail.IsBodyHtml = false;

                    if (!string.IsNullOrWhiteSpace(filesAttach))
                    {
                        mail.Attachments.Add(new Attachment(filesAttach));
                        //mail.Attachments.Add(new Attachment("D:\\DatosJulio\\Habitos.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                    }

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception excepcion)
            {
                errorMessage = string.Format(Constantes.TEMPLATE_ERROR, "SendEmailHotmail", excepcion.Message);
            }

            return errorMessage;
        }

        #endregion

        #region SendEmailDBExperts

        public static string SendEmailDBExperts(string emailTo, string emailSubject, string emailBody)
        {

            #region Variables de trabajo

            int puerto = 25;
            string errorMessage = string.Empty;
            string remitente = "admin@dbexperts.com.co";
            string passwordRemitente = "Inspiron1420";
            //string destinatario = "julio.arango@gmail.com";
            //string servidor = "127.0.0.1";//127.0.0.1
            string servidor = "190.8.176.202";

            #endregion

            try
            {
                MailMessage objMail = new MailMessage(remitente, emailTo, emailSubject, emailBody);
                NetworkCredential objNC = new NetworkCredential(remitente, passwordRemitente);
                SmtpClient objsmtp = new SmtpClient(servidor, puerto);

                objsmtp.EnableSsl = false;
                objsmtp.UseDefaultCredentials = false;
                objsmtp.Credentials = objNC;
                objsmtp.Send(objMail);
            }
            catch (Exception excepcion)
            {
                errorMessage = string.Format(Constantes.TEMPLATE_ERROR, "SendEmailDBExperts", excepcion.Message);
            }

            return errorMessage;
        }

        #endregion

        #region EmailIsValid

        public static bool EmailIsValid(string email)
        {
            bool result = false;
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion

        #endregion

    }
}
