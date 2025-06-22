using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sports.DTO;
using Sports.Model;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),

        RoleClaimType = "Role",
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmailService>();

builder.Services.AddDbContext<DB>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped<Token>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseStaticFiles(); // مهم ييجي بدري علشان يخدم الصور والـ CSS من wwwroot

app.UseRouting(); // ضروري لتفعيل الـ Endpoints بشكل صحيح

app.UseCors("AllowAll"); // ييجي بعد UseRouting وقبل UseAuthorization

app.UseAuthorization(); // لو فيه Authentication كمان ضيف UseAuthentication قبله

app.UseSwagger();       // ممكن ييجي هنا
app.UseSwaggerUI();     // مع بعضه

app.MapControllers(); // ييجي في الآخر

app.Run();
;
