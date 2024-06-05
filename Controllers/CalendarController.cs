using Microsoft.AspNetCore.Mvc;
using myte.Models;
using myte.Services;

namespace myte.Controllers
{
    public class CalendarController : Controller
    {
        private RegistroHorasService _registroHorasService;
        private WbsService _wbsService;

        public CalendarController(RegistroHorasService registroHorasService, WbsService wbsService)
        {
            _registroHorasService = registroHorasService;
            _wbsService = wbsService;
        }


        public async Task<IActionResult> Index()
        {
            string email = TempData.Peek("EmailUsuario") as string;
            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "Não é possivel recuperar o email"; // Use o email padrão, substitua conforme necessário
            }

            ViewBag.Email = email;

            var wbsList = await _wbsService.GetAllWbsAsync();
            var registroHorasList = (await _registroHorasService.GetRegistroHorasAsync()).Where(r => r.Funcionario_Email == email).ToList();

            var model = new CalendarPageViewModel
            {
                WbsList = wbsList,
                RegistroHorasList = registroHorasList
            };

            ViewBag.Email = email;
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string email = TempData.Peek("EmailUsuario") as string;
            ViewBag.Email = email;
            ViewBag.WbsList = await _wbsService.GetAllWbsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistroHoras registroHoras)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _registroHorasService.AddRegistroHorasAsync(registroHoras);
                    TempData["SuccessMessage"] = "Hora cadastrada com sucesso!";
                    return RedirectToAction(nameof(Index), new { email = registroHoras.Funcionario_Email });
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    ModelState.AddModelError(string.Empty, "Erro ao enviar os dados para a API.");
                }
            }

            ViewBag.WbsList = await _wbsService.GetAllWbsAsync();
            ViewBag.Email = registroHoras.Funcionario_Email;
            return View(registroHoras);
        }

        private List<CalendarViewModel> GetCalendarDays(DateTime date, int quinzena, List<RegistroHoras> registroHorasList)
        {
            var days = new List<CalendarViewModel>();
            var year = date.Year;
            var month = date.Month;
            var startDay = quinzena == 1 ? 1 : 16;
            var endDay = quinzena == 1 ? 15 : DateTime.DaysInMonth(year, month);

            for (int day = startDay; day <= endDay; day++)
            {
                var dateTime = new DateTime(year, month, day);
                var dateOnly = DateOnly.FromDateTime(dateTime); // Converte DateTime para DateOnly
                var registro = registroHorasList.FirstOrDefault(r => r.Dia == dateOnly); // Compara DateOnly diretamente
                days.Add(new CalendarViewModel
                {
                    Day = day,
                    DayOfWeek = dateTime.DayOfWeek.ToString(),
                    Hours = registro?.Horas ?? 0,
                    WbsCodigo = registro?.WBS_Codigo
                });
            }

            return days;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllWbs()
        {
            var wbsList = await _wbsService.GetAllWbsAsync();
            return Json(wbsList);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateRegistroHoras(int id)
        {

            try
            {
                string email = TempData.Peek("EmailUsuario") as string;
                var registroHoras = await _registroHorasService.GetRegistroHorasByIdAsync(id);
                if (registroHoras != null)
                {
                    ViewBag.WbsList = await _wbsService.GetAllWbsAsync();
                    return View(registroHoras);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Não é possivel editar, um registro que ainda não foi criado.";

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRegistroHoras(int id, RegistroHoras registroHoras)
        {
            if (id != registroHoras.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _registroHorasService.UpdateRegistroHorasAsync(id, registroHoras);
                    return RedirectToAction(nameof(Index), new { email = registroHoras.Funcionario_Email });
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    ModelState.AddModelError(string.Empty, "Erro ao enviar os dados para a API.");
                }
            }

            ViewBag.WbsList = await _wbsService.GetAllWbsAsync();
            return View(registroHoras);
        }

        /*[HttpDelete]
        public async Task<IActionResult> DeleteRegistroHoras(int id)
        {
            var result = await _registroHorasService.DeleteRegistroHorasAsync(id);
            return Json(result);
        }*/
    }
}