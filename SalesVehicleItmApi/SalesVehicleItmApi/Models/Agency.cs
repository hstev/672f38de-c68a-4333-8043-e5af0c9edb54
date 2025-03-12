using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using SalesVehicleItmApi.Models;

namespace SalesVehicleItmApi.Models;

public partial class Agency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}


public static class AgencyEndpoints
{
	public static void MapAgencyEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Agency").WithTags(nameof(Agency));

        group.MapGet("/", async (VehicleSalesDbContext db) =>
        {
            return await db.Agencies.ToListAsync();
        })
        .WithName("GetAllAgencies")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Agency>, NotFound>> (int id, VehicleSalesDbContext db) =>
        {
            return await db.Agencies.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Agency model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAgencyById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Agency agency, VehicleSalesDbContext db) =>
        {
            var affected = await db.Agencies
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, agency.Id)
                  .SetProperty(m => m.Name, agency.Name)
                  .SetProperty(m => m.Location, agency.Location)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAgency")
        .WithOpenApi();

        group.MapPost("/", async (Agency agency, VehicleSalesDbContext db) =>
        {
            db.Agencies.Add(agency);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Agency/{agency.Id}",agency);
        })
        .WithName("CreateAgency")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VehicleSalesDbContext db) =>
        {
            var affected = await db.Agencies
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAgency")
        .WithOpenApi();
    }
}