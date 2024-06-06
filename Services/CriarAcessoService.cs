using Microsoft.AspNetCore.Mvc;
using myte.Models;


namespace myte.Services
{
    public class CriarAcessoService  
    {

        private HttpClient _httpClient;

        public CriarAcessoService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }



        public async Task<User> AddAcessoAsync(User user)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/Admin/Post/PostUser", user);
            apiResposta.EnsureSuccessStatusCode();
            return await apiResposta.Content.ReadFromJsonAsync<User>();

        }

        public async Task<User> GetAcessoAsync(string email)
        {
            var apiResposta = await _httpClient.GetFromJsonAsync<User>($"/api/Admin/GetOne/GetOne/{email}");

            return apiResposta;
        }



        public async Task DeleteAcessoAsync(string email)
        {
            var apiResposta = await _httpClient.DeleteAsync($"/api/Admin/Delete/Delete/{email}");

            apiResposta.EnsureSuccessStatusCode();
        }
    }
}
