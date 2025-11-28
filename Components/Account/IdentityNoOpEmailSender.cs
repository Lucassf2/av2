using Microsoft.AspNetCore.Identity;
using HamburgueriaBlazor.Data;
using System.Threading.Tasks;

namespace HamburgueriaBlazor.Components.Account
{
    internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
    {
        public Task SendEmailAsync(ApplicationUser user, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string callbackUrl)
        {
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string callbackUrl)
        {
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string code)
        {
            return Task.CompletedTask;
        }
    }
}
