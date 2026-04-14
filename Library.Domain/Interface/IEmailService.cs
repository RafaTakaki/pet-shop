namespace Library.Domain.Interface;

public interface IEmailService
{
    Task<bool> EnviarEmailAsync(string toEmail, string body);
}
