using Recipes;
using UnityEngine;

namespace Trash
{
    public class FloorZone : MonoBehaviour
    {
        private GameObject _obj;

        void OnTriggerEnter(Collider other)
        {
            _obj = other.gameObject;
            if (_obj.CompareTag("Ingredient") && _obj.GetComponent<Ingredient>().currentState != IngredientState.Spoiled)
            {
                _obj.GetComponent<Ingredient>().ChangeState(IngredientState.Spoiled);
            }
        }
    }
}