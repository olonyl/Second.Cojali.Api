using Second.Cojali.Domain.Ports;

namespace Second.Cojali.Infrastructure.Adapters;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Simulate asynchronous email sending
        await Task.Run(() =>
        {
            Console.WriteLine($"Simulating email to: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
        });
    }
}
