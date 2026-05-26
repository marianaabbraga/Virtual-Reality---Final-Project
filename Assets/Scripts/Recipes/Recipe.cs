using UnityEngine;

namespace Recipes
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Kitchen/Recipe")]
    public class Recipe : ScriptableObject
    {
        public string recipeName;
        public int requiredLevel;
        public RecipeIngredient[] requiredIngredients;
    }
}