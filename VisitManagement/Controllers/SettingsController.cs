using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;
using VisitManagement.Services;

namespace VisitManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public SettingsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            var smtpSettings = await _context.SmtpSettings.FirstOrDefaultAsync();
            var emailTemplates = await _context.EmailTemplates.OrderBy(t => t.TemplateType).ToListAsync();

            ViewBag.SmtpSettings = smtpSettings;
            return View(emailTemplates);
        }

        // GET: Settings/ConfigureSmtp
        public async Task<IActionResult> ConfigureSmtp()
        {
            var settings = await _context.SmtpSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new SmtpSettings();
            }
            return View(settings);
        }

        // POST: Settings/ConfigureSmtp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigureSmtp(SmtpSettings settings)
        {
            if (ModelState.IsValid)
            {
                var existing = await _context.SmtpSettings.FirstOrDefaultAsync();
                if (existing == null)
                {
                    settings.CreatedDate = DateTime.Now;
                    settings.ModifiedDate = DateTime.Now;
                    _context.SmtpSettings.Add(settings);
                }
                else
                {
                    existing.Server = settings.Server;
                    existing.Port = settings.Port;
                    existing.FromEmail = settings.FromEmail;
                    existing.FromName = settings.FromName;
                    existing.Username = settings.Username;
                    existing.Password = settings.Password;
                    existing.DefaultToRecipients = settings.DefaultToRecipients;
                    existing.DefaultCcRecipients = settings.DefaultCcRecipients;
                    existing.EnableSsl = settings.EnableSsl;
                    existing.EnableNotifications = settings.EnableNotifications;
                    existing.ModifiedDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = "SMTP settings saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(settings);
        }

        // GET: Settings/TestEmail
        public IActionResult TestEmail()
        {
            return View();
        }

        // POST: Settings/SendTestEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendTestEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Please enter an email address.";
                return RedirectToAction(nameof(TestEmail));
            }

            var result = await _emailService.SendTestEmailAsync(email);
            if (result)
            {
                TempData["Success"] = $"Test email sent successfully to {email}!";
            }
            else
            {
                TempData["Error"] = "Failed to send test email. Please check your SMTP settings.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Settings/CreateTemplate
        public IActionResult CreateTemplate()
        {
            return View(new EmailTemplate());
        }

        // POST: Settings/CreateTemplate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTemplate(EmailTemplate template)
        {
            if (ModelState.IsValid)
            {
                template.CreatedDate = DateTime.Now;
                template.ModifiedDate = DateTime.Now;
                _context.EmailTemplates.Add(template);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Email template created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Settings/EditTemplate/5
        public async Task<IActionResult> EditTemplate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.EmailTemplates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Settings/EditTemplate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTemplate(int id, EmailTemplate template)
        {
            if (id != template.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    template.ModifiedDate = DateTime.Now;
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Email template updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailTemplateExists(template.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Settings/DeleteTemplate/5
        public async Task<IActionResult> DeleteTemplate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.EmailTemplates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Settings/DeleteTemplate/5
        [HttpPost, ActionName("DeleteTemplate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTemplateConfirmed(int id)
        {
            var template = await _context.EmailTemplates.FindAsync(id);
            if (template != null)
            {
                _context.EmailTemplates.Remove(template);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Email template deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmailTemplateExists(int id)
        {
            return _context.EmailTemplates.Any(e => e.Id == id);
        }
    }
}
