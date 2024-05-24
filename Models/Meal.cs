using FoodJournal.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodJournal.Models;

public class Meal
{
    public Meal() { }
    public int Id { get; set; }

    [Required]
    public MealType Type { get; set; }
    public string MealTypeString
    {
        get { return Type.ToString(); }
        set
        {
            Type = (MealType)Enum.Parse(typeof(MealType), value);
        }
    }

    public string? Description { get; set; }
    public bool IsFavorite { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    public List<Food> Foods { get; set; } = [];


}

