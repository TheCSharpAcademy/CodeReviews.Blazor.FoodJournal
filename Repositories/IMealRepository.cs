using FoodJournal.Models;

namespace FoodJournal.Repositories;

public interface IMealRepository
{
    Task<List<Meal>> GetMeals();
    Task<Meal> GetMealById(int id);
    Task<string> AddMeal(Meal meal);
    Task<string> DeleteMeal(int id);
    Task<string> UpdateMeal(int id, Meal meal);
    Task<List<Food>> GetMealFoods(int id);
    Task<List<Meal>> GetMealsByFilter(DateTime? date, String? foodName, String? mealType, bool? isfavorite);
    Task<List<Meal>> GetMealsInRange(DateTime start, DateTime end);
}
