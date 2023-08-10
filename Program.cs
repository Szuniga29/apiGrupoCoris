using apiGrupoCoris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniValidation;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<apiGrupoCorisDbContext>(options => { options.UseSqlServer("name=ConnectionStrings:DefaultConnection"); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/clientes", async (apiGrupoCorisDbContext context) =>
{
    try
    {
        if (await context.Ventas.ToListAsync() != null)
        {
            var canales = await context.Ventas
            .Select(v => v.Cliente)
            .Distinct()
            .ToListAsync();
            return Results.Ok(canales);
        }

        return Results.BadRequest();
    }
    catch (Exception ex)
    {

        return Results.Json(new { codigo = 500, mensaje = ex.Message }, statusCode: StatusCodes.Status500InternalServerError);
    }

});

app.MapGet("/ventaPorCodigo/{id}/{cliente}", async (string id, string cliente, apiGrupoCorisDbContext context) =>
{
    try
    {
        var venta = await context.Ventas.FirstOrDefaultAsync(v => v.Id == id && v.Cliente == cliente);

        if (venta != null)
        {
            return Results.Ok(venta);
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        return Results.Json(new { codigo = 500, mensaje = ex.Message }, statusCode: StatusCodes.Status500InternalServerError);
    }
});


app.MapPost("/añadirNuevo", async ([FromBody] Venta ventas, apiGrupoCorisDbContext context) =>
{
try
{
    if (ventas != null)
    {
        bool existeRegistro = await context.Ventas.AnyAsync(v =>
            v.Cliente == ventas.Cliente &&
            v.Marca == ventas.Marca &&
            v.Descripcion == ventas.Descripcion);

            if (existeRegistro)
            {
                return Results.Conflict(); 
            }

            ventas.PrecioxGr =  ventas.Precio / (decimal)ventas.Gramaje;
            ventas.FechaIngresa = DateTime.Now;
            ventas.Id = null;

            await context.Ventas.AddAsync(ventas);
            await context.SaveChangesAsync();
            return Results.Ok(ventas);
        }

        return Results.Conflict();
    }
    catch (Exception ex)
    {
        return Results.Json(new { codigo = 500, mensaje = ex.Message }, statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.MapPut("/editar", async (Venta venta, apiGrupoCorisDbContext context) =>
{
    var producto = await context.Ventas.FindAsync(venta.Id);
    try
    {
        venta.FechaIngresa = DateTime.Now;
        if (producto is null)
        {
            return Results.NotFound();
        }

        producto.Gramaje = venta.Gramaje;
        producto.Precio = venta.Precio;
        producto.PrecioxGr = producto.Precio / (decimal)producto.Gramaje;
        producto.FechaIngresa = venta.FechaIngresa;

        await context.SaveChangesAsync();

        return Results.Ok(producto);
    }
    catch (Exception ex)
    {
        return Results.Json(new { codigo = 500, mensaje = ex.Message }, statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.Run();
