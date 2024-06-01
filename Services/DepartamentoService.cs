using myte.Models;

namespace myte.Services
{
    public class DepartamentoService
    {

        private HttpClient _httpClient;

        public DepartamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }

        //lista todos os departamentos
        public async Task<List<Departamento>> GetAllDepartamentosAsync()
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<List<Departamento>>("/api/Departamento/GetAll");
            return apiResposta;
        }

        //resgata apenas 1 departamento

        public async Task<Departamento> GetDepartamentoByIdAsync(int id)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<Departamento>($"/api/Departamento/GetOne/{id}");
            return apiResposta;
        }

        //Cria um departamento

        public async Task<Departamento> AddDepartamento(Departamento departamento)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/Departamento/AddDepartamento", departamento);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<Departamento>(); //desserializando
        }

        //Atualiza um departamento

        public async Task<Departamento> UpdateDepartamentoAsync(int id, Departamento departamento)
        {
            var apiResposta = await _httpClient.PutAsJsonAsync($"/api/Departamento/UpdateDepartamento/{id}", departamento);
            apiResposta.EnsureSuccessStatusCode();

            return await apiResposta.Content.ReadFromJsonAsync<Departamento>();
        }

        //Exclui um departamento

        public async Task DeleteDepartamentoAsync(int id)
        {
            var apiResposta = await _httpClient.DeleteAsync($"/api/Departamento/DeletarDepartamento/{id}");
            apiResposta.EnsureSuccessStatusCode();
        }
    }
}
