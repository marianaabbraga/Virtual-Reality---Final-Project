using Recipes;

namespace Orders
{
    // Define a estrutura de cada pedido
    public class Order
    {
        public Recipe Recipe;
        public float TimeLimit;
        public float TimeRemaining;
        public bool IsExpired => TimeRemaining <= 0;

        public Order(Recipe recipe, float timeLimit)
        {
            this.Recipe = recipe;
            this.TimeLimit = timeLimit;
            this.TimeRemaining = timeLimit;
        }

        public void UpdateTimer(float deltaTime)
        {
            TimeRemaining -= deltaTime;
        }
    }
}