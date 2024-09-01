using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication_MinimalAPI;
using WebApplication_MinimalAPI.Data;
using WebApplication_MinimalAPI.Models;
using WebApplication_MinimalAPI.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

//app.MapGet("/helloSUT23", () => "Hello SUT23 from minimal Api");

//app.MapGet("/helloJesus", (string name) =>

//{

//    name = "Jesus christus";

//    return name;

//});

app.MapGet("/api/Coupons", () =>
{
    APIRespons response = new APIRespons();
    response.Result = CouponStore.couponList;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;



    return Results.Ok(response);
}).WithName("GetCoupons").Produces(200);

app.MapGet("/api/Coupon/{id}", (int id) =>
{
    APIRespons response = new APIRespons();
    response.Result = CouponStore.couponList.FirstOrDefault(c => c.ID == id);
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;


    return Results.Ok(response);
}).WithName("GetCoupon").Produces<Coupon>(200);

app.MapPost("/api/Coupon", async (IValidator<CouponCreateDTO> validator,IMapper _mapper,CouponCreateDTO coupone_C_DTO) =>
{

    APIRespons response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest};

    var validatorResult = await validator.ValidateAsync(coupone_C_DTO);
    if (!validatorResult.IsValid)
    {
        return Results.BadRequest(response);
    }
    if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == coupone_C_DTO.Name.ToLower()) != null)
    {
        response.ErrorMessages.Add("Coupone Name already Exists");
        return Results.BadRequest(response);
    }

    //Utan AutoMapper
    //Coupon coupoun = new Coupon()
    //{
    //    Name = coupone_C_DTO.Name,
    //    Precent = coupone_C_DTO.Precent,
    //    IsActive = coupone_C_DTO.IsActive
    //};

    //Med AutoMapper
    Coupon coupon = _mapper.Map<Coupon>(coupone_C_DTO);
    coupon.ID = CouponStore.couponList.OrderByDescending(c => c.ID).FirstOrDefault().ID + 1;
    CouponStore.couponList.Add(coupon);

    CouponDTO couponDto = _mapper.Map<CouponDTO>(coupon);
    response.Result = couponDto;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;

    return Results.Ok(response);
}).WithName("CreateCoupon").Produces<CouponCreateDTO>(201).Accepts<APIRespons>("application/json").Produces(400);


app.MapPut("/api/coupon", async (IMapper _mapper, IValidator<CouponUpdateDTO> _validator, CouponUpdateDTO coupon_U_DTO) =>
{
    APIRespons response = new() { IsSuccess =false, StatusCode = System.Net.HttpStatusCode.BadRequest };
    //Add Validation
    var validateResult = await _validator.ValidateAsync(coupon_U_DTO);
    if (!validateResult.IsValid)
    {
        response.ErrorMessages.Add(validateResult.Errors.FirstOrDefault().ToString());
    }

    Coupon couponFromeStore = CouponStore.couponList.FirstOrDefault(c => c.ID == coupon_U_DTO.ID);
    couponFromeStore.IsActive = coupon_U_DTO.IsActive;
    couponFromeStore.Name = coupon_U_DTO.Name;
    couponFromeStore.Precent = coupon_U_DTO.Precent;
    couponFromeStore.LastUpdate = DateTime.Now;

    //AutoMapper

    Coupon coupone = _mapper.Map<Coupon>(coupon_U_DTO);

    response.Result = _mapper.Map<CouponDTO>(couponFromeStore);
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;
    
    return Results.Ok(response);

}).WithName("UpdateCoupone").Accepts<CouponUpdateDTO>("application/json").Produces<CouponUpdateDTO>(200).Produces(400);

app.MapDelete("api/coupon/{id:int}", (int id) =>
{
    //söka och träffa
    APIRespons response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };


    Coupon couponFromStore = CouponStore.couponList.FirstOrDefault(c => c.ID == id);

    if (couponFromStore != null)
    {
        CouponStore.couponList.Remove(couponFromStore);
        response.IsSuccess = true;
        response.StatusCode=System.Net.HttpStatusCode.Continue;
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid ID,kek");
        return Results.BadRequest(response);
    }
}).WithName("DeleteCoupon");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
// slutade vid 01:12:03