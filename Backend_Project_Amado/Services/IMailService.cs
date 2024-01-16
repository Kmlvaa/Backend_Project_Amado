namespace Backend_Project_Amado.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
