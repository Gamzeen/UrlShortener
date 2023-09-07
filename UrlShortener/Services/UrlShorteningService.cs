using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Services;

public class UrlShorteningService
{
    public const int NumberIfCharsInShortLink = 7;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvqxyz0123456789";

    private readonly Random _random = new Random();
    private readonly ApplicationDbContext _dbContext;

    public UrlShorteningService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateUniqueCode()
    {

        var codeChars = new char[NumberIfCharsInShortLink];
        while (true)
        {
            for (var i = 0; i < NumberIfCharsInShortLink; i++)
            {
                var randomIndex = _random.Next(Alphabet.Length - 1);
                codeChars[i] = Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (! await _dbContext.ShortenedUrls.AnyAsync(s=>s.Code ==code))
            {
                return code;
            }
        }
    }
}