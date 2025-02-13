using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Paddle : MonoBehaviour
{
    //Base stats
    public float baseSpeed = 10f;
    public float baseSize = 1f;

    //Current stats
    public float speed;
    public float size;

    public List<ModifierList> modifiers = new List<ModifierList>();

    private void Start()
    {
        SpeedBuff mod = new SpeedBuff();
        modifiers.Add(new ModifierList(mod, mod.Name, 2));
        speed = modifiers[0].modifier.StatChange(this, baseSpeed, 2);
    }


    protected Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //Initialize stats at their base value
        speed = baseSpeed;
        size = baseSize;
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

    private void OnBallHit(Ball ball = null)
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

    private void UpdateStats()
    {
        
    }
}
