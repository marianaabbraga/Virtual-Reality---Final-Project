using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tablet
{
    public class RecipeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private RecipePage recipePage;

        void Awake()
        {
            button.onClick.AddListener(recipePage.Toggle);
        }
    }
}

   