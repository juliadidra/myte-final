using Microsoft.AspNetCore.Authorization;
using myte.Models;

namespace myte.Services
{
    public class FuncionarioService
    {
        private HttpClient _httpClient;

        public FuncionarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }

        //lista todos os funcionario
        public async Task<List<Funcionario>> GetAllFuncionarioAsync()
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<List<Funcionario>>("/api/Funcionario/GetAll");
            return apiResposta;
        }

        //resgata apenas 1 Funcionario
        public async Task<Funcionario> GetFuncionarioByIdAsync(string email)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<Funcionario>($"/api/Funcionario/GetOne/{email}");
            return apiResposta;
        }

        //Cria um Funcionario

        public async Task<Funcionario> AddFuncionarioAsync(Funcionario funcionario)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/Funcionario/PostFuncionario", funcionario);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<Funcionario>(); //desserializando
        }

        //Atualiza um Funcionario

        public async Task<Funcionario> UpdateFuncionarioAsync(string email, Funcionario funcionario)
        {
            var apiResposta = await _httpClient.PutAsJsonAsync($"/api/Funcionario/PutFuncionario/{email}", funcionario);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<Funcionario>();
        }

        //Exclui um departamento

        public async Task DeleteFuncionarioAsync(string email)
        {
            var apiResposta = await _httpClient.DeleteAsync($"/api/Funcionario/Delete/{email}");
            apiResposta.EnsureSuccessStatusCode();
        }
    }
}
