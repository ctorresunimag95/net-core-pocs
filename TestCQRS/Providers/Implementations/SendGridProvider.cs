namespace TestCQRS.Providers.Implementations
{
    public class SendGridProvider : IEmailProvider
    {
        public string SendEmail()
        {
            return "Email sent by SendGrid";
        }
    }
}
