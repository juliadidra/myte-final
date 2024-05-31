using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using myte.Models;
using System.Data;

namespace myte.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult Index() 
        {
            return View(Repositorio.TodosOsDepartamentos);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departamento registroDepartamento)
        {
            if (ModelState.IsValid)
            {
                Repositorio.Inserir(registroDepartamento);
                TempData["SuccessMessage"] = "Departamento cadastrado com sucesso!";
                return Redirect("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
                return View();
                
            }
        }

        public IActionResult Update(string Identificador)
        {
            Departamento consulta = Repositorio.TodosOsDepartamentos.Where((r) => r.Nome == Identificador).First();
            return View(consulta);
        }

        [HttpPost]
        public IActionResult Update(string Identificador, Departamento registroAlterado) 
        { 
            if (ModelState.IsValid)
            {
                var consulta = Repositorio.TodosOsDepartamentos.Where((r) => r.Nome == Identificador).First();

                consulta.Nome = registroAlterado.Nome;

                return Redirect("Index");

            }

            return View();

        }


        [HttpPost]
        public IActionResult Delete(string Identificador)
        {
            try
            {
                Departamento Consulta = Repositorio.TodosOsDepartamentos.Where((r) => r.Nome == Identificador).First();

                Repositorio.Excluir(Consulta);
                TempData["SuccessMessage"] = "exclusao realizada com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Não é possivel prosseguir com a ação";
                return Redirect("Index");
            }
            

        }

    }
}
