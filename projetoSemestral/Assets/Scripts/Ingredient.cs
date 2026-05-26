using Recipes;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Define o nome e estado de cada ingrediente
    [Header("Propriedades")]
    public IngredientList ingredientName;
    public IngredientState currentState = IngredientState.Raw;
    
    [Header("Materiais")]
    [SerializeField] private Material rawMaterial;
    [SerializeField] private Material cookedMaterial;
    [SerializeField] private Material spoiledMaterial;
    public RoundManager roundManager;
    
    [Header("Reset")]
    public Transform resetPoint;
    
    public void ChangeState(IngredientState newState)
    {
        currentState = newState;
        UpdateMaterial();
        Debug.Log(ingredientName + " está agora: " + newState);
    }
    
    private void UpdateMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) return;

        switch (currentState)
        {
            case IngredientState.Raw:
                if (rawMaterial != null) meshRenderer.material = rawMaterial;
                break;
            case IngredientState.Cooked:
                if (cookedMaterial != null) meshRenderer.material = cookedMaterial;
                break;
            case IngredientState.Spoiled:
                if (spoiledMaterial != null) meshRenderer.material = spoiledMaterial;
                if (roundManager.state == RoundState.On)
                {
                    Orders.OrderManager.Instance.spoiledIngredients++;
                }
                break;
        }
    }
    
    public void ResetPosition()
    {
        transform.SetParent(null);
        ChangeState(IngredientState.Raw);
        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;
    }
}
