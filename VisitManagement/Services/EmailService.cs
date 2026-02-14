using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Services
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ApplicationDbContext context, ILogger<EmailService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> SendVisitCreatedEmailAsync(Visit visit, string userEmail)
        {
            var template = await _context.EmailTemplates
                .FirstOrDefaultAsync(t => t.TemplateType == "VisitCreated" && t.IsActive);

            if (template == null)
            {
                _logger.LogWarning("No active email template found for VisitCreated");
                return false;
            }

            var subject = ReplacePlaceholders(template.Subject, visit);
            var body = ReplacePlaceholders(template.Body, visit);

            return await SendEmailAsync(
                template.ToRecipients ?? userEmail, 
                template.CcRecipients, 
                template.BccRecipients, 
                subject, 
                body);
        }

        public async Task<bool> SendVisitUpdatedEmailAsync(Visit visit, string userEmail)
        {
            var template = await _context.EmailTemplates
                .FirstOrDefaultAsync(t => t.TemplateType == "VisitUpdated" && t.IsActive);

            if (template == null)
            {
                _logger.LogWarning("No active email template found for VisitUpdated");
                return false;
            }

            var subject = ReplacePlaceholders(template.Subject, visit);
            var body = ReplacePlaceholders(template.Body, visit);

            return await SendEmailAsync(
                template.ToRecipients ?? userEmail, 
                template.CcRecipients, 
                template.BccRecipients, 
                subject, 
                body);
        }

        public async Task<bool> SendTestEmailAsync(string toEmail)
        {
            return await SendEmailAsync(toEmail, null, null, "Test Email from Visit Management System", 
                "This is a test email to verify your SMTP configuration is working correctly.");
        }

        private async Task<bool> SendEmailAsync(string toEmails, string? ccEmails, string? bccEmails, string subject, string body)
        {
            try
            {
                var settings = await _context.SmtpSettings.FirstOrDefaultAsync();
                
                if (settings == null || !settings.EnableNotifications)
                {
                    _logger.LogWarning("SMTP settings not configured or notifications disabled");
                    return false;
                }

                using var smtpClient = new SmtpClient(settings.Server, settings.Port)
                {
                    EnableSsl = settings.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                if (!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password))
                {
                    smtpClient.Credentials = new NetworkCredential(settings.Username, settings.Password);
                }

                using var message = new MailMessage
                {
                    From = new MailAddress(settings.FromEmail, settings.FromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                // Add TO recipients
                var toAddresses = string.IsNullOrEmpty(toEmails) 
                    ? (settings.DefaultToRecipients ?? "").Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    : toEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var email in toAddresses)
                {
                    var trimmedEmail = email.Trim();
                    if (!string.IsNullOrEmpty(trimmedEmail))
                    {
                        message.To.Add(trimmedEmail);
                    }
                }

                // Add CC recipients
                var ccAddresses = string.IsNullOrEmpty(ccEmails)
                    ? (settings.DefaultCcRecipients ?? "").Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    : ccEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var email in ccAddresses)
                {
                    var trimmedEmail = email.Trim();
                    if (!string.IsNullOrEmpty(trimmedEmail))
                    {
                        message.CC.Add(trimmedEmail);
                    }
                }

                // Add BCC recipients
                var bccAddresses = string.IsNullOrEmpty(bccEmails)
                    ? (settings.DefaultBccRecipients ?? "").Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    : bccEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var email in bccAddresses)
                {
                    var trimmedEmail = email.Trim();
                    if (!string.IsNullOrEmpty(trimmedEmail))
                    {
                        message.Bcc.Add(trimmedEmail);
                    }
                }

                if (message.To.Count == 0)
                {
                    _logger.LogWarning("No TO recipients specified for email");
                    return false;
                }

                await smtpClient.SendMailAsync(message);
                _logger.LogInformation($"Email sent successfully to {string.Join(", ", message.To.Select(t => t.Address))}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email");
                return false;
            }
        }

        // New unified method to send visit notifications based on template type
        public async Task<bool> SendVisitNotificationAsync(Visit visit, EmailTemplateType templateType)
        {
            var templateTypeString = templateType.ToString();
            var template = await _context.EmailTemplates
                .FirstOrDefaultAsync(t => t.TemplateType == templateTypeString && t.IsActive);

            if (template == null)
            {
                _logger.LogWarning($"No active email template found for {templateType}");
                return false;
            }

            var subject = ReplacePlaceholders(template.Subject, visit);
            var body = ReplacePlaceholders(template.Body, visit);

            return await SendEmailAsync(
                template.ToRecipients, 
                template.CcRecipients, 
                template.BccRecipients, 
                subject, 
                body);
        }

        // New unified method to send task notifications based on template type
        public async Task<bool> SendTaskNotificationAsync(TaskAssignment task, EmailTemplateType templateType)
        {
            var templateTypeString = templateType.ToString();
            var template = await _context.EmailTemplates
                .FirstOrDefaultAsync(t => t.TemplateType == templateTypeString && t.IsActive);

            if (template == null)
            {
                _logger.LogWarning($"No active email template found for {templateType}");
                return false;
            }

            var subject = ReplacePlaceholders(template.Subject, task);
            var body = ReplacePlaceholders(template.Body, task);

            return await SendEmailAsync(
                template.ToRecipients, 
                template.CcRecipients, 
                template.BccRecipients, 
                subject, 
                body);
        }

        private string ReplacePlaceholders(string text, Visit visit)
        {
            return text
                .Replace("{AccountName}", visit.AccountName ?? "")
                .Replace("{VisitDate}", visit.VisitDate.ToString("dd/MM/yyyy"))
                .Replace("{Location}", visit.Location ?? "")
                .Replace("{Category}", visit.Category?.ToString() ?? "Not assigned")
                .Replace("{SalesSpoc}", visit.SalesSpoc ?? "")
                .Replace("{OpportunityType}", visit.OpportunityType.ToString())
                .Replace("{NameAndAttendees}", visit.VisitorsName ?? "");
        }

        private string ReplacePlaceholders(string text, TaskAssignment task)
        {
            var result = text
                .Replace("{TaskName}", task.TaskName ?? "")
                .Replace("{AssignedTeam}", task.AssignedToTeam ?? "")
                .Replace("{DueDate}", task.DueDate.ToString("dd/MM/yyyy"))
                .Replace("{Priority}", task.Priority.ToString())
                .Replace("{TaskStatus}", task.Status.ToString())
                .Replace("{TaskDescription}", task.Description ?? "");

            // Add visit details if available
            if (task.Visit != null)
            {
                result = result
                    .Replace("{AccountName}", task.Visit.AccountName ?? "")
                    .Replace("{VisitDate}", task.Visit.VisitDate.ToString("dd/MM/yyyy"))
                    .Replace("{Location}", task.Visit.Location ?? "")
                    .Replace("{Category}", task.Visit.Category?.ToString() ?? "");
            }

            return result;
        }
    }
}
