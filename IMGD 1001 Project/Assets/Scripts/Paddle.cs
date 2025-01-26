using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;

    public Rigidbody2D ball;

    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _collider;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();


        if (ball != null)
        {
            float collisionHeight =  ball.transform.position.y - transform.position.y;
            Vector2 ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;

            Debug.Log("Point on paddle: " + collisionHeight);

            if (collisionHeight > 0.16)
            {
                ballVelocity.y = math.abs(ballVelocity.y);
            } else if (collisionHeight < -0.16)
            {
                ballVelocity.y = -math.abs(ballVelocity.y);
            }

            ball.GetComponent<Rigidbody2D>().velocity = ballVelocity;
        }
    }

    public void ResetPosition()
    {
        _rigidbody.position = new Vector2(_rigidbody.position.x, 0.0f);
        _rigidbody.velocity = Vector2.zero;
    }
}
