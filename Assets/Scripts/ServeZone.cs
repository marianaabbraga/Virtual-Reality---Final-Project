using System.Collections.Generic;
using Orders;
using Recipes;
using UnityEngine;

public class ServeZone : MonoBehaviour
{
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private Transform resetPointPlate;
    [SerializeField] private Transform resetPointBowl;
    [SerializeField] private Transform resetPointIngredients;
    bool _inZone;
    float _timer;
    private GameObject _obj;

    void Start()
    {
        _inZone = false;
        _timer = delayTime;
    }
    
    void OnTriggerEnter(Collider other)
    {
        _obj = other.gameObject;
        if (other.gameObject.CompareTag("Plate") || other.gameObject.CompareTag("Bowl"))
        {
            _inZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            _inZone = false;
        }
        else if (other.gameObject.CompareTag("Bowl"))
        {
            _inZone = false;
        }
    }

    private void Update()
    {
        if (_inZone)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                DetachIngredients(_obj);

                _timer = delayTime;
                _inZone = false;
            }
        }
        else
        {
            _timer = delayTime;
        }
    }
    
    // Percorre todos os filhos do prato, separa-os e compara o prato aos pedidos
    void DetachIngredients(GameObject container)
    {
        Ingredient[] allIngredients = container.GetComponentsInChildren<Ingredient>();
        List<Transform> children = new List<Transform>();
        foreach (Ingredient ingredient in allIngredients)
        {
            children.Add(ingredient.transform);
        }
        
        bool orderComplete = OrderManager.Instance.CheckOrder(children);

        if (orderComplete)
        {
            foreach (Transform child in children)
            {
                Ingredient ingredient = child.GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    if (ingredient.currentState != IngredientState.Raw)
                    {
                        ingredient.ChangeState(IngredientState.Raw);
                    }
                    if (ingredient.resetPoint != null)
                    {
                        ingredient.ResetPosition();
                    }
                    else
                    {
                        Debug.Log("O " + ingredient.ingredientName + " não tem resetPoint.");
                    }
                }
            }
            
            if (_obj.CompareTag("Plate"))
            {
                ResetObject(_obj, resetPointPlate);
            }
            else if (_obj.CompareTag("Bowl"))
            {
                ResetObject(_obj, resetPointBowl);
            }
        }
        else
        {
            Debug.Log("Prato não corresponde a nenhum pedido!");
        }
    }

    void ResetObject(GameObject obj, Transform resetObj)
    {
        obj.transform.position = resetObj.position;
        obj.transform.rotation = resetObj.rotation;
    }
}