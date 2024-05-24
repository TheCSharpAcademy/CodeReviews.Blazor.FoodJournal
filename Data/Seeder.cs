using FoodJournal.Models;

namespace FoodJournal.Data;

public class Seeder
{
    private FoodJournalDbContext _context;

    public Seeder(FoodJournalDbContext context)
    {
        _context = context;
    }

    public async Task SeedDb()
    {
        var random = new Random();

        List<Food> foodsToSeed = new List<Food>();
        List<Meal> mealsToSeed = new List<Meal>();
        if (!_context.Foods.Any())
        {


            var food = new Food()
            {
                Name = "Soup",
                NutritionType = Enums.Nutrition.Drink,
                Calories = 150,
                Description = "Hot",
                IsToAvoid = false
            };
            foodsToSeed.Add(food);

            var food2 = new Food()
            {
                Name = "Chicken",
                NutritionType = Enums.Nutrition.WhiteMeat,
                Calories = 180,
                Description = "Roasted",
                IsToAvoid = false
            };
            foodsToSeed.Add(food2);

            var food3 = new Food()
            {
                Name = "Meatball",
                NutritionType = Enums.Nutrition.RedMeat,
                Calories = 350,
                Description = "Barbeque",
                IsToAvoid = false
            };
            foodsToSeed.Add(food3);

            var food4 = new Food()
            {
                Name = "Baklava",
                NutritionType = Enums.Nutrition.Dessert,
                Calories = 650,
                Description = "Best",
                IsToAvoid = true
            };
            foodsToSeed.Add(food4);

            await _context.Foods.AddRangeAsync(foodsToSeed);
            await _context.SaveChangesAsync();
        }
        else
            foodsToSeed = _context.Foods.ToList();

        if (!_context.Meals.Any())
        {
            var meal = new Meal()
            {
                Date = DateTime.Now - TimeSpan.FromDays(random.Next(1, 20)),
                IsFavorite = true,
                Foods = new List<Food>() { foodsToSeed[0] }
            };
            mealsToSeed.Add(meal);

            var meal2 = new Meal()
            {
                Date = DateTime.Now - TimeSpan.FromDays(random.Next(1, 20)),
                IsFavorite = false,
                Foods = new List<Food>() { foodsToSeed[1] }
            };
            mealsToSeed.Add(meal2);

            var meal3 = new Meal()
            {
                Date = DateTime.Now - TimeSpan.FromDays(random.Next(1, 20)),
                IsFavorite = false,
                Foods = new List<Food>() { foodsToSeed[2] }
            };
            mealsToSeed.Add(meal3);

            var meal4 = new Meal()
            {
                Date = DateTime.Now - TimeSpan.FromDays(random.Next(1, 20)),
                IsFavorite = false,
                Foods = new List<Food>() { foodsToSeed[3] }
            };
            mealsToSeed.Add(meal4);

            var meal5 = new Meal()
            {
                Date = DateTime.Now - TimeSpan.FromDays(random.Next(1, 20)),
                IsFavorite = false,
                Foods = new List<Food>()
            };
            meal5.Foods = foodsToSeed;
            mealsToSeed.Add(meal5);

            await _context.AddRangeAsync(mealsToSeed);
            await _context.SaveChangesAsync();
        }

    }
}
