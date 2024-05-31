using myte.Models;

namespace myte.Services
{
    public class RegistroHorasService
    {
        HttpClient _httpClient;

        public RegistroHorasService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }

        public async Task<List<RegistroHoras>> GetRegistroHorasAsync()
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<List<RegistroHoras>>($"/api/RegistroHoras/GetAll");
            return apiResposta;
        }

        public async Task<RegistroHoras> GetRegistroHorasByIdAsync(int id)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<RegistroHoras>($"/api/RegistroHoras/GetOne/{id}");
            return apiResposta;
        }

        public async Task<RegistroHoras> AddRegistroHorasAsync(RegistroHoras registroHoras)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/RegistroHoras/PostHoras", registroHoras);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<RegistroHoras>();
        }

        public async Task<RegistroHoras> UpdateRegistroHorasAsync(int id, RegistroHoras registroHoras)
        {
            var apiResposta = await _httpClient.PutAsJsonAsync($"/api/RegistroHoras/PutHoras/{id}", registroHoras);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<RegistroHoras>();
        }

        public async Task<List<Wbs>> GetAllWbsAsync()
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<List<Wbs>>("/api/Wbs/GetAll");
            return apiResposta;
        }
    }
}
