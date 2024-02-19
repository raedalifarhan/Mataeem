using Mataeem.Models;

namespace Mataeem.Interfaces
{
    public interface IAuthService
    {
        Task<List<string?>> GetAllRoles();

        Task<string> CreateToken(AppUser user);

        Task<string> CreatePhoneToken(string phoneNumber);

        Task<bool> VerifyPhoneNumber(string phoneNumber, string verificationCode);
    }
}