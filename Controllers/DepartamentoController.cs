using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using myte.Models;
using myte.Services;
using System.Data;

namespace myte.Controllers
{

    public class DepartamentoController : Controller
    {
        private DepartamentoService _departamentoService;

        public DepartamentoController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        //1 tarefa crud: Read - Leitura e recuperação de dados
        public async Task<IActionResult> Index()
        {
            try
            {
                var departamento = await _departamentoService.GetAllDepartamentosAsync();
                return View(departamento);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel listar os registro de departamentos");
                return View(new List<Departamento>());
            }

        }

        //2 tarefa assincrona crud: seleção de registro
        public ViewResult GetDepartamentoUnico() => View();
        [HttpPost]
        public async Task<IActionResult> GetDepartamentoUnico(int id)
        {
            //requisição com uso do serivce
            var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        public ViewResult Create() => View();

        //sobrecargar do método AdicionarDepartamento()
        [HttpPost]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departamentoService.AddDepartamento(departamento);
                    TempData["SuccessMessage"] = "Departamento cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar o registro de Departamento");
                }
            }
            return View(departamento);

        }



        public async Task<IActionResult> Update(int id)
        {
            //criar a requisição de seleção do registro[
            var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await _departamentoService.UpdateDepartamentoAsync(id, departamento);
                TempData["SuccessMessage"] = "Departamento atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //criar a requisição de selção do registro para ser excluido
                var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
                if (departamento != null)
                {
                    await _departamentoService.DeleteDepartamentoAsync(id);
                    TempData["SuccessMessage"] = "exclusao realizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

            } 
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = "Não é possivel excluir, existem funcionarios nesse departamento.";
              
                //TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
            }

            return RedirectToAction(nameof(Index));

        }

    }
}
