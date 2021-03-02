using System;

namespace TestCQRS.Providers.Implementations
{
    public class SmtpProvider : IEmailProvider
    {
        public string SendEmail()
        {
            return "Email sent by SMTP server";
        }
    }
}
