using Domain.DTOs;

namespace Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegistroUsuarioDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }    
}
