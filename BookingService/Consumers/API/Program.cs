using Application.BookingApplication;
using Application.BookingApplication.Ports;
using Application.GuestApplication;
using Application.GuestApplication.Ports;
using Application.Room;
using Application.Room.Ports;
using Data;
using Data.GuestData;
using Data.RoomData;
using Data.BookingData;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Ports;
using Domain.RoomDomain.Ports;
using Microsoft.EntityFrameworkCore;
using Application.MercadoPago;
using Application.PaymentApplication.Ports;
using System.Text.Json.Serialization;
using Payment.Application;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(BookingManager));

#region IoC
builder.Services.AddScoped<IGuestManeger, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();
#endregion IoC

#region DB wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(
    options => options.UseSqlServer(connectionString));
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();