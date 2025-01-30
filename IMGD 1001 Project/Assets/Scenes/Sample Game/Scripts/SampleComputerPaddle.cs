using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleComputerPaddle : SamplePaddle
{
    public Rigidbody2D ball;

    private void FixedUpdate()
    {
        //Ball moving away
        if (this.ball.velocity.x > 0f)
        {
            if (this.ball.position.y > this.transform.position.y)
            {
                _rigidbody.AddForce(Vector2.up * this.speed);
            }
            else if (this.ball.position.y < this.transform.position.y)
            {
                _rigidbody.AddForce(Vector2.down * this.speed);
            }
        }

        //Ball moving towards
        else
        {
            if (this.transform.position.y > 0f)
            {
                _rigidbody.AddForce(Vector2.down * this.speed);
            }
            else if (this.transform.position.y < 0f)
            {
                _rigidbody.AddForce(Vector2.up * this.speed);
            }
        }
    }
}
