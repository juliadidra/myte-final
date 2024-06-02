
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myte.Models;
using myte.Services;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();



builder.Services.AddScoped<WbsService>();
builder.Services.AddScoped<RegistroHorasService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CriarAcessoService>();
/***** Primeiro bloco unifica os serviços para a aplicação funcionar *****/

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
            });


//1° Adicionar o serviço de string de conexão com o servidor db
builder.Services.AddDbContext<AppDbContext>((options) =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);
//2° Contexto de autenticação
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// 02/06
// Configura uma política de autorização global que exige que todos os usuários estejam autenticados


//-----------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


//3° Adiciona o método que auxilia na aplicação dos processos de autenticação de usuários para área restrita
//UseAuthentication(); é um método
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
