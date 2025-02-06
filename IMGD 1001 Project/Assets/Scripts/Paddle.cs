using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    protected Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
            BallHit(ball);
        }

    }

    private void BallHit(Ball ball = null)
    {
        Debug.Log(ball.GetVelocity().magnitude);


        string speedTier = "Light";


        if (ball.GetVelocity().magnitude < 6)
        {
            speedTier = "Light";
        } else if (ball.GetVelocity().magnitude < 8)
        {
            speedTier = "Medium";
        } else {
            speedTier = "Heavy";
        }

        string variant = Random.Range(1, 5).ToString();

        string soundToPlay = "hit" + speedTier + variant;

        AudioManager.instance.PlaySFX(soundToPlay, 1);
    }
}
