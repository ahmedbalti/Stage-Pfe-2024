using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Gestion_User.Controllers;
using User.Gestion.Service.Services;
using User.Gestion.Data.Models;
using System;
using System.Collections.Generic;


//namespace Gestion_User.Tests;

public class TicketBenchmark
{
    private TicketController _controller;

    [GlobalSetup]
    public void Setup()
    {
        // Setup des dépendances (exemple simplifié)
        var services = new ServiceCollection();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IEmailService, EmailService>(); // Si nécessaire

        var provider = services.BuildServiceProvider();
        var ticketService = provider.GetRequiredService<ITicketService>();
        var emailService = provider.GetRequiredService<IEmailService>();

        // Initialisation du contrôleur avec les services mockés ou réels
        _controller = new TicketController(ticketService, emailService);
    }

    [Benchmark]
    public async Task TestGetTicketsPerformance()
    {
        try
        {
            var result = await _controller.GetTickets();
            if (result.Result is OkObjectResult okResult)
            {
                var tickets = okResult.Value as IEnumerable<Ticket>;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in TestGetTicketsPerformance: {ex.Message}");
        }
    }


    [Benchmark]
    public async Task TestAddTicketPerformance()
    {
        var ticketDto = new TicketDTO
        {
            Titre = TicketTitle.DemandeAide,  // Enum example
            Description = "Test description",
            Priority = TicketPriority.Moyenne,
            Statut = TicketStatut.Nouveau
        };

        var result = await _controller.AddTicket(ticketDto);
    }

    [Benchmark]
    public async Task TestUpdateTicketPerformance()
    {
        var ticketDto = new TicketDTO
        {
            Titre = TicketTitle.Reclamation,
            Description = "Updated description",
            Priority = TicketPriority.Haute,
            Statut = TicketStatut.Resolu,
            ResolutionDate = DateTime.UtcNow
        };

        var ticketId = Guid.NewGuid();  // Remplace avec un ticket ID valide si possible
        var result = await _controller.UpdateTicket(ticketId, ticketDto);
    }

    [Benchmark]
    public async Task TestGetAllTicketsPerformance()
    {
        var result = await _controller.GetAllTickets();
    }
}

