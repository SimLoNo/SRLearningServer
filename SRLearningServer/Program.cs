using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Account;
using SRLearningServer.Data;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Repositories;
using System.Text.Json.Serialization;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Services;
using SRLearningServer.Components.Converters;
using SRLearningServer.Components.Interfaces.Converters;
using Microsoft.AspNetCore.Components;
using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.FrontendServices;
using Radzen;
using SRLearningServer.Components.Interfaces.Utilities;
using SRLearningServer.Components.Utilities;
using Microsoft.AspNetCore.Authorization;
using SRLearningServer.Components.Authorization;

using Azure.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Azure;
using System.Net.Http.Headers;
using System.Net.Http;


var builder = WebApplication.CreateBuilder(args);

var BlobStorageUri = builder.Configuration["AzureURIs:BlobStorage"];
var KeyVaultURI = builder.Configuration["AzureURIs:KeyVault"];
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
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

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var SRConnectionString = builder.Configuration.GetConnectionString("SrConnection") ?? throw new InvalidOperationException("Connection string 'SrConnection' not found.");
builder.Services.AddDbContext<SRContext>(options =>
    options.UseSqlServer(SRConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAzureClientsCore();

/*builder.Services.AddDataProtection()
                .PersistKeysToAzureBlobStorage(new Uri(BlobStorageUri),
                                                new DefaultAzureCredential())
                .ProtectKeysWithAzureKeyVault(new Uri(KeyVaultURI),
                                                new DefaultAzureCredential());*/

builder.Services.AddTransient<IEmailSender<ApplicationUser>, EmailSenderUtility>();
builder.Services.AddTransient<IBaseEmailSender, BaseEmailSenderUtility>();
builder.Services.Configure<AuthMessageSenderOptionsUtility>(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options => {
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
    options.SlidingExpiration = true;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(3));
/*builder.Services.AddDbContext<SRContext>(options =>
    options.UseSqlServer(SRConnectionString));*/

builder.Services
    .AddScoped<ITypeCategoryListRepository, TypeCategoryListRepository>()
    .AddScoped<ITypeCategoryListService, TypeCategoryListService>()
    .AddScoped<ITypeRepository, TypeRepository>()
    .AddScoped<ICardRepository, CardRepository>()
    .AddScoped<IAttachmentRepository, AttachmentRepository>()
    .AddScoped<IResultRepository, ResultRepository>()
    .AddScoped<ITypeService, TypeService>()
    .AddScoped<ICardService, CardService>()
    .AddScoped<IAttachmentService, AttachmentService>()
    .AddScoped<IResultService, ResultService>()
    .AddScoped<IFrontendAttachmentService, FrontendAttachmentService>()
    .AddScoped<IFrontendCardService, FrontendCardService>()
    .AddScoped<IFrontendResultService, FrontendResultService>()
    .AddScoped<IFrontendTypeCategoryListService, FrontendTypeCategoryListService>()
    .AddScoped<IFrontendTypeService, FrontendTypeService>()
    .AddScoped<IDomainToDtoConverter, DomainToDtoConverter>()
    .AddScoped<IDtoToDomainConverter, DtoToDomainConverter>()
    .AddScoped<INotificationUtility, NotificationUtility>()
    .AddScoped<IActiveCheckService, ActiveCheckService>()
    .AddScoped<IReportUtility, ReportUtility>()
    .AddScoped<IActivitySortingUtility, ActivitySortingUtility>()
    .AddSingleton<IAuthorizationHandler, IsAdminAuthorizationHandler>();

// Register HttpClient with the base address of the application
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    var httpClient = new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
    var authStateProvider = sp.GetRequiredService<AuthenticationStateProvider>();
    var authState = authStateProvider.GetAuthenticationStateAsync().Result;
    var user = authState.User;

    if (user.Identity.IsAuthenticated)
    {
        var token = user.FindFirst("access_token")?.Value;
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    return httpClient;
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

/*builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});*/

/*string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
userFolder = Path.Combine(userFolder, ".aspnet");
userFolder = Path.Combine(userFolder, "https");
userFolder = Path.Combine(userFolder, "srlearning.pfx");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificates:path").Value = userFolder;

string kestrelCertPassword = builder.Configuration.GetValue<string>("KestrelCertPassword");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificates:password").Value = kestrelCertPassword;*/

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.MapControllers(); // Ensure that the controllers are mapped


app.Run();
