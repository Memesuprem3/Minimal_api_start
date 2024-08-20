using WebApplication_MinimalAPI.Data;
using WebApplication_MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/helloSUT23", () => "Hello SUT23 from minimal Api");

app.MapGet("/helloJesus", (string name) =>

{

    name = "Jesus christus";

    return name;

});

app.MapGet("/api/Coupon",() => 
{
    return Results.Ok(CouponStore.couponList);
});

app.MapGet("/api/Coupon/{id}", (int id) =>
{
return Results.Ok(CouponStore.couponList.FirstOrDefault (c => c.ID == id));
});

app.MapPost("/api/Coupon", (Coupon coupone) => 
{
    if(coupone.ID != 0 || string.IsNullOrEmpty(coupone.Name))
    {
        return Results.BadRequest("invalid id OR name, lol");
    }
    if(CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == coupone.Name.ToLower()) != null)
    {
        return Results.BadRequest("Coupone Name already exists,lol");
    };
    coupone.ID = CouponStore.couponList.OrderByDescending(c => c.ID).FirstOrDefault().ID + 1;
    CouponStore.couponList.Add(coupone);

    return Results.Ok(coupone);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
