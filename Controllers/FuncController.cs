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

        public FuncController(FuncionarioService funcionarioService, DepartamentoService departamentoService)
        {
            _funcionarioService = funcionarioService;
            _departamentoService = departamentoService;
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
                ModelState.AddModelError(string.Empty, "Não foi possivel listar os registro de funcionario");
                return View(new List<Funcionario>());
            }
        }

        public ViewResult GetFuncionarioUnico() => View();

        [HttpPost]
        public async Task<IActionResult> GetFuncionarioUnico(string email)
        {
            //requisição com uso do serivce
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);

            if (funcionario == null)
            {
                return NotFound();
            }

            return RedirectToAction("ListaFuncionarios", new { email = funcionario.Email });
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
            //criar a requisição de selção do registro para ser excluido
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);
            if (funcionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.DeleteFuncionarioAsync(email);
            TempData["SuccessMessage"] = "exclusao realizada com sucesso!";
            return RedirectToAction(nameof(ListaFuncionarios));
        }






        /*
        public IActionResult ListaFuncionarios()
        {
            return View(RepositoryFunc.TodosOsFuncionarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Funcionario registroFunc)
        {

            if (ModelState.IsValid)
            {
                RepositoryFunc.Inserir(registroFunc); //ação de inserção de dados na lista

                TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";

                return Redirect("/User/Create"); // view message
            }
            else
            {
                TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
                return View();
            }



        }
        //Atualizar (Update)       
        public IActionResult Update(string Identificador) // Primeiro método
        {           
            Funcionario consulta = RepositoryFunc.TodosOsFuncionarios.Where((r) => r.Nome == Identificador ).First();
            return View(consulta);
        }

        //Sobrecarga do Update
        [HttpPost]
        public IActionResult Update(string Identificador, Funcionario registroAlterado)
        {
            /*  if (ModelState.IsValid)
              {
                  //Alteração da prop Sobrenome
                  var consulta = RepositoryFunc.TodosOsFuncionarios.Where((r) => r.Nome ==
                  Identificador).First();

                  if (foto != null)
                  {
                      using (var memoryStream = new MemoryStream())
                      {
                          foto.CopyTo(memoryStream);
                          registroAlterado.Foto = memoryStream.ToArray();
                      }
                  }

            if (ModelState.IsValid)
            {
                var consulta = RepositoryFunc.TodosOsFuncionarios.Where((r) => r.Nome == Identificador).First();
                consulta.Nome = registroAlterado.Nome;
                consulta.Sobrenome = registroAlterado.Sobrenome;
                consulta.DataDeNascimento = registroAlterado.DataDeNascimento;
                consulta.Email = registroAlterado.Email;
                consulta.DataDeContratacao = registroAlterado.DataDeContratacao;
                consulta.Genero = registroAlterado.Genero;
                consulta.Senioridade = registroAlterado.Senioridade;
                consulta.Cargo = registroAlterado.Cargo;
                consulta.Departamento = registroAlterado.Departamento;
                consulta.Acesso = registroAlterado.Acesso;
                //consulta.Foto = registroAlterado.Foto;

                return Redirect("ListaFuncionarios");
            }


             return View(); 

        }

        [HttpPost]
        public IActionResult Delete(string Identificador)
        {
            //Definir a consulta exclusão
            try
            {
                Funcionario consulta = RepositoryFunc.TodosOsFuncionarios.Where((r) => r.Nome ==
                Identificador).First();
                // acessar o método Excluir - partir da classe Repository
                RepositoryFunc.Excluir(consulta);
                TempData["SuccessMessage"] = "exclusao realizada com sucesso!";
                return RedirectToAction("ListaFuncionarios");
            }
            catch
            {
                TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
                return Redirect("Index");
            }

        }*/
    }
}

