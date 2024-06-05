using Microsoft.AspNetCore.Authorization;
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

        private FuncionarioService _funcionarioService;


        public UserController(LoginService loginService, CriarAcessoService criarAcessoService, FuncionarioService funcionarioService)
        {
            _loginService = loginService;
            _criarAcessoService = criarAcessoService;
            _funcionarioService = funcionarioService;
        }

        public ViewResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(login.Email);
                HttpContext.Session.SetString("UserEmail", funcionario.Email); // Armazenando o email na sessão

                TempData["EmailUsuario"] = login.Email;

                if (funcionario == null)
                {

                    TempData["ErrorMessage"] = "Não é possivel realizar o login";
                    return View();
                }

                await _loginService.AddLoginAsync(login);
                if (funcionario.Acesso == "Admin")
                {
                    return RedirectToAction("Home", "Home");

                }
                else if (funcionario.Acesso == "Gerente")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Calendar");
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Não é possivel realizar o login";
                return View();

            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            bool logout = await _loginService.LogoutAsync();

            if (logout)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Erro ao realizar logout.");
                return View();
            }

        }


        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            try
            {
                await _criarAcessoService.AddAcessoAsync(user);
                TempData["SuccessMessage"] = "Funcionario cadastrado com sucesso!";
                return RedirectToAction("ListaFuncionarios", "Func");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao criar acesso");
            }

            return View(user);

        }
    }

}