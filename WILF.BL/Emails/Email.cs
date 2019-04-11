using System;

namespace WILF.BL.Emails
{
    public sealed class Email
    {
        #region Variables

        private static string address = System.Configuration.ConfigurationManager.AppSettings["adress_email"];
        private static string host = System.Configuration.ConfigurationManager.AppSettings["host_email"];
        private static string puerto = System.Configuration.ConfigurationManager.AppSettings["port_email"];
        private static string username = System.Configuration.ConfigurationManager.AppSettings["user_email"];
        private static string password = System.Configuration.ConfigurationManager.AppSettings["password_email"];
        private static Int32 port = 0;

        #endregion

        #region Metodos publicos
        /// <summary>
        /// Envió de corrego electronico 
        /// </summary>
        /// <param name="mailMessage">Se hace envio de los valores de email</param>
        /// <returns></returns>
        public static bool send(System.Net.Mail.MailMessage mailMessage)
        {
            bool result = false;
            if (ValidarVariables())
            {
                try
                {
                    using (System.Net.Mail.SmtpClient oMail = new System.Net.Mail.SmtpClient())
                    {
                        oMail.Host = host;
                        oMail.Port = port;
                        oMail.EnableSsl = false;
                        oMail.UseDefaultCredentials = false;
                        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                        {
                            oMail.Credentials = new System
                                .Net
                                .NetworkCredential(username, password);
                        }
                        oMail.Timeout = 500000;
                        oMail.Send(mailMessage);
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mailMessage.Dispose();
                    mailMessage = null;
                }
            }
            return result;
        }
        /// <summary>
        /// Envio de correo electronico
        /// </summary>
        /// <param name="to">Destinatarios separado por como o punto y coma</param>
        /// <param name="subject">Asundo el correo electronico</param>
        /// <param name="body">Cuerpo del correo electronico</param>
        /// <param name="cc">Copias de correo electronico, opcional</param>
        /// <returns></returns>
        public static bool send(string to, string subject, string body, string cc = null)
        {
            bool result = false;
            if (ValidarVariables())
            {
                try
                {
                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                    {
                        mailMessage.IsBodyHtml = true;
                        mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        mailMessage.From = new System.Net.Mail.MailAddress(address);

                        mailMessage.IsBodyHtml = true;
                        mailMessage.Priority = System.Net.Mail.MailPriority.High;
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        if (!string.IsNullOrEmpty(to))
                        {
                            to = to.Replace(",", ";");
                            foreach (string email in to.Split(';'))
                            {
                                mailMessage.To.Add(email);
                            }
                        }
                        else
                        {
                            throw new ArgumentException(@"No ingreso el parametro de ""to"", debe de ingresar este parametro");
                        }
                        if (!string.IsNullOrEmpty(cc))
                        {
                            cc = cc.Replace(",", ";");
                            foreach (string email in cc.Split(';'))
                            {
                                mailMessage.CC.Add(email);
                            }
                        }
                        using (System.Net.Mail.SmtpClient oMail = new System.Net.Mail.SmtpClient())
                        {
                            oMail.Host = host;
                            oMail.Port = port;
                            oMail.EnableSsl = false;
                            oMail.UseDefaultCredentials = false;
                            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                            {
                                oMail.Credentials = new System
                                    .Net
                                    .NetworkCredential(username, password);
                            }
                            oMail.Timeout = 500000;
                            oMail.Send(mailMessage);
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region Metodos privados

        private static bool ValidarVariables()
        {
            bool result = true;
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Debe crear en el web.config en la sección AppSettings el key: adress_email");
            }
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Debe crear en el web.config en la sección AppSettings el key: host_email");
            }
            if (string.IsNullOrEmpty(puerto))
            {
                throw new ArgumentException("Debe crear en el web.config en la sección AppSettings el key: port_email");
            }
            else
            {
                if (!Int32.TryParse(puerto, out port))
                {
                    throw new ArgumentException("La variable del web.config en la sección AppSettings el key: port_email no es numerico");
                }
            }
            return result;
        }

        #endregion
    }
}
