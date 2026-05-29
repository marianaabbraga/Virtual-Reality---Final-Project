using System.Collections.Generic;
using System.Linq;
using Recipes;
using UnityEngine;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        public static OrderManager Instance;

        [Header("Configuração")]
        [SerializeField] private Recipe[] availableRecipes;
        [SerializeField] private RoundManager roundManager;
        [SerializeField] private int maxOrders = 3;
        [SerializeField] private float timeBetweenOrders = 10f;
        [SerializeField] private float orderTimeLimit = 120f;

        private List<Order> _activeOrders = new List<Order>();
        private float _timer;
        private int _currentMaxOrders;
        private bool _resetStats;
        private List<float> _ratings = new List<float>();

        public float ordersRating = 0;
        public int spoiledIngredients = 0;
        public int ordersCompleted = 0;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            _timer = timeBetweenOrders;
            if (roundManager.state == RoundState.On)
            {
                AddRandomOrder();
            }
        }

        private void Update()
        {
            ordersRating = _ratings.Count > 0 ? _ratings.Average() : 0;
            if (roundManager.state == RoundState.On)
            {
                _currentMaxOrders = maxOrders;
                if (_resetStats)
                {
                    _ratings.Clear();
                    ordersCompleted = 0;
                    spoiledIngredients = 0;
                    _resetStats = false;
                }
            }
            else
            {
                _currentMaxOrders = 0;
                _activeOrders.Clear();
                _resetStats = true;
            }
            
            foreach (Order order in _activeOrders)
            {
                order.UpdateTimer(Time.deltaTime);
            }

            _activeOrders.RemoveAll(o => o.IsExpired);

            _timer -= Time.deltaTime;
            if (_timer <= 0 && _activeOrders.Count < _currentMaxOrders)
            {
                AddRandomOrder();
                _timer = timeBetweenOrders;
            }
        }

        private void AddRandomOrder()
        {
            if (availableRecipes.Length == 0)
            {
                Debug.LogError("Nenhuma receita disponível! Preenche o array no Inspector.");
                return;
            }

            Recipe randomRecipe = availableRecipes[Random.Range(0, availableRecipes.Length)];

            if (randomRecipe == null)
            {
                Debug.LogError("Receita é null! Verifica o array no Inspector.");
                return;
            }

            _activeOrders.Add(new Order(randomRecipe, orderTimeLimit));
            Debug.Log("Novo pedido: " + randomRecipe.recipeName);
        }

        public bool CheckOrder(List<Transform> plateIngredients)
        {
            List<Ingredient> ingredientsOnPlate = new List<Ingredient>();
            foreach (Transform child in plateIngredients)
            {
                Ingredient ingredient = child.GetComponent<Ingredient>();
                if (ingredient != null)
                    ingredientsOnPlate.Add(ingredient);
            }
            
            foreach (Order order in _activeOrders)
            {
                List<RecipeIngredient> requiredIngredients = order.Recipe.requiredIngredients.ToList();
                int totalIngredientsRequired = requiredIngredients.Sum(r => r.quantity);
                
                List<RecipeIngredient> missingIngredients = requiredIngredients.Where(required =>
                {
                    int countOnPlate = ingredientsOnPlate.Count(ingredient =>
                        ingredient.ingredientName.ToString() == required.ingredientName.ToString() &&
                        ingredient.currentState == required.requiredState
                    );

                    return countOnPlate < required.quantity;
                }).ToList();

                
                if (missingIngredients.Count == 0 &&
                    totalIngredientsRequired == ingredientsOnPlate.Count)
                {
                    // Rating calculations
                    float timeUsedPercentage = (order.TimeRemaining / orderTimeLimit) * 100f;
                    _ratings.Add(timeUsedPercentage);
                    
                    _activeOrders.Remove(order);
                    Debug.Log("Pedido completo: " + order.Recipe.recipeName + ". Rating: " + ordersRating.ToString("F1"));
                    ordersCompleted ++;
                    return true;
                }
                foreach (RecipeIngredient missing in missingIngredients)
                {
                    Debug.Log("Falta: " + missing.ingredientName + " (" + missing.requiredState + ")");
                }
            }
            return false;
        }

        public List<Order> GetActiveOrders() => _activeOrders;
    }
}