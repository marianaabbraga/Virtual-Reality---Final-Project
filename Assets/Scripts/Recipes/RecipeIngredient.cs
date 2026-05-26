namespace Recipes
{
    [System.Serializable]
    public class RecipeIngredient
    {
        public IngredientList ingredientName;
        public int quantity;
        public IngredientState requiredState;
    }
}