using Microsoft.AspNetCore.Mvc;
using myte.Models;
using myte.Services;
using System.Security.Cryptography.X509Certificates;

namespace Projeto.ASPNET.MVC.CRUD_MyTE.Controllers
{
    public class FuncController : Controller
    {
        private FuncionarioService _funcionarioService;

        public FuncController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        //1 tarefa crud: Read - Leitura e recuperação de dados
        public async Task<IActionResult> ListaFuncionarios()
        {
            try
            {
                var funcionario = await _funcionarioService.GetAllFuncionarioAsync();
                return View(funcionario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel listar os registro de funcionarios");
                return View(new List<Funcionario>());
            }

        }

        //2 tarefa assincrona crud: seleção de registro
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
            return View(funcionario);
        }

        public ViewResult Create() => View();

        //sobrecargar do método AdicionarFuncionario()
        [HttpPost]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _funcionarioService.AddFuncionarioAsync(funcionario);
                    TempData["SuccessMessage"] = "Funcionario cadastrado com sucesso!";
                    return RedirectToAction(nameof(ListaFuncionarios));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar o registro de Funcionario");
                }
            }
            return View(funcionario);

        }



        public async Task<IActionResult> Update(string email)
        {
            //criar a requisição de seleção do registro[
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }


        [HttpPost]
        public async Task<IActionResult> Update(string email, Funcionario funcionario)
        {
            if (email != funcionario.Email)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await _funcionarioService.UpdateFuncionarioAsync(email, funcionario);
                TempData["SuccessMessage"] = "Funcionario atualizado com sucesso!";
                return RedirectToAction(nameof(ListaFuncionarios));
            }
            return View(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            //criar a requisição de selção do registro para ser excluido
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(email);
            if (funcionario == null)
            {
                return NotFound();
                //TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
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

