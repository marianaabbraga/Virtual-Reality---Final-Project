using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tablet
{
    public class RecipePage : MonoBehaviour
    {
        [Header("Painel – Lista de Receitas")]
        [SerializeField] private GameObject recipeListPanel;
        [SerializeField] private Transform  recipeListContent;
        [SerializeField] private GameObject recipeEntryTemplate;

        [Header("Painel – Ingredientes")]
        [SerializeField] private GameObject      ingredientsPanel;
        [SerializeField] private TextMeshProUGUI ingredientsPanelTitle;
        [SerializeField] private Transform       ingredientsContent;
        [SerializeField] private GameObject      ingredientLineTemplate;
        [SerializeField] private Button          backButton;

        private bool isOpen = false;
        private List<Recipe> recipes = new();
        private readonly List<GameObject> spawnedEntries     = new();
        private readonly List<GameObject> spawnedIngredients = new();

        void Awake()
        {
            var loaded = Resources.LoadAll<Recipe>("Recipes");
            recipes.AddRange(loaded);

            recipeListPanel.SetActive(false);
            ingredientsPanel.SetActive(false);
            backButton.onClick.AddListener(ShowRecipeList);
        }

        public void Toggle()
        {
            if (isOpen) Close();
            else        Open();
        }

        public void Open()
        {
            isOpen = true;
            ShowRecipeList();
        }

        public void Close()
        {
            isOpen = false;
            recipeListPanel.SetActive(false);
            ingredientsPanel.SetActive(false);
        }

        private void ShowRecipeList()
        {
            ingredientsPanel.SetActive(false);
            recipeListPanel.SetActive(true);

            foreach (var go in spawnedEntries) Destroy(go);
            spawnedEntries.Clear();

            foreach (var recipe in recipes)
            {
                var entry = Instantiate(recipeEntryTemplate, recipeListContent);
                entry.SetActive(true);

                var nameText = entry.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null) nameText.text = recipe.recipeName;

                var btn = entry.GetComponent<Button>();
                var captured = recipe;
                btn.onClick.AddListener(() => ShowIngredients(captured));

                spawnedEntries.Add(entry);
            }
        }

        private void ShowIngredients(Recipe recipe)
        {
            recipeListPanel.SetActive(false);
            ingredientsPanel.SetActive(true);

            ingredientsPanelTitle.text = recipe.recipeName;

            foreach (var go in spawnedIngredients) Destroy(go);
            spawnedIngredients.Clear();

            foreach (var ingredient in recipe.requiredIngredients)
            {
                var line = Instantiate(ingredientLineTemplate, ingredientsContent);
                line.SetActive(true);

                var txt = line.GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null)
                    txt.text = $"{ingredient.ingredientName} x{ingredient.quantity} ({ingredient.requiredState})";

                spawnedIngredients.Add(line);
            }
        }
    }
}