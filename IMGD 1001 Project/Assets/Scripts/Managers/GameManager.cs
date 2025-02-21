using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ballPrefab;
    public int scoreToWin = 5;
    public float countdownSeconds = 3f;

    //Player references
    public Paddle player1Paddle, player2Paddle;
    public Text player1MatchScoreText, player2MatchScoreText; //UI text for the player's match score
    private int _player1MatchScore, _player2MatchScore; //The player's match score
    public RoundScoreCounter player1RoundScoreCounter, player2RoundScoreCounter; //The players' round score counters
    

    //General UI references
    public TMP_Text countdownText;
    public GameObject debugMenu;
    public TMP_Text announcementText;
    public UpgradeSelectionScreen upgradeSelectionScreen;


    //Unity methods
    private void Start()
    {
        ToggleDebugMenu();
        upgradeSelectionScreen.gameObject.SetActive(false);
        StartCoroutine(ServeBall(countdownSeconds));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
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

    //Go to the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    //Game methods
    public void Player1Scores()
    {

        Debug.Log("Player 1 Scores");
        AudioManager.instance.PlaySFX("crowd");

        player1RoundScoreCounter.IncrementScore();

        //If the player scores 2 points, they win the round and their match score is incremented
        if (player1RoundScoreCounter.score >= 2)
        {
            _player1MatchScore++;
            player1MatchScoreText.text = _player1MatchScore.ToString();

            StartCoroutine(RoundWon(player1Paddle, player2Paddle));
        }
        else
        {
            ResetPlayArea();
            StartCoroutine(ServeBall(countdownSeconds));
        }

    }
    public void Player2Scores()
    {

        Debug.Log("Player 2 Scores");
        AudioManager.instance.PlaySFX("crowd", 1.5f);

        player2RoundScoreCounter.IncrementScore();

        //If the player scores 2 points, they win the round and their match score is incremented
        if (player2RoundScoreCounter.score >= 2)
        {
            _player2MatchScore++;
            player2MatchScoreText.text = _player2MatchScore.ToString();

            StartCoroutine(RoundWon(player2Paddle, player1Paddle));
        }
        else
        {
            ResetPlayArea();
            StartCoroutine(ServeBall(countdownSeconds));
        }

    }
    private void ResetPlayArea()
    {

        this.player1Paddle.ResetPosition();
        this.player2Paddle.ResetPosition();

    }
    

    //Coroutines
    IEnumerator Announce(string message, float duration = 4f)
    {
        announcementText.text = message;
        announcementText.enabled = true;
        yield return new WaitForSeconds(duration);
        announcementText.enabled = false;
    }

    IEnumerator RoundWon(Paddle winner, Paddle loser)
    {
        Coroutine coroutine;
        bool matchWon = false;

        //If a player has won the match, announce it and reset the match score. Otherwise, announce the round winner
        if (_player1MatchScore >= scoreToWin)
        {
            AudioManager.instance.PlaySFX("crowdLong", 1.5f);
            coroutine = StartCoroutine(Announce("Player 1 wins the match!", 6));
            _player1MatchScore = 0;
            _player2MatchScore = 0;
            player1MatchScoreText.text = _player1MatchScore.ToString();
            player2MatchScoreText.text = _player2MatchScore.ToString();
            matchWon = true;
        }
        else if (_player2MatchScore >= scoreToWin)
        {
            AudioManager.instance.PlaySFX("crowdLong", 1.5f);
            coroutine = StartCoroutine(Announce("Player 2 wins the match!"));
            _player1MatchScore = 0;
            _player2MatchScore = 0;
            player1MatchScoreText.text = _player1MatchScore.ToString();
            player2MatchScoreText.text = _player2MatchScore.ToString();
            matchWon = true;
        }
        else
        {
            coroutine = StartCoroutine(Announce(winner.gameObject.name + " wins the round!"));
        }

        //Start the upgrade selection screen after the announcement finishes, unless the match has been won
        yield return coroutine;
        if (matchWon)
        {
            GoToMainMenu();
            yield break;
        }
        ResetPlayArea();
        upgradeSelectionScreen.StartPicking(loser);

        //Wait until the player has selected their upgrade before continuing
        yield return new WaitUntil(() => upgradeSelectionScreen.gameObject.activeSelf == false);

        //Start the next round
        player1RoundScoreCounter.ResetScore();
        player2RoundScoreCounter.ResetScore();
        StartCoroutine(ServeBall(countdownSeconds));

    }

    public IEnumerator ServeBall(float countdownSeconds)
    {
        Ball ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        //Call the OnReset method for every modifier on both players
        foreach (Modifier modifier in player1Paddle.modifiers)
        {
            modifier.OnReset(ball);
        }
        foreach (Modifier modifier in player2Paddle.modifiers)
        {
            modifier.OnReset(ball);
        }


        Debug.Log("Starting countdown");
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

        Debug.Log("Serving ball");
        ball.AddStartingForce();

    }
}
