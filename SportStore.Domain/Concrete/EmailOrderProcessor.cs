using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Entities;
using SportStore.Domain.Abstract;
using System.Net.Mail;
using System.Net;

namespace SportStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "1107035047@qq.com";
        public string MailFromAddress = "zhongxian117@163.com";
        public bool UseSsl = true;
        public string UserName = "username";
        public string Password = "password";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"d:/sports_store_emails";
    }

    //邮件订单处理器
    public class EmailOrderProcessor : IOrderProcessor {

        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings) {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo) {

            using (var smtpClient = new SmtpClient()) {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName,emailSettings.Password);

                if (emailSettings.WriteAsFile) {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder().AppendLine("a new order has  been submitted").AppendLine("---").AppendLine("Items");

                foreach(var line in cart.lines){
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0}x{1}(subtotal:{2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value:{0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrqp:{0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress,emailSettings.MailToAddress,"New order submitted!",body.ToString());

                if (emailSettings.WriteAsFile) {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
