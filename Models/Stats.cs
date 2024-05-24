namespace FoodJournal.Models
{
    public class Stats
    {
        public int? TotalMeals { get; set; }
        public int? TotalFoods { get; set; }
        public decimal? TotalCalories { get; set; }
        public double? AvarageCaloriesPerDay { get; set; }
        public string? MostFavoriteFood { get; set; }
        public Meal? MostFavoriteMeal { get; set; }
        public string? MostTrickedFood { get; set; }
        public decimal? TrickedMeals { get; set; }

    }
}
