using FoodJournal.Models;
using FoodJournal.Repositories;

namespace FoodJournal.Services;

public class MealService
{
    private readonly IMealRepository _mealRepository;


    public MealService(IMealRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }

    public async Task<List<Meal>> GetMeals()
    {
        var result = await _mealRepository.GetMeals();
        return result;
    }

    public async Task<List<Meal>> GetMealsByFilter(DateTime? date, String? foodName, String? mealType, bool? isfavorite)
    {
        if (date != null)
        {
            date = date + TimeSpan.FromDays(1);
        }
        if (mealType == "All")
            mealType = null;
        var result = await _mealRepository.GetMealsByFilter(date, foodName, mealType, isfavorite);
        return result;
    }

    public async Task<Meal> GetMealById(int id)
    {
        return await _mealRepository.GetMealById(id);
    }

    public async Task<string> DeleteMeal(int id)
    {
        return await _mealRepository.DeleteMeal(id);
    }

    public async Task<string> UpdateMeal(int id, Meal meal)
    {
        return await _mealRepository.UpdateMeal(id, meal);
    }

    public async Task<string> AddMeal(Meal meal)
    {
        return await _mealRepository.AddMeal(meal);
    }

    public async Task<List<Food>> GetMealFoods(int id)
    {
        return await _mealRepository.GetMealFoods(id);
    }
}
