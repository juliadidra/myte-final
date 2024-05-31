using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using myte.Models;

namespace myte.Services
{
    public class WbsService
    {
        private HttpClient _httpClient;

        public WbsService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }

        //lista todas as WBS
        public async Task<List<Wbs>> GetAllWbsAsync()
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<List<Wbs>>($"/api/WBS/GetAll");
            return apiResposta;
        }

        //resgata apenas 1 WBS

        public async Task<Wbs> GetWbsByIdAsync(string codigo)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<Wbs>($"/api/WBS/GetOne/{codigo}");
            return apiResposta;
        }

        //Cria uma WBS

        public async Task<Wbs> AddWbsAsync(Wbs wbs)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/WBS/PostWBS", wbs);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<Wbs>(); //desserializando
        }

        //Atualiza uma WBS

        public async Task<Wbs> UpdateWbsAsync(string codigo, Wbs wbs)
        {
            var apiResposta = await _httpClient.PutAsJsonAsync($"/api/WBS/PutWBS/{codigo}", wbs);
            apiResposta.EnsureSuccessStatusCode() ;

            return await apiResposta.Content.ReadFromJsonAsync<Wbs>();
        }

        //Exclui uma WBS

        public async Task DeleteWbsAsync(string codigo)
        {
            var apiResposta = await _httpClient.DeleteAsync($"/api/WBS/DeleteWBS/{codigo}");
            apiResposta.EnsureSuccessStatusCode();
        }
    }
}
