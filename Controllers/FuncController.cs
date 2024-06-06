using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myte.Models;
using myte.Services;
using System.Security.Cryptography.X509Certificates;

namespace Projeto.ASPNET.MVC.CRUD_MyTE.Controllers
{

    public class FuncController : Controller
    {
        private FuncionarioService _funcionarioService;
        private DepartamentoService _departamentoService;

        private CriarAcessoService _criarAcessoService;

        public FuncController(FuncionarioService funcionarioService, DepartamentoService departamentoService, CriarAcessoService criarAcessoService)
        {
            _funcionarioService = funcionarioService;
            _departamentoService = departamentoService;
            _criarAcessoService = criarAcessoService;
        }

        //1 tarefa crud: Read - Leitura e recuperação de dados
        public async Task<IActionResult> ListaFuncionarios(string email = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    // Se um email foi fornecido, buscar apenas o funcionário com esse email
                    var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);
                    if (funcionario != null)
                    {
                       
                        return View(new List<Funcionario> { funcionario });
                    }
                }

                // Se nenhum email foi fornecido, retornar todos os funcionários
                var funcionarios = await _funcionarioService.GetAllFuncionarioAsync();
                return View(funcionarios);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Não existe funcionario com esse email";
                return View(new List<Funcionario>());
            }
        }

        public ViewResult GetFuncionarioUnico() => View();

        [HttpPost]
        public async Task<IActionResult> GetFuncionarioUnico(string email)
        {

            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);

                if (funcionario != null)
                {
                    return RedirectToAction("ListaFuncionarios", new { email = funcionario.Email });
                }        
            }
            catch
            {
                TempData["ErrorMessage"] = "Não existe funcionario com esse email";
            }

            return RedirectToAction("ListaFuncionarios");
        }

        public async Task<IActionResult> AdicionarFuncionario()
        {
            var depFunc = new DepartamentoFuncionarioViewModel
            {
                departamento = await _departamentoService.GetAllDepartamentosAsync()
            };
            return View(depFunc);
        }

        //sobrecargar do método AddEstudante()
        [HttpPost]
        public async Task<IActionResult> AdicionarFuncionario(Funcionario funcionario)
        {
            var depFunc = new DepartamentoFuncionarioViewModel
            {
                
            funcionario = funcionario,
                departamento = await _departamentoService.GetAllDepartamentosAsync()
            };
            TempData["EmailFuncionario"] = funcionario.Email;

            if (ModelState.IsValid)
            {
                try
                {
                    await _funcionarioService.AddFuncionarioAsync(funcionario);
                    return RedirectToAction("Create", "User");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar o registro de Funcionario");
                }
            }
            return View(depFunc);

        }

        public async Task<IActionResult> UpdateFuncionario(string email)
        {
            var depFunc = new DepartamentoFuncionarioViewModel
            {
                funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email),
                departamento = await _departamentoService.GetAllDepartamentosAsync()
            };
            //criar a requisição de seleção do registro[
            if (depFunc.funcionario == null)
            {
                return NotFound();
            }
            return View(depFunc);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFuncionario(string email, Funcionario funcionario)
        {
            try
            {

                if (email != funcionario.Email)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    if (funcionario.Departamento_Id == 0)
                    {
                        funcionario.Departamento_Id = null;
                    }
                    await _funcionarioService.UpdateFuncionarioAsync(email, funcionario);
                    return RedirectToAction(nameof(ListaFuncionarios));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Erro ao atualizar o registro de Funcionario");
            }
            return View(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFuncionario(string email)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);

                var acesso = await _criarAcessoService.GetAcessoAsync(email);


                if (funcionario != null && acesso != null)
                {
                    await _funcionarioService.DeleteFuncionarioAsync(email);

                    await _criarAcessoService.DeleteAcessoAsync(email);

                    TempData["SuccessMessage"] = "Exclusão realizada com sucesso!";
                    return RedirectToAction(nameof(ListaFuncionarios));
                }

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Não é possivel excluir, existem registro de horas associado a esse funcionario.";
            }
            return RedirectToAction(nameof(ListaFuncionarios));
        }
    }
}



