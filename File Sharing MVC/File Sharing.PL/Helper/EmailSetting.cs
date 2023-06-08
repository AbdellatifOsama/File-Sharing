using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using NuGet.Common;

namespace File_Sharing.PL.Helper
{
    public class EmailSetting
    {
        public static async Task SendEmail(string reciever, string header, string body)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "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");
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("abdellatifosama81@gmail.com", "Abdellatif Osama");
            var subject = header;
            var to = new EmailAddress(reciever, "Example User");
            var plainTextContent = body;
            var htmlContent = EmailHTMLBuilder(body);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        private static string EmailHTMLBuilder(string url)
        {
            var html = $" <div style=\"text-align: center;\">\r\n" +
                $"    <h1>\r\n    Reset Password Link\r\n  </h1>\r\n  <p>\r\n   " +
                $"<a style=\"background-color:#0d6efd;border-radius:4px;color:#fff;display:inline-block;font-family:Helvetica, " +
                $"Arial, sans-serif;font-size:18px;font-weight:normal;line-height:50px;text-align:center;text-decoration:none;width:350px;" +
                $"-webkit-text-size-adjust:none;\" href=\"{url}\">click here to go to reset Page</a>\r\n  " +
                $"</p>\r\n </div>";
            return html;
        }
    }
}
