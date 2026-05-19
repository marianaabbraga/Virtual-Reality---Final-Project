using Recipes;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Define o nome e estado de cada ingrediente
    [Header("Propriedades")]
    public IngredientList ingredientName;
    public IngredientState currentState = IngredientState.Raw;
    
    [Header("Reset")]
    public Transform resetPoint;
    
    public void ChangeState(IngredientState newState)
    {
        currentState = newState;
        Debug.Log(ingredientName + " está agora: " + newState);
    }
    
    public void ResetPosition()
    {
        transform.SetParent(null);
        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;
    }
}
