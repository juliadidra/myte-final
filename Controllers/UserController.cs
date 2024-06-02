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

        private CriarAcessoService _criarAcessoService;

        public UserController(LoginService loginService, CriarAcessoService criarAcessoService)
        {
            _loginService = loginService;
            _criarAcessoService = criarAcessoService;
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

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            try
            {
                await _criarAcessoService.AddAcessoAsync(user);
                return RedirectToAction("ListaFuncionarios", "Func");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao tentar se logar");
            }

            return View(user);

        }
    }

}