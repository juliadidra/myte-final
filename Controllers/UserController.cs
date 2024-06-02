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

            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(login.Email);               
            try
            {
             await _loginService.AddLoginAsync(login);
                if (funcionario != null && funcionario.Acesso == "Admin")
                {
                 return RedirectToAction("Home", "Home");

                }
                else 
                {
                 return RedirectToAction("Index", "Calendar");
                }
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