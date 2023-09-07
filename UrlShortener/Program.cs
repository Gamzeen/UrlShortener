using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Entities;
using UrlShortener.Extensions;
using UrlShortener.Models;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UrlShorteningService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapPost("api/shorten", async (
    ShortenerUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationDbContext dbContext,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url,UriKind.Absolute,out _))
    {
        return Results.BadRequest("The specified URL is invalid.");
    }

    var code = await urlShorteningService.GenerateUniqueCode();

    var shotenedUrl = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        Code = code,
        CreatedOnUtc = DateTime.Now
    };
    dbContext.ShortenedUrls.Add(shotenedUrl);
    dbContext.SaveChangesAsync();

    return Results.Ok(shotenedUrl.ShortUrl);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
