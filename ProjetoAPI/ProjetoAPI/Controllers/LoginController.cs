using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Models;
using ProjetoAPI.Services;

namespace ProjetoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest(new { error = "Nome de usuário e senha são obrigatórios." });
        }

        var isAuthenticated = _loginService.Authenticate(model.Username, model.Password);

        if (!isAuthenticated)
        {
            return Unauthorized(new { error = "Nome de usuário ou senha inválidos." });
        }

        return Ok(new { message = "Login bem-sucedido!" });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest(new { error = "Nome de usuário e senha são obrigatórios." });
        }

        var isRegistered = _loginService.RegisterUser(model.Username, model.Password);

        if (!isRegistered)
        {
            return BadRequest(new { error = "Usuário já registrado." });
        }

        return Ok(new { message = "Usuário registrado com sucesso!" });
    }
}
