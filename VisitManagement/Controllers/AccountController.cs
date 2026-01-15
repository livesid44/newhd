using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VisitManagement.Models;
using VisitManagement.ViewModels;

namespace VisitManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            // Check if Windows Authentication is enabled and user is authenticated via Windows
            if (User.Identity?.IsAuthenticated == true && User.Identity?.AuthenticationType == "Negotiate")
            {
                // Auto-provision or sign in the Windows authenticated user
                await HandleWindowsAuthenticationAsync();
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account locked out due to multiple failed login attempts. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task HandleWindowsAuthenticationAsync()
        {
            if (User.Identity?.IsAuthenticated != true || User.Identity.Name == null)
            {
                return;
            }

            var windowsIdentity = User.Identity.Name;
            // Extract username from domain\username format
            var username = windowsIdentity.Contains('\\') 
                ? windowsIdentity.Split('\\')[1] 
                : windowsIdentity;

            // Try to find existing user by LDAP user ID or username
            var user = await _userManager.FindByNameAsync(windowsIdentity);
            
            if (user == null)
            {
                // Auto-provision new user from Active Directory
                user = new ApplicationUser
                {
                    UserName = windowsIdentity,
                    Email = $"{username}@domain.com", // You may want to query AD for actual email
                    FullName = username,
                    EmailConfirmed = true,
                    AuthType = AuthenticationType.LDAP,
                    LdapUserId = windowsIdentity,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user);
                
                if (result.Succeeded)
                {
                    // Add to default User role
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
            else if (user.AuthType != AuthenticationType.LDAP)
            {
                // Update existing user to LDAP auth type
                user.AuthType = AuthenticationType.LDAP;
                user.LdapUserId = windowsIdentity;
                await _userManager.UpdateAsync(user);
            }

            // Sign in the user
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
