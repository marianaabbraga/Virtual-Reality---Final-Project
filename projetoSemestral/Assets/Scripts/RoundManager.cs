using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] public float roundDuration = 300f;
    [SerializeField] public float intervalDuration = 45f;
    [SerializeField] private Orders.OrderManager orderManager;
    
    public float timer;
    public RoundState state = RoundState.Off;
    public int level = 1;
    
    public float rateGoal;
    public int orderGoal;
    public int spoiledGoal = 10;
    
    void Start()
    {
        timer = intervalDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (state == RoundState.Off)
            {
                if (level <= 0)
                {
                    level = 1;
                }
                if (level < 4 && orderManager.ordersRating >= rateGoal && orderManager.ordersCompleted >= orderGoal && orderManager.spoiledIngredients <= spoiledGoal)
                {
                    level++;
                }
                DefineGoals();
                timer = roundDuration;
                state = RoundState.On;
            }
            else
            {
                timer = intervalDuration;
                state = RoundState.Off;
            }
        }
    }

    void DefineGoals()
    {
        if (level == 1)
        {
            rateGoal = 0;
            orderGoal = 3;
            spoiledGoal = 10;
        } else if (level == 2)
        {
            rateGoal = 30;
            orderGoal = 7;
            spoiledGoal = 5;
        } else if (level == 3)
        {
            rateGoal = 55;
            orderGoal = 12;
            spoiledGoal = 3;
        } else if (level == 4)
        {
            rateGoal = 80;
            orderGoal = 15;
            spoiledGoal = 0;
        }
    }
}
