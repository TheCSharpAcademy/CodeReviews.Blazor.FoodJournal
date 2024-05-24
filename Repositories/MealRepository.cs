using FoodJournal.Data;
using FoodJournal.Enums;
using FoodJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Repositories;

public class MealRepository : IMealRepository
{
    FoodJournalDbContext _context;

    public MealRepository(FoodJournalDbContext context)
    {
        _context = context;
    }

    public async Task<List<Meal>> GetMeals()
    {
        try
        {
            var mealWithFoods = await _context.Meals.Include(m => m.Foods).ToListAsync();
            return await Task.FromResult(mealWithFoods);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Meal>> GetMealsByFilter(DateTime? date, String? foodName, String? mealType, bool? isfavorite)
    {
        try
        {

            var meals = from m in _context.Meals
                        select m;

            if (date != null)
            {
                meals = meals.Where(s => s.Date < date && s.Date > (date - TimeSpan.FromDays(1)));
            }

            if (foodName != null)
            {
                meals = meals.Where(x => x.Foods.Any(food => food.Name.ToLower().Contains(foodName.ToLower())));
            }

            if (isfavorite != null)
            {
                meals = meals.Where(x => x.IsFavorite == isfavorite);
            }

            if (mealType != null)
            {
                meals = meals.Where(x => x.Type == (MealType)Enum.Parse(typeof(MealType), mealType));
            }

            return await meals.Include(m => m.Foods).ToListAsync();


        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Meal>> GetMealsInRange(DateTime start, DateTime end)
    {
        try
        {
            var meals = await _context.Meals.Where(x => x.Date > start && x.Date < end + TimeSpan.FromDays(1)).Include(m => m.Foods).ToListAsync();
            return await Task.FromResult(meals);
        }
        catch { return null; }
    }

    public async Task<Meal> GetMealById(int id)
    {
        try
        {
            var resultMeal = await _context.Meals.FindAsync(id);
            return await Task.FromResult(resultMeal);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string> AddMeal(Meal meal)
    {
        try
        {
            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();
            return await Task.FromResult("Meal recorded to Db");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }
    }

    public async Task<string> DeleteMeal(int id)
    {
        try
        {
            var mealToRemove = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(mealToRemove);
            await _context.SaveChangesAsync();
            return await Task.FromResult("Meal deleted successfuly");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }
    }

    public async Task<string> UpdateMeal(int id, Meal meal)
    {
        try
        {

            var mealToUpdate = await _context.Meals.FindAsync(id);
            if (mealToUpdate != null)
            {
                mealToUpdate.Id = id;
                mealToUpdate.Type = meal.Type;
                mealToUpdate.Description = meal.Description;
                mealToUpdate.IsFavorite = meal.IsFavorite;
                mealToUpdate.Foods = meal.Foods;
                mealToUpdate.Date = meal.Date;
                _context.Meals.Update(mealToUpdate);
                await _context.SaveChangesAsync();
                return await Task.FromResult("Update Successful");
            }
            else
                return await Task.FromResult("No such meal found in DB to update");
        }
        catch (Exception ex)
        {
            return await Task.FromResult(ex.Message);
        }

    }

    public async Task<List<Food>> GetMealFoods(int id)
    {
        try
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
                return await Task.FromResult(meal.Foods.ToList());
            else
                return null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return null;
        }
    }

}
