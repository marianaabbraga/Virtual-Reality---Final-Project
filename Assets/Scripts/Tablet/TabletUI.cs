using Orders;
using UnityEngine;

namespace Tablet
{
    public class TabletUI : MonoBehaviour
    {
        [SerializeField] private RoundManager roundManager;
        [SerializeField] private TabletScreen tabletScreen;
        [SerializeField] private OrderManager orderManager;
        
        [SerializeField] private SkipButton skipButton;
        [SerializeField] private RecipePage recipePage;

        void Update()
        {
            tabletScreen.dayNumber.text = roundManager.level.ToString();
            
            if (roundManager.state == RoundState.Off)
            {
                tabletScreen.roundInfo.text = "O restaurante abre em:";
            } 
            else if (roundManager.state == RoundState.On)
            {
                tabletScreen.roundInfo.text = "O restaurante fecha em:";
            }
            
            int totalSeconds = Mathf.CeilToInt(roundManager.timer);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            tabletScreen.timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            tabletScreen.rateText.text = orderManager.ordersRating.ToString("F1") + "% / " + roundManager.rateGoal.ToString("F1") + "%";
            tabletScreen.doneText.text = orderManager.ordersCompleted.ToString() + " / " + roundManager.orderGoal.ToString();
            tabletScreen.rottenText.text = orderManager.spoiledIngredients.ToString() + " / " + roundManager.spoiledGoal.ToString();
        }
    }
}