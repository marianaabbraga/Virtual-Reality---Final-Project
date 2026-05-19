using UnityEngine;

public class PlateZone : MonoBehaviour
{
    // Deteta se os ingredientes estão ou não dentro do prato, se estiver o ingrediente torna-se filho do prato, quando sai do prato, o processo é revertido.
    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && other.transform.parent != transform.parent)
        {
            other.transform.SetParent(transform.parent);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            other.transform.SetParent(null);
        }
    }
}
