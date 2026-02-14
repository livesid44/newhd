using VisitManagement.Models;

namespace VisitManagement.Services
{
    public interface IEmailService
    {
        Task<bool> SendVisitCreatedEmailAsync(Visit visit, string userEmail);
        Task<bool> SendVisitUpdatedEmailAsync(Visit visit, string userEmail);
        Task<bool> SendTestEmailAsync(string toEmail);
        Task<bool> SendVisitNotificationAsync(Visit visit, EmailTemplateType templateType);
        Task<bool> SendTaskNotificationAsync(TaskAssignment task, EmailTemplateType templateType);
    }
}
