
using User.Gestion.Service.Models;

namespace User.Gestion.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
