using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] public float roundDuration = 300f;
    [SerializeField] public float intervalDuration = 45f;
    public float timer;
    public RoundState state = RoundState.Off;
    
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
}
