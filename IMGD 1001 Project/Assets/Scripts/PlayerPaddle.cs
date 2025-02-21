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



    private void Update()
    {
        ball = FindObjectOfType<Ball>();
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

        if (debugMode == true && ball != null)
        {
            transform.position = new Vector3(transform.position.x, ball.GetPosition().y, transform.position.z);
        }

        //Update the player's y scale based on their scale stat
        transform.localScale = new Vector3(transform.localScale.x, stats.GetStat("size"), transform.localScale.z);

        //Display current stats in new list in inspector
        currentStats = new List<string>();
        foreach (Stat stat in stats.stats)
        {
            currentStats.Add(stat._name.FirstCharacterToUpper() + ": " + stat._value);
        }
        foreach (Modifier modifier in modifiers)
        {
            if (ball != null) modifier.OnUpdate(this.ball);
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
