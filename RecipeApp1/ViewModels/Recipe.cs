using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace RecipeApp1.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }

    public class Step
    {
        public string Description { get; set; }
    }

    public delegate void CaloriesExceededHandler(string message);

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<Step> Steps { get; private set; }
        public event CaloriesExceededHandler CaloriesExceeded;

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void AddStep(Step step)
        {
            Steps.Add(step);
        }

        public int TotalCalories()
        {
            int totalCalories = Ingredients.Sum(i => i.Calories);
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories.");
            }
            return totalCalories;
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities(List<Ingredient> originalIngredients)
        {
            Ingredients = new List<Ingredient>(originalIngredients);
        }

        public void ClearRecipe()
        {
            Ingredients.Clear();
            Steps.Clear();
        }
        
    }
}
