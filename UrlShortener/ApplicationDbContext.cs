using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Services;

namespace UrlShortener;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions options) :base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberIfCharsInShortLink);
            builder.HasIndex(s => s.Code).IsUnique();

        });
    }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
}