using UnityEngine;
using Recipes;

namespace Cooking
{
    public class StoveZone : MonoBehaviour
    {
        [SerializeField] private float cookTime = 15f;

        private GameObject _obj;
        bool _inZone;
        float _timer;

        private void Start()
        {
            {
                _inZone = false;
                _timer = cookTime;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            _obj = other.gameObject;
            if (_obj.CompareTag("Pot"))
            {
                _inZone = true;
                Debug.Log("Panela está no fogão.");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (_obj.CompareTag("Pot"))
            {
                _inZone = false;
                Debug.Log("Panela saiu do fogão.");
            }
        }

        void Update()
        {
            if (_inZone)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    CookIngredients();
                    _timer = cookTime;
                }
            }
        }
        
        void CookIngredients()
        {
            Ingredient[] ingredients = _obj.GetComponentsInChildren<Ingredient>();
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient.currentState == IngredientState.Raw)
                {
                    ingredient.ChangeState(IngredientState.Cooked);
                } else if (ingredient.currentState == IngredientState.Cooked)
                {
                    ingredient.ChangeState(IngredientState.Spoiled);
                }
                
                Debug.Log(ingredient.ingredientName + " foi cozinhado!");
            }
        }
    }
}
