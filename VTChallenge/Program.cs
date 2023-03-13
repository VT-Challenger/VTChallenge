using Microsoft.EntityFrameworkCore;
using VTChallenge.Data;
using VTChallenge.Repositories;
using VTChallenge.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddAntiforgery();
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("SqlVtChallengeTaj");

builder.Services.AddTransient<HttpClient>();
builder.Services.AddTransient<IRepositoryVtChallenge, RepositoryVtChallenge>();
builder.Services.AddTransient<IServiceValorant, ServiceValorant>();
builder.Services.AddDbContext<VTChallengeContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Index}/{id?}");

app.Run();
