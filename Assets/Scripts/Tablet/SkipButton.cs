using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tablet
{
    public class SkipButton : MonoBehaviour
    {
        [Header("Referências")]
        [SerializeField] private RoundManager roundManager;

        [Header("UI do Botão")]
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonLabel;

        [Header("Cores")]
        [SerializeField] private Color colorOpen  = new Color(0.18f, 0.80f, 0.44f); 
        [SerializeField] private Color colorClose = new Color(0.85f, 0.15f, 0.15f); 

        private Image buttonImage;

        void Awake()
        {
            buttonImage = button.GetComponent<Image>();
            button.onClick.AddListener(OnSkipClicked);
        }

        void Update()
        {
            RefreshVisuals();
        }

        /// <summary>Actualiza cor e texto conforme o estado do round.</summary>
        private void RefreshVisuals()
        {
            if (roundManager.state == RoundState.Off)
            {
                buttonLabel.text  = "Abrir Agora";
                buttonImage.color = colorOpen;
            }
            else
            {
                buttonLabel.text  = "Fechar";
                buttonImage.color = colorClose;
            }
        }
        
        private void OnSkipClicked()
        {
            roundManager.timer = 0f;
        }
    }
}