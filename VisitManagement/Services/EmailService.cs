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

            return await SendEmailAsync(userEmail, subject, body, template);
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

            return await SendEmailAsync(userEmail, subject, body, template);
        }

        public async Task<bool> SendTestEmailAsync(string toEmail)
        {
            return await SendEmailAsync(toEmail, "Test Email from Visit Management System", 
                "This is a test email to verify your SMTP configuration is working correctly.");
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string body, EmailTemplate? template = null)
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

                // Add primary recipient (the user who triggered the email)
                message.To.Add(toEmail);

                // Add template-specific recipients if template is provided
                if (template != null)
                {
                    // Add To recipients from template
                    if (!string.IsNullOrWhiteSpace(template.ToRecipients))
                    {
                        AddRecipients(message.To, template.ToRecipients);
                    }

                    // Add CC recipients from template
                    if (!string.IsNullOrWhiteSpace(template.CcRecipients))
                    {
                        AddRecipients(message.CC, template.CcRecipients);
                    }

                    // Add BCC recipients from template
                    if (!string.IsNullOrWhiteSpace(template.BccRecipients))
                    {
                        AddRecipients(message.Bcc, template.BccRecipients);
                    }
                }

                await smtpClient.SendMailAsync(message);
                _logger.LogInformation($"Email sent successfully to {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {toEmail}");
                return false;
            }
        }

        private void AddRecipients(MailAddressCollection collection, string recipients)
        {
            if (string.IsNullOrWhiteSpace(recipients))
                return;

            var emails = recipients.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in emails)
            {
                var trimmedEmail = email.Trim();
                if (!string.IsNullOrEmpty(trimmedEmail))
                {
                    try
                    {
                        collection.Add(trimmedEmail);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Invalid email address: {trimmedEmail}. Error: {ex.Message}");
                    }
                }
            }
        }

        private string ReplacePlaceholders(string text, Visit visit)
        {
            return text
                .Replace("{AccountName}", visit.AccountName ?? "")
                .Replace("{VisitDate}", visit.VisitDate.ToString("dd/MM/yyyy"))
                .Replace("{Location}", visit.Location ?? "")
                .Replace("{VisitStatus}", visit.VisitStatus.ToString())
                .Replace("{SalesSpoc}", visit.SalesSpoc ?? "")
                .Replace("{OpportunityType}", visit.OpportunityType.ToString())
                .Replace("{VisitType}", visit.VisitType ?? "")
                .Replace("{VisitorsName}", visit.VisitorsName ?? "")
                .Replace("{IntimationDate}", visit.IntimationDate.ToString("dd/MM/yyyy"));
        }
    }
}
