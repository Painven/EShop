using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace EShopAPI.Services;

public interface IOrderNotificator
{
    Task SendNewOrderNotification(string customerEmail, int orderNumber);
}

public class MailKitOrderEmailNotificator : IOrderNotificator
{
    private readonly IConfiguration configuration;
    private readonly ILogger logger;

    public MailKitOrderEmailNotificator(IConfiguration configuration, ILogger<MailKitOrderEmailNotificator> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task SendNewOrderNotification(string customerEmail, int orderNumber)
    {
        var configSection = configuration.GetSection("ServerEmailSenderConfiguration");
        string serverUsername = configSection.GetValue<string>("Username");
        string serverPassword = configSection.GetValue<string>("Password");
        string serverSmtpHost = configSection.GetValue<string>("SmtpHost");
        int serverSmtpPort = configSection.GetValue<int>("SmtpPort");

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(serverUsername));
        email.To.Add(MailboxAddress.Parse(customerEmail));
        email.To.Add(MailboxAddress.Parse(serverUsername));
        email.Subject = $"Заказ № {orderNumber} принят в работу";
        email.Body = new TextPart(TextFormat.Plain)
        {
            Text = "Ваш заказ принят в работу. О выполнении будет сообщено отдельно"
        };

        try
        {
            using var smtp = new SmtpClient();
            smtp.Connect(serverSmtpHost, serverSmtpPort, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(serverUsername, serverPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            string successMsg = $"Уведомление о заказе отправлено '{serverUsername ?? string.Empty}'-- > '{customerEmail ?? String.Empty}'";
            logger.LogInformation($"{successMsg}");
        }
        catch (Exception ex)
        {
            string errorMsg= $"Ошибка отправки Email '{serverUsername ?? string.Empty}'-- > '{customerEmail ?? String.Empty}'";
            logger.LogError($"{errorMsg}\r\n{ex.Message}");
        }
    }
}
