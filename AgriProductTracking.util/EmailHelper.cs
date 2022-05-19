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
            
                MailMessage email = new MailMessage(_emailSetting.SMTPFrom, model.CustomerEmail);
                email.Body = "Order Comfirmed,Thank You!";
                email.Subject = "DS-Order-Comfired";
                email.IsBodyHtml = _emailSetting.IsEnableHTML;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient client = new SmtpClient(_emailSetting.SMTPServer, _emailSetting.SMTPPort);

                System.Net.NetworkCredential networkCredential = new

                System.Net.NetworkCredential(_emailSetting.SMTPFrom, _emailSetting.SMTPPassword);

                client.EnableSsl = _emailSetting.IsSMTPUseSSL;

                client.UseDefaultCredentials = false;

                client.Credentials = networkCredential;
            try
            {
                client.Send(email);

            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
            
        }
        public static bool SendMobileBillAddPaymentMeesage(CompanyEmailSettingModel _emailSetting, CustomerOrderResponseViewModel model)
        {

            MailMessage email = new MailMessage(_emailSetting.SMTPFrom, model.CustomerEmail);
            email.Body = "Order Comfirmed,Thank You!";
            email.Subject = "DS-Order-Comfired,Add Mobile Bill";
            email.IsBodyHtml = _emailSetting.IsEnableHTML;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            SmtpClient client = new SmtpClient(_emailSetting.SMTPServer, _emailSetting.SMTPPort);

            System.Net.NetworkCredential networkCredential = new

            System.Net.NetworkCredential(_emailSetting.SMTPFrom, _emailSetting.SMTPPassword);

            client.EnableSsl = _emailSetting.IsSMTPUseSSL;

            client.UseDefaultCredentials = false;

            client.Credentials = networkCredential;
            try
            {
                client.Send(email);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;


        }

        public static bool SendDeliveryPatnerMessage(CompanyEmailSettingModel _emailSetting, OrderConfirmResponseViewModel model)
        {

            MailMessage email = new MailMessage(_emailSetting.SMTPFrom, model.DeliveryServiceEmail);
            email.Body = "DS Assignment O2 Order Comfirmed!";
            email.Subject = "Pick the order";
            email.IsBodyHtml = _emailSetting.IsEnableHTML;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            SmtpClient client = new SmtpClient(_emailSetting.SMTPServer, _emailSetting.SMTPPort);

            System.Net.NetworkCredential networkCredential = new

            System.Net.NetworkCredential(_emailSetting.SMTPFrom, _emailSetting.SMTPPassword);

            client.EnableSsl = _emailSetting.IsSMTPUseSSL;

            client.UseDefaultCredentials = false;

            client.Credentials = networkCredential;
            try
            {
                client.Send(email);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;


        }



    }
    
}
