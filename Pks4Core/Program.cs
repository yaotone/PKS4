using Pks4Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Home/login_process");

builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication("Cookies");
builder.Services.AddDbContext<Pks4Context>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=pks4;Username=postgres;Password=9526", x => x.UseNodaTime()));
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}


app.UseStaticFiles();
app.UseRouting();
app.UseSession();


app.UseAuthorization();
app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "messages",
    pattern: "{controller=Authorized}/{action=messages}/{id?}");


app.Run();



