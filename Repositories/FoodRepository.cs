using FoodJournal.Data;
using FoodJournal.Enums;
using FoodJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Repositories;

//Her işlemden sonra dondurulen string ana sayfa footerda mesaj olarak gösterilecek

public class FoodRepository : IFoodRepository
{
    private FoodJournalDbContext _context;

    public FoodRepository(FoodJournalDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Food>> GetFoods(string? name, string? nutritionType, bool? isToAvoid)
    {
        try
        {
            var foods = from m in _context.Foods
                        select m;

            if (name != null)
            {
                foods = foods.Where(s => s.Name.Contains(name));
            }

            if (nutritionType != null)
            {
                foods = foods.Where(x => x.NutritionType == (Nutrition)Enum.Parse(typeof(Nutrition), nutritionType));
            }

            if (isToAvoid != null)
            {
                foods = foods.Where(x => x.IsToAvoid == isToAvoid);
            }

            return await foods.ToListAsync();
        }
        catch
        {
            return null;
        }
    }


    public async Task<Food> GetFoodById(int id)
    {
        try
        {
            var food = await _context.Foods.FindAsync(id);
            return await Task.FromResult(food);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string> DeleteFood(int id)
    {
        try
        {

            var foodToDelete = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(foodToDelete);
            await _context.SaveChangesAsync();
            return await Task.FromResult("Food deleted successfuly");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }
    }

    public async Task<string> UpdateFood(int id, Food food)
    {
        try
        {

            var foodToUpdate = await _context.Foods.FindAsync(id);
            if (foodToUpdate != null)
            {
                foodToUpdate.Id = id;
                foodToUpdate.Name = food.Name;
                foodToUpdate.Calories = food.Calories;
                foodToUpdate.Description = food.Description;
                foodToUpdate.NutritionTypeString = food.NutritionTypeString;
                foodToUpdate.Meals = food.Meals;
                _context.Foods.Update(foodToUpdate);
                await _context.SaveChangesAsync();
                return await Task.FromResult("Update Successful");
            }
            else
                return await Task.FromResult("No such Food found in DB to update");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }

    }

    public async Task<string> AddFood(Food food)
    {

        try
        {
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
            return await Task.FromResult("Food recorded to Db");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }
    }
}
