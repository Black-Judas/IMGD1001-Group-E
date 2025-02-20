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

    //Player references
    public Paddle player1Paddle, player2Paddle;
    public Text player1MatchScoreText, player2MatchScoreText; //UI text for the player's match score
    private int _player1MatchScore, _player2MatchScore; //The player's match score
    public RoundScoreCounter player1RoundScoreCounter, player2RoundScoreCounter; //The players' round score counters

    //General UI references
    public TMP_Text countdownText;
    public float countdownSeconds = 3f;
    public GameObject debugMenu;
    public TMP_Text announcementText;


    //Unity methods
    private void Start()
    {
        ToggleDebugMenu();

        StartCoroutine(StartRound(countdownSeconds));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleDebugMenu();
        }
    }

    //Debug methods
    public void ToggleDebugMenu() //Toggle the debug menu
    {
        //Debug.Log("Debug toggled");
        debugMenu.SetActive(!debugMenu.activeSelf);
    }


    //Game methods
    public void Player1Scores()
    {

        Debug.Log("Player 1 Scores");

        player1RoundScoreCounter.IncrementScore();

        //If the player scores 2 points, they win the round and their match score is incremented
        if (player1RoundScoreCounter.score >= 2)
        {
            _player1MatchScore++;
            player1MatchScoreText.text = _player1MatchScore.ToString();
            player1RoundScoreCounter.ResetScore();
            player2RoundScoreCounter.ResetScore();

            StartCoroutine(RoundWon(player1Paddle));
        }
        else
        {
            ResetPlayArea();
            StartCoroutine(StartRound(countdownSeconds));
        }

    }
    public void Player2Scores()
    {

        Debug.Log("Player 2 Scores");

        player2RoundScoreCounter.IncrementScore();

        //If the player scores 2 points, they win the round and their match score is incremented
        if (player2RoundScoreCounter.score >= 2)
        {
            _player2MatchScore++;
            player2MatchScoreText.text = _player2MatchScore.ToString();
            player1RoundScoreCounter.ResetScore();
            player2RoundScoreCounter.ResetScore();


            StartCoroutine(RoundWon(player2Paddle));
        }
        else
        {
            ResetPlayArea();
            StartCoroutine(StartRound(countdownSeconds));
        }

    }
    private void ResetPlayArea()
    {

        this.player1Paddle.ResetPosition();
        this.player2Paddle.ResetPosition();
        this.ball.ResetPosition();

    }
    

    //Coroutines
    IEnumerator Announce(string message, float duration = 4f)
    {
        announcementText.text = message;
        announcementText.enabled = true;
        yield return new WaitForSeconds(duration);
        announcementText.enabled = false;
    }
    IEnumerator RoundWon(Paddle player)
    {
        //Announce the winner
        Coroutine coroutine;
        coroutine = StartCoroutine(Announce(player.gameObject.name + " wins the round!", 3f));

        //TODO: Add in the modifier selection screen here

        ResetPlayArea();

        //Start the next round after the announcement finishes
        yield return coroutine;
        StartCoroutine(StartRound(countdownSeconds));

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
