/*
Cook Lasagna

You are given a recipe for cooking a lasagna. The recipe consists of a list of ingredients and their required quantities. 
Each ingredient has a specific preparation time and requires multiple appliances for cooking. 
Each appliance has a different cooking time. 
Your task is to write a program that calculates the total time needed to cook the lasagna, 
taking into account the availability of appliances and selecting the one with the shortest cooking time for each ingredient. 
Write a function cook_lasagna(recipe, available_tools) -> int that takes a list of ingredients and a list of available appliances as input, 
and returns the total cooking time in minutes. 
For each ingredient, you should select the available appliance with the shortest cooking time.

recipe = [
  {"name": "Pasta", "quantity": 200, "preparation_time": 5, "appliances": ["Pot", "Oven", "Microwave"]},
  {"name": "Tomato Sauce", "quantity": 500, "preparation_time": 10, "appliances": ["Pot", "Pan", "Oven"]},
  {"name": "Cheese", "quantity": 300, "preparation_time": 5, "appliances": ["Oven", "Microwave", "Pan"]}
]

available_appliances = [
    {"name": "Pot", "cooking_time": 10},
    {"name": "Pan", "cooking_time": 15},
    {"name": "Oven", "cooking_time": 20},
    {"name": "Microwave", "cooking_time": 5}
]


// Output -> 30 minutes

*/

// Define the global pool of available appliances and their fixed speeds
Dictionary<string, int> availableAppliances = new()
{
    {"Pot", 10},
    {"Pan", 15},
    {"Oven", 20},
    {"Microwave", 5}
};

// Define the recipe
List<Ingredient> recipe = [
    new ("Pasta", 200, 5, ["Pot", "Oven", "Microwave"]),
    new ("Tomato Sauce", 500, 10, ["Pot", "Pan", "Oven"]),
    new ("Cheese", 300, 5, ["Oven", "Microwave", "Pan"])
];

Console.WriteLine($"{CookLasagna(recipe, availableAppliances)} minutes");

/// <summary>
/// Calculates the total time assuming sequential Prep but parallel Cooking to cook the lasagna.
/// </summary>
/// <param name="recipe">The list of ingredients.</param>
/// <param name="availableTools">The available appliances.</param>
/// <remarks>
/// 1. Calculate total active preparation time (Sequential)
/// 2. Find the single longest cooking time among all ingredients (Parallel)
/// Find the longest time among ALL ingredients
/// </remarks>
/// <result>The total cooking time in minutes.</result>
static int CookLasagna(List<Ingredient> recipe, Dictionary<string, int> availableTools) =>
    recipe.Sum(i => i.PreparationTime) + recipe
        .Select(ingredient =>
            ingredient.CompatibleAppliances
                .Where(availableTools.ContainsKey)
                .Select(tool => availableTools[tool])
                .DefaultIfEmpty(0)
                .Min()
        )
        .DefaultIfEmpty(0)
        .Max();

/// <summary>
/// Ingredient Record
/// </summary>
/// <param name="Name">The name of the ingredient.</param>
/// <param name="Quantity">The quantity of the ingredient.</param>
/// <param name="PreparationTime">The preparation time for the ingredient.</param>
/// <param name="Appliances">The appliances required for the ingredient.</param>
public record Ingredient(string Name, int Quantity, int PreparationTime, List<string> CompatibleAppliances);