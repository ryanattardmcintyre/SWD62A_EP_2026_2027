using Common.Interfaces;
using Common.Models;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services.BookingPolicies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryDbContext>();
builder.Services.AddControllersWithViews();


//Transient - it creates an instance for every call
//Singleton - it creates ONE instance for all users/calls/requests
//Scoped (most commonly used) - it creates ONE instance for every request
//e.g. end-user => /Books/Create => 1) checks whether book exists 2) adds the book

string booksDataSource = builder.Configuration["booksdatasource"].ToString();
string jsonConnectionString = builder.Configuration["jsonConnectionString"].ToString();
if (booksDataSource == "db")
{
    builder.Services.AddScoped<IBooksRepository, BooksRepository>();
}
else
{
    builder.Services.AddScoped<IBooksRepository, BooksJsonRepository>(
        x => new BooksJsonRepository(jsonConnectionString));
}
builder.Services.AddScoped<CategoriesRepository>();
builder.Services.AddScoped<ReservationsRepository>();

builder.Services.AddKeyedScoped<IBookingPolicy, SummerBookingPolicy>("summer");
builder.Services.AddKeyedScoped<IBookingPolicy, WinterBookingPolicy>("winter");


var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
