using ProjetoAPI.Models;

namespace ProjetoAPI.Services;

public class LoginService
{
    private readonly List<LoginModel> _usuarios = new();

    public bool Authenticate(string username, string password)
    {
        return _usuarios.Any(u => u.Username == username && u.Password == password);
    }

    public bool RegisterUser(string username, string password)
    {
        if (_usuarios.Any(u => u.Username == username))
        {
            return false; // Usuário já existe
        }

        _usuarios.Add(new LoginModel { Username = username, Password = password });
        return true; // Registro bem-sucedido
    }
}
