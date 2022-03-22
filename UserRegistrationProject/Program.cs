using UserRegistrationProject.Config;
using UserRegistrationProject.DAL.Repositories.Abstractions;
using UserRegistrationProject.DAL.Repositories.Implementation;
using UserRegistrationProject.Helpers.Abstractions;
using UserRegistrationProject.Helpers.Implementation;
using UserRegistrationProject.Services.Abstraction;
using UserRegistrationProject.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Register", "");
});

builder.Services.Configure<SqlServerConfig>(builder.Configuration.GetSection(nameof(SqlServerConfig)));
builder.Services.Configure<HashingOptions>(builder.Configuration.GetSection(nameof(HashingOptions)));

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();


builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
