using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Gestion_User.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using User.Gestion.Service.Services;
using User.Gestion.Data.Models;
using Gestion_User.Models.Authentication.Login;
using Gestion_User.Models.Authentication.SignUp;
namespace Gestion_User.Tests;

public class AuthenticationBenchmark
{
    private AuthenticationController _controller;

    [GlobalSetup]
    public void Setup()
    {
        // Setup des dépendances nécessaires (exemple simplifié)
        var services = new ServiceCollection();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddDefaultTokenProviders();

        var provider = services.BuildServiceProvider();
        var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        var signInManager = provider.GetRequiredService<SignInManager<ApplicationUser>>();
        var emailService = provider.GetRequiredService<IEmailService>();
        var userManagement = provider.GetRequiredService<IUserManagement>();
        var configuration = new ConfigurationBuilder().Build();

        _controller = new AuthenticationController(userManager, emailService, roleManager, userManagement, configuration, signInManager);
    }

    [Benchmark]
    public async Task TestRegisterUserPerformance()
    {
        var registerUser = new RegisterUser
        {
            Username = "testuser",
            Email = "testuser@example.com",
            Password = "Test@1234",
            Roles = new List<string> { "User" }
        };

        var result = await _controller.Register(registerUser);
    }

    [Benchmark]
    public async Task TestLoginPerformance()
    {
        var loginModel = new LoginModel
        {
            Username = "testuser",
            Password = "Test@1234"
        };

        var result = await _controller.Login(loginModel);
    }


}


