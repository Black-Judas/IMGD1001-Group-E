using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    public Paddle playerPaddle, computerPaddle;

    public Text playerScoreText, computerScoreText;
    public TMP_Text countdownText;

    public float countdownSeconds = 3f;

    private int _playerScore;
    private int _computerScore;

    private Coroutine _coroutine;

    private void Start()
    {
        AudioManager.instance.PlayMusic("gameTheme");
        _coroutine = StartCoroutine(StartRound(countdownSeconds));
    }

    public void PlayerScores()
    {
        _playerScore++;
        this.playerScoreText.text = _playerScore.ToString();

        ResetRound();
    }

    public void ComputerScores()
    {
        _computerScore++;
        this.computerScoreText.text = _computerScore.ToString();

        ResetRound();
    }

    private void ResetRound()
    {
        this.playerPaddle.ResetPosition();
        this.computerPaddle.ResetPosition();
        this.ball.ResetPosition();

        _coroutine = StartCoroutine(StartRound(countdownSeconds));
    }

    IEnumerator StartRound(float countdownSeconds)
    {
        Debug.Log("Starting Round");
        countdownText.enabled = true;
        while (countdownSeconds > 0)
        {
            countdownText.text = math.ceil(countdownSeconds).ToString();
            countdownSeconds -= Time.deltaTime;
            yield return null;
        }

        countdownText.enabled = false;
        this.ball.AddStartingForce();
    }
}
