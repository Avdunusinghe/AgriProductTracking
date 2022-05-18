using AgriProductTracker.ViewModel.Common;
using AgriProductTracker.ViewModel.Order;
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

        public static bool SendPaymentSuccessEmail(CompanyEmailSettingModel _emailSetting, CustomerOrderResponseViewModel model)
        {
            try
            {
                MailMessage email = new MailMessage(_emailSetting.SMTPFrom, model.CustomerEmail);
                email.Body = "Order Comfirmed,Thank You!";
                email.Subject = "DS-Order-Comfired";
                email.IsBodyHtml = _emailSetting.IsEnableHTML;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient client = new SmtpClient(_emailSetting.SMTPServer, _emailSetting.SMTPPort);

                System.Net.NetworkCredential auth = new

                System.Net.NetworkCredential(_emailSetting.SMTPUsername, _emailSetting.SMTPPassword);

                client.EnableSsl = true;

                client.UseDefaultCredentials = false;

                client.Credentials = auth;

                client.Send(email);

            }
            catch(Exception ex)
            {
                return false;
            }

            return true;




            
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
