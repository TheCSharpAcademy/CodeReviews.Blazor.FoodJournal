using FoodJournal.Models;
using FoodJournal.Repositories;


namespace FoodJournal.Services;

public class FoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;

    }

    public async Task<List<Food>> GetFoods(string? name, string? nutritionType, bool? isToAvoid)
    {
        var result = await _foodRepository.GetFoods(name, nutritionType, isToAvoid);
        return result;
    }

    public async Task<Food> GetFoodById(int id)
    {
        return await _foodRepository.GetFoodById(id);
    }

    public async Task<string> DeleteFood(int id)
    {
        return await _foodRepository.DeleteFood(id);
    }

    public async Task<string> UpdateFood(int id, Food food)
    {
        return await _foodRepository.UpdateFood(id, food);
    }

    public async Task<string> AddFood(Food food)
    {
        return await _foodRepository.AddFood(food);
    }

}
