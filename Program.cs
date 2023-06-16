// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// CLASSE BASE PARA FUNCIONAMENTO DO PROGRAMA (CORAÇÃO)
// O PRÓPRIO PROGRAMA EM SI

// ÁREA DOS USINGS/IMPORTS
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebRPGCreation.Data;

// NAMESPACE É O NOME DO PRÓPRIO PROGRAMA
namespace WebRPGCreation    
{
    // CLASSE PRINCIPAL
    public class Program
    {
        // FOI CRIADO ESTE MAIN COM INTUITO DE AGILIZAR PROCESSOS E FACILITAR A CRIAÇÃO DAS ROLES
        // E PRINCIPALMENTE DA CONTA DE GAME MASTER
        public static async Task Main(string[] args)
        {
            // VARIÁVEL ALICERCE PARA CONTRUÇÃO DO PROGRAMA
            var builder = WebApplication.CreateBuilder(args);

            // PARA ADICIONAR SERVIÇOS AO CONTAINER (CONEXÃO COM BASE DE DADOS)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // CONFIGURAÇÃO DO SERVIÇO PRINCIPAL PARA BASE DE DADOS
            // NESTE CASO SQL SERVER
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // CONFIGURAÇÃO DO SERVIÇO PRINCIPAL PARA TRABALHAR COM O IDENTITY
            // SERVE AINDA PARA CONFIRMAÇÃO DE CONTA E TRABALHAR COM ROLES
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // CONFIGURA O HTTP REQUEST PIPELINE.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ADICIONADO PARA SE TRABALHAR COM IDENTITY (LOGINS E ETC)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // USADO A PRIMEIRA VEZ PARA CRIAÇÃO DAS ROLES
            // ESTÁ COMENTADO POIS NÃO HÁ MAIS NECESSIDADE DE RECRIÁ-LOS

            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //    var roles = new[] { "GM", "Jogador" };

            //    foreach (var role in roles)
            //    {
            //        if (!await roleManager.RoleExistsAsync(role))
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //    }
            //}

            // FUNDAMENTAL PARA CRIAÇÃO DO UTILIZADOR ADMIN, NESTE CASO GAME MASTER
            // HÁ VERIFICAÇÃO SE O MESMO JÁ EXISTE E CASO NÃO EXISTA ELE É CRIADO (TODA VEZ QUE RODA O PROGRAMA)
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "rodrigo@gm.com";
                string password = "123.Abc";

                if (await  userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;

                    await userManager.CreateAsync(user, password);

                    // ATRIBUIÇÃO DA ROLE GM (ADMIN)
                    await userManager.AddToRoleAsync(user, "GM");
                }
            }

            app.Run();
        }
    }
}