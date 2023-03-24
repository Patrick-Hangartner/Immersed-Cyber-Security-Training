using Immersed.Models.Requests;
using Immersed.Models.Requests.InviteMembers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Immersed.Services.Interfaces
{
    public interface IEmailsService
    {
        public void WelcomeEmail();
        
        void PhishingEmail(string token, PhishingAddRequest model);
        public void SendConfirmEmail(string token, string email);
        public void SendInviteEmail(InviteMembersAddRequest model, string token);
        public void ContactUsEmail(ContactUsAddRequest model);
        public void SendConfirmContactUsEmail(ContactUsAddRequest model);
    }
}
