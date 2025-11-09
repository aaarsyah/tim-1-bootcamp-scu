using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using MyApp.BlazorUI;
using MyApp.BlazorUI.Components; 
using MyApp.BlazorUI.Services; 



var builder = WebApplication.CreateBuilder(args);

// TODO: Pasang license key (gratis) untuk AutoMapper dan MediatR
builder.Logging.AddFilter("LuckyPennySoftware.AutoMapper.License", LogLevel.None);
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();


// Configure HttpClient for API calls
builder.Services.AddHttpClient("WebAPI", sp =>
{
    sp.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7269");
});

// Add navigation service
builder.Services.AddScoped<NavigationManagerExt>();

//Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IAdminService, AdminService>();

//Auth
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMyClassService, MyClassService>();

builder.Services.AddScoped<ICourseService, CourseService>();

// Error Handling
builder.Services.AddScoped<ErrorService, ErrorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for Courseion scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
