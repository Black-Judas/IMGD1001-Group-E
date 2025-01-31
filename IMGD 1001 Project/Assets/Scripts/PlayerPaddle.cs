using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;

    private void Update()
    {
        if (Input.GetKey(moveUp) )
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKey(moveDown))
        {
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }


    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0)
        {
            _rigidbody.AddForce(_direction * this.speed);
        }
    }
}
