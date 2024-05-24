using FoodJournal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodJournal.Models;

public class Food
{

    public Food() { }
    public int Id { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid positive integer")]
    public int? Calories { get; set; }
    public bool IsToAvoid { get; set; }
    public Nutrition NutritionType { get; set; }
    public string NutritionTypeString
    {
        get { return NutritionType.ToString(); }
        set
        {
            NutritionType = (Nutrition)Enum.Parse(typeof(Nutrition), value);
        }
    }

    public List<Meal> Meals { get; set; } = [];


}