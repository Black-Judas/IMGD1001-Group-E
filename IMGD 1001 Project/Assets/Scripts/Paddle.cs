using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Paddle : MonoBehaviour
{

    public List<Modifier> modifiers = new List<Modifier>();
    public List<ModifierPanel> activeModifiers = new List<ModifierPanel>();


    public StatsList stats { get; protected set; }
    public List<string> currentStats;

    protected Rigidbody2D _rigidbody;
    public StatHandler statHandler { get; protected set; }
    public ModifierHandler modifierHandler { get; protected set; }

    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        statHandler = FindObjectOfType<StatHandler>();
        modifierHandler = FindObjectOfType<ModifierHandler>();

        //Initialize the player's stats
        stats = statHandler.GetStats(this);

        //Debug: Add a modifier to the player
        //modifierHandler.addModifier(this, new SpeedBuff());

    }

    public void UpdateStats()
    {
        stats = statHandler.GetStats(this);
    }

    public float GetStat(string name)
    {
        return stats.GetStat(name);
    }

    public void ResetPosition()
    {
        _rigidbody.position = new Vector2(_rigidbody.position.x, 0.0f);
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            OnBallHit(ball);
        }

    }

    private void OnBallHit(Ball ball)
    {
        BallImpactSound(ball);
        foreach (Modifier modifier in modifiers)
        {
            modifier.OnBallHit(ball);
        }
    }

    private void BallImpactSound(Ball ball)
    {
        //Check how fast the ball is going
        Ball.speedTier speedTier = ball.GetSpeedTier();
        //Debug.Log(speedTier);

        //Choose a random variant to play
        string variant = Random.Range(1, 5).ToString();


        //Determine what sound effect to play based on how fast the ball hits the paddle
        string soundToPlay = null;

        switch (speedTier)
        {
            case Ball.speedTier.Slow:
                soundToPlay = "hit" + "Light" + variant;
                break;
            case Ball.speedTier.Medium:
                soundToPlay = "hit" + "Medium" + variant;
                break;
            case Ball.speedTier.Fast:
                soundToPlay = "hit" + "Heavy" + variant;
                break;
            case Ball.speedTier.Lightning:
                AudioManager.instance.PlaySFX("ballLaunch"); //TODO: Figure out how to handle super fast impacts
                soundToPlay = "hit" + "Heavy" + variant;
                break;
        }


        //Play the sound
        AudioManager.instance.PlaySFX(soundToPlay, 1);
    }
}
