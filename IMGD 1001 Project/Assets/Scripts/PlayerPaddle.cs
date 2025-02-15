using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;

    public bool debugMode = false;
    public Ball ball;


    private void Update()
    {
        if (Input.GetKey(moveUp))
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

        if (debugMode == true)
        {
            transform.position = new Vector3(transform.position.x, ball.transform.position.y, transform.position.z);
        }

        //Update the player's y scale based on their scale stat
        transform.localScale = new Vector3(transform.localScale.x, stats.GetStat("size"), transform.localScale.z);

        //Display current stats in new list in inspector
        currentStats = new List<string>();
        foreach (Stat stat in stats.stats)
        {
            currentStats.Add(stat._name.FirstCharacterToUpper() + ": " + stat._value);
        }

    }


    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0)
        {
            _rigidbody.AddForce(_direction * this.stats.GetStat("speed"));
        }
    }
}
