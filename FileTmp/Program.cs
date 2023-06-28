using System.Text;
using FileTmp.Data;
using FileTmp.Models;
using FileTmp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddCors();
builder.Services.AddSwaggerDocument();

builder.Services.AddTransient<ProductService>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});



builder.Services.AddIdentity<TestUser, IdentityRole>(opt => {
    opt.User.RequireUniqueEmail = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});



var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();

    app.UseOpenApi(); // serve OpenAPI/Swagger documents
    app.UseSwaggerUi3(settings =>
    {
        settings.DocumentPath = "/swagger/v1/swagger.json"; // Swagger JSON endpoint
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
