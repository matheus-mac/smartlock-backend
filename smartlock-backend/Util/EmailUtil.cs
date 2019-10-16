using smartlock_backend.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace smartlock_backend.Util
{
    public class EmailUtil
    {

        public static SmtpClient smtp = new SmtpClient("smtp.live.com")
        {
            EnableSsl = true,
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("sistema-caracracha@outlook.com", "tcc2Mac13")
        };


        public static bool EnviarEmail(string emailDestinatario, string assunto, string texto)
        {
            MailMessage email = new MailMessage
            {
                From = new MailAddress("sistema-caracracha@outlook.com")
            };
            email.To.Add(emailDestinatario);
            email.Subject = assunto;
            email.Body = texto;

            try
            {
                smtp.Send(email);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
            return true;
        }


    }
}