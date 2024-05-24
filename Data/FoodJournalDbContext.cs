using FoodJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Data;

public class FoodJournalDbContext : DbContext
{
    public FoodJournalDbContext(DbContextOptions<FoodJournalDbContext> options) : base(options) { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Meal> Meals { get; set; }

}
