using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using HamburgueriaBlazor.Components;
using HamburgueriaBlazor.Components.Account;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository;
using HamburgueriaBlazor.Repository.IRepository;
using HamburgueriaBlazor.Services;
using System;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Enable Blazor Server detailed errors for development
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<SharedStateService>();

// --- Adicionado: serviços de pagamento, frete, DTOs e exceções ---
builder.Services.AddScoped<HamburgueriaBlazor.Services.Payments.IPaymentService, HamburgueriaBlazor.Services.Payments.PagamentoPixService>();
builder.Services.AddScoped<HamburgueriaBlazor.Services.Payments.PagamentoCartaoService>();
builder.Services.AddScoped<HamburgueriaBlazor.Services.Frete.ICalculadoraFrete, HamburgueriaBlazor.Services.Frete.FretePadrao>();
// Registre FreteExpresso quando quiser usar frete expresso:
// builder.Services.AddScoped<ICalculadoraFrete, FreteExpresso>();


builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddRadzenComponents();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// Make sure authorization services are registered
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Read retry settings from configuration (if present)
var retrySection = builder.Configuration.GetSection("ExtensionSettings").GetSection("RetryPolicy");
int maxRetryCount = retrySection.GetValue<int?>("MaxRetryCount") ?? 5;
int maxDelayMs = retrySection.GetValue<int?>("MaxDelay") ?? 100;
var errorNumbersToAdd = retrySection.GetSection("ErrorNumbersToAdd").Get<int[]>() ?? Array.Empty<int>();

// Use DbContextFactory to create DbContext instances on demand (prevents concurrency issues in Blazor Server)
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(maxRetryCount, TimeSpan.FromMilliseconds(maxDelayMs), errorNumbersToAdd)));

// Provide a scoped ApplicationDbContext created from the factory so Identity and other scoped consumers can still get a scoped DbContext
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // show developer exception page in development to see server exceptions
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// Ensure routing and authentication/authorization middleware are enabled
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
