using RecipeApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeApp1.ViewModels
{
    public class RecipeViewModel
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Step> Steps { get; set; }
        private ObservableCollection<Ingredient> OriginalIngredients { get; set; }

        public RecipeViewModel()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Steps = new ObservableCollection<Step>();
            OriginalIngredients = new ObservableCollection<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            SaveOriginalIngredients();
        }

        public void AddStep(Step step)
        {
            Steps.Add(step);
        }

        public void DisplayRecipe()
        {
            string recipeDetails = "Ingredients:\n";
            foreach (var ingredient in Ingredients)
            {
                recipeDetails += $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})\n";
            }

            recipeDetails += "\nSteps:\n";
            foreach (var step in Steps)
            {
                recipeDetails += $"{step.Description}\n";
            }

            int totalCalories = Ingredients.Sum(ingredient => ingredient.Calories);
            recipeDetails += $"\nTotal Calories: {totalCalories}";

            MessageBox.Show(recipeDetails);
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities()
        {
            Ingredients.Clear();
            foreach (var ingredient in OriginalIngredients)
            {
                Ingredients.Add(new Ingredient
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit,
                    Calories = ingredient.Calories,
                    FoodGroup = ingredient.FoodGroup
                });
            }
        }

        public void ClearRecipe()
        {
            Ingredients.Clear();
            Steps.Clear();
            OriginalIngredients.Clear();
        }

        public void FilterRecipes(string ingredientName, string foodGroup, int maxCalories)
        {
            var filteredIngredients = OriginalIngredients.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(ingredientName))
            {
                filteredIngredients = filteredIngredients.Where(i => i.Name.Contains(ingredientName));
            }

            if (!string.IsNullOrWhiteSpace(foodGroup) && foodGroup != "All")
            {
                filteredIngredients = filteredIngredients.Where(i => i.FoodGroup == foodGroup);
            }

            if (maxCalories > 0)
            {
                filteredIngredients = filteredIngredients.Where(i => i.Calories <= maxCalories);
            }

            Ingredients.Clear();
            foreach (var ingredient in filteredIngredients)
            {
                Ingredients.Add(ingredient);
            }
        }

        private void SaveOriginalIngredients()
        {
            OriginalIngredients = new ObservableCollection<Ingredient>(Ingredients.Select(ingredient => new Ingredient
            {
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit,
                Calories = ingredient.Calories,
                FoodGroup = ingredient.FoodGroup
            }));
        }
    }
}