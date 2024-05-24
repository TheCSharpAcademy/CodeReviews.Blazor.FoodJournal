using FoodJournal.Models;

namespace FoodJournal.Repositories;

public interface IFoodRepository
{
    Task<List<Food>> GetFoods(string? name, string? nutritionType, bool? isToAvoid);
    Task<Food> GetFoodById(int id);
    Task<string> DeleteFood(int id);
    Task<string> UpdateFood(int id, Food food);
    Task<string> AddFood(Food food);
}
