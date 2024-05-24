using FoodJournal.Models;
using FoodJournal.Repositories;

namespace FoodJournal.Services;

public class StatsService
{
    private readonly IMealRepository _mealRepository;

    public StatsService(IMealRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }

    public async Task<Stats> GetStats(DateTime start, DateTime end)
    {
        var meals = await _mealRepository.GetMealsInRange(start, end);

        var mealsCount = meals != null ? meals.Count() : 0;
        DateTime startDate = (DateTime)meals.Min(x => x.Date);
        DateTime endDate = (DateTime)meals.Max(x => x.Date);

        var foods = meals.SelectMany(m => m.Foods);
        var foodsCount = foods != null ? foods.Count() : 0;

        var favoriteFood = foods.GroupBy(f => f.Id)
                                    .OrderByDescending(g => g.Count())
                                    .FirstOrDefault();
        var mostFavoriteFood = favoriteFood != null ? favoriteFood.ElementAt(0).Name : "?";

        var mostFavoriteMealQuery = meals.GroupBy(m => m.Foods)
                                    .OrderByDescending(g => g.Count())
                                    .FirstOrDefault();
        var mostFavoriteMeal = mostFavoriteMealQuery != null ? mostFavoriteMealQuery.ElementAt(0) : new Meal() { Foods = new() { new Food() { Name = "Not favorite yet" } } };

        var mostTrickedFoodQuery = foods.Where(f => f.IsToAvoid)
                                     .GroupBy(f => f.Name)
                                     .OrderByDescending(g => g.Count())
                                     .Select(g => new { Name = g.Key, Count = g.Count() })
                                     .FirstOrDefault();
        var mostTrickedFood = mostTrickedFoodQuery != null ? mostTrickedFoodQuery.Name : "Not any!";

        decimal trickedMealPercentage;

        try
        {
            decimal mealsWithToAvoidFood = meals.Count(meal => meal.Foods.Any(food => food.IsToAvoid));
            trickedMealPercentage = (decimal)mealsWithToAvoidFood / meals.Count();
        }
        catch
        {
            trickedMealPercentage = 0;
        }



        var totalCalories = foods.Sum(f => f.Calories);
        TimeSpan deltaDate = endDate - startDate;
        var caloriesPerDay = deltaDate.Days == 0 ? totalCalories : (totalCalories / deltaDate.Days);

        var result = new Stats()
        {
            MostFavoriteFood = mostFavoriteFood,
            MostFavoriteMeal = mostFavoriteMeal,
            MostTrickedFood = mostTrickedFood,
            TotalCalories = totalCalories,
            AvarageCaloriesPerDay = caloriesPerDay,
            TotalFoods = foodsCount,
            TotalMeals = mealsCount,
            TrickedMeals = trickedMealPercentage

        };
        return await Task.FromResult(result);

    }
}
