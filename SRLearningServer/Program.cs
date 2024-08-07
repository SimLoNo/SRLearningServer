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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();



builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var SRConnectionString = builder.Configuration.GetConnectionString("SrConnection") ?? throw new InvalidOperationException("Connection string 'SrConnection' not found.");
builder.Services.AddDbContext<SRContext>(options =>
    options.UseSqlServer(SRConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

/*builder.Services.AddDbContext<SRContext>(options =>
    options.UseSqlServer(SRConnectionString));*/

builder.Services
    .AddScoped<ITypeRepository, TypeRepository>()
    .AddScoped<ICardRepository, CardRepository>()
    .AddScoped<IAttachmentRepository, AttachmentRepository>()
    .AddScoped<IResultRepository, ResultRepository>()
    .AddScoped<ITypeService, TypeService>()
    .AddScoped<ICardService, CardService>()
    .AddScoped<IAttachmentService, AttachmentService>()
    .AddScoped<IResultService, ResultService>()
    .AddScoped<IDomainToDtoConverter, DomainToDtoConverter>()
    .AddScoped<IDtoToDomainConverter, DtoToDomainConverter>();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
