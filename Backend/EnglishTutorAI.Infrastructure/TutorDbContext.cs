using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure;

public class TutorDbContext(DbContextOptions<TutorDbContext> options) : DbContext(options);