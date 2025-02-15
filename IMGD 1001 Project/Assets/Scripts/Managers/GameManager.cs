using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        _coroutine = StartCoroutine(StartRound(countdownSeconds));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
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
        //Debug.Log("Starting Round");
        countdownText.enabled = true;
        float currentSecond = math.ceil(countdownSeconds);

        while (countdownSeconds > 0)
        {
            if (currentSecond != math.floor(countdownSeconds))
            {
                //Debug.Log(currentSecond);
                AudioManager.instance.PlaySFX("beep1");
                currentSecond = math.floor(countdownSeconds);
            }
            countdownText.transform.localScale = new Vector3(countdownSeconds % 1+0.4f, countdownSeconds % 1+0.4f, 1);
            countdownText.text = math.ceil(countdownSeconds).ToString();
            countdownSeconds -= Time.deltaTime;
            yield return null;
        }

        if (AudioManager.instance.musicSource.isPlaying == false)
        {
            AudioManager.instance.PlayMusic("gameTheme");
        }
        countdownText.enabled = false;
        AudioManager.instance.PlaySFX("ballLaunch");
        this.ball.AddStartingForce();
    }
}
