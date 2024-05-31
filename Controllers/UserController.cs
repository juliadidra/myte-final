using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myte.Models;

namespace myte.Controllers
{
    //Operações CRUD do Cadastro de dados
    public class UserController : Controller
    {
        /* 1° Movimento Definição dos elementos lógicos e DI para operações de dados
        ============================================================================*/

        //1° Passo: Elemento lógico referencial (private) | para manipular os dados da db
        //O UserManager<> oferece esse recurso.

        public IActionResult Create()
        {
            return View();
        }
    
    }
}
