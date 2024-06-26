﻿using Microsoft.AspNetCore.Mvc;
using myte.Models;
using myte.Services;

namespace myte.Controllers
{
    public class WbsController : Controller
    {
        private WbsService _wbsService;

        public WbsController(WbsService wbsService)
        {
            _wbsService = wbsService;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var wbs = await _wbsService.GetAllWbsAsync();

                // trecho adicionado para implementar uma condição
                if (!String.IsNullOrEmpty(searchString))
                {
                    wbs = wbs.Where(s => s.Nome.Contains(searchString)).ToList();
                }
                return View(wbs);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível listar os registros.");
                return View(new List<Wbs>());
            }        

        }

       
        public ViewResult CreateWbs() => View();


        [HttpPost]
        public async Task<IActionResult> CreateWbs(Wbs wbs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _wbsService.AddWbsAsync(wbs);
                    TempData["SuccessMessage"] = "Wbs cadastrada com sucesso!";
                    return RedirectToAction("Index");


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar registro");
                }

            }
            TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
            return View(wbs);


        }


        public async Task<IActionResult> UpdateWbs(string codigo)
        {
            var wbs = await _wbsService.GetWbsByIdAsync(codigo);

            if (wbs == null)
            {
                return NotFound();
            }
            return View(wbs);

            /*Wbs consulta = Repository.TodasAsWbs.Where((r) => r.Codigo == id).First();
            return View(consulta);*/
        }

        //sobrecarga
        [HttpPost]
        public async Task<IActionResult> UpdateWbs(string codigo, Wbs wbs) //alterei o nome de Create para CreateWbs
        {

            if (codigo != wbs.Codigo)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _wbsService.UpdateWbsAsync(codigo, wbs);
                TempData["SuccessMessage"] = "WBS atualizada com sucesso!";
                return Redirect("Index");
            }

            return View();

            /*if (ModelState.IsValid)
            {
                var consulta = Repository.TodasAsWbs.Where((r) => r.Codigo == id).First();

                consulta.Nome = wbsAlterada.Nome;
                consulta.Descricao = wbsAlterada.Descricao;
                consulta.Tipo = wbsAlterada.Tipo;
                consulta.Codigo = wbsAlterada.Codigo;

                
            }
           */

        }

        [HttpPost]
        public async Task<IActionResult> DeleteWbs(string codigo)
        {
            try
            {
                var wbs = await _wbsService.GetWbsByIdAsync(codigo);

                if (wbs != null)
                {
                    await _wbsService.DeleteWbsAsync(codigo);
                    TempData["SuccessMessage"] = "Exclusão realizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Não é possivel excluir, existem registro de horas com essa WBS.";
            }

            return RedirectToAction(nameof(Index));

        }

        // TempData["SuccessMessageDelete"] = "exclusao realizada com sucesso!";


        /*try
        {
            Wbs consulta = Repository.TodasAsWbs.Where((r) => r.Codigo == id).First();
            Repository.Excluir(consulta);


        }
        catch
        {
            TempData["ErrorMessageDelete"] = "Não é possivel prosseguir com a ação";
            return Redirect("Index");
        }*/

    }
}
