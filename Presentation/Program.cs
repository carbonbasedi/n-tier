using Business.Services.Abstract;
using Business.Services.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositiories.Abstract;
using DataAccess.Repositiories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

#region builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
#endregion

#region Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
#endregion

#region Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#region app
var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();
#endregion