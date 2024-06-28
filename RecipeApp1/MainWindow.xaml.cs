using RecipeApp1.Models;
using RecipeApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeApp1
{
    public partial class MainWindow : Window
    {
        private RecipeViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            SetDefaultPlaceholderText();
            viewModel = (RecipeViewModel)DataContext;
        }

        private void SetDefaultPlaceholderText()
        {
            SetPlaceholderText(IngredientNameTextBox);
            SetPlaceholderText(IngredientQuantityTextBox);
            SetPlaceholderText(IngredientUnitTextBox);
            SetPlaceholderText(IngredientCaloriesTextBox);
            SetPlaceholderText(IngredientFoodGroupTextBox);
            SetPlaceholderText(StepDescriptionTextBox);
            SetPlaceholderText(ScaleFactorTextBox);
            SetPlaceholderText(IngredientFilterTextBox);
        }

        private void SetPlaceholderText(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void ClearPlaceholderText(TextBox textBox)
        {
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ClearPlaceholderText(textBox);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                SetPlaceholderText(textBox);
            }
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(IngredientQuantityTextBox.Text, out double quantity) &&
                int.TryParse(IngredientCaloriesTextBox.Text, out int calories))
            {
                viewModel.AddIngredient(new Ingredient
                {
                    Name = IngredientNameTextBox.Text,
                    Quantity = quantity,
                    Unit = IngredientUnitTextBox.Text,
                    Calories = calories,
                    FoodGroup = IngredientFoodGroupTextBox.Text
                });
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please enter valid quantity and calorie values.");
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddStep(new Step { Description = StepDescriptionTextBox.Text });
            StepDescriptionTextBox.Text = "Step Description";
        }

        private void DisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DisplayRecipe();
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ScaleFactorTextBox.Text, out double factor))
            {
                viewModel.ScaleRecipe(factor);
                ScaleFactorTextBox.Text = "Factor";
            }
            else
            {
                MessageBox.Show("Please enter a valid scale factor.");
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ResetQuantities();
        }

        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearRecipe();
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterRecipes(IngredientFilterTextBox.Text,
                                    (string)((ComboBoxItem)FoodGroupFilterComboBox.SelectedItem).Content,
                                    (int)MaxCaloriesSlider.Value);
        }

        private void ClearInputFields()
        {
            IngredientNameTextBox.Text = "Ingredient Name";
            IngredientQuantityTextBox.Text = "Quantity";
            IngredientUnitTextBox.Text = "Unit";
            IngredientCaloriesTextBox.Text = "Calories";
            IngredientFoodGroupTextBox.Text = "Food Group";
        }
    }
}