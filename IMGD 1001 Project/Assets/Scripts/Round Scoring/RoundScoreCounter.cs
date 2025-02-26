using UnityEngine;

public class RoundScoreCounter : MonoBehaviour
{
    public int score = 0;

    private RoundScoreBubble[] bubbles;

    private void Awake()
    {
        bubbles = GetComponentsInChildren<RoundScoreBubble>();
    }

    // Each time a player scores, increment the score and turn on the next bubble
    public void IncrementScore()
    {
        score++;
        if (score <= bubbles.Length)
        {
            bubbles[score - 1].SetOn();
        }
    }

    public void ResetScore()
    {
        score = 0;
        foreach (RoundScoreBubble bubble in bubbles)
        {
            bubble.SetOff();
        }
    }
}
