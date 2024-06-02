using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myte.Models;
using myte.Services;

namespace myte.Controllers
{
    //Operações CRUD do Cadastro de dados
    public class UserController : Controller
    {
        private LoginService _loginService;

        public UserController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public ViewResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            try
            {
                await _loginService.AddLoginAsync(login);
                return RedirectToAction("Home", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao tentar se logar");
            }

            return View(login);
        }

        public IActionResult Create()
        {
            return View();

        }
    }

}