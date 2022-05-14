using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracking.util
{
    public class EmailHelper
    {
        public EmailHelper()
        {

        }
        public static void SendRegisterted(string registeredCustomerEmail, string userName, string password)
        {
            //var schoolEmail = "sliititpscmc@gmail.com";
            //var passowrd = "1qaz2wsx@";

            MailMessage message = new MailMessage(userName,password);

            string mailBody = "User Name :-" + userName + " " + "Password:-" + password + Environment.NewLine + "Please Don't Reply(Auto genarated Email_SMTP Server)" + Environment.NewLine
                + "Deparment of Computer Science and Software Engineering -  SLIIT)" + Environment.NewLine + "_RESTful API Debugging_ASP.net core";

            message.Subject = "Customer Registered Successfully";

            message.Body = mailBody;

            message.BodyEncoding = Encoding.UTF8;

            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            System.Net.NetworkCredential networkCredential = new

            System.Net.NetworkCredential(registeredCustomerEmail, password);

            client.EnableSsl = true;

            client.UseDefaultCredentials = false;

            client.Credentials = networkCredential;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}
