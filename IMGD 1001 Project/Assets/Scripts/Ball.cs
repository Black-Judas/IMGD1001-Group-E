using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 200f;

    public enum speedTier {
        Slow,
        Medium,
        Fast,
        Lightning
    }

    private speedTier currentSpeedTier = speedTier.Slow;

    [SerializeField] private ParticleSystem impactParticles;

    private Rigidbody2D _rigidbody;

    private ParticleSystem impactParticlesInstance;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GetVelocity().magnitude > 17)
        {
            currentSpeedTier = speedTier.Lightning;
        } 
        else if (GetVelocity().magnitude > 13)
        {
            currentSpeedTier = speedTier.Fast;
        }
        else if (GetVelocity().magnitude > 10)
        {
            currentSpeedTier = speedTier.Medium;
        }
        else
        {
            currentSpeedTier = speedTier.Slow;
        }

    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < -0.5f ? Random.Range(-1f, -0.5f) :
                                         Random.Range(0.5f, 1f);

        Vector2 direction = new Vector2(x, y);
        _rigidbody.AddForce(direction * this.speed);
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force);
    }

    public void ResetPosition()
    {
        _rigidbody.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Spawn in particles
        SpawnImpactParticles(collision);

    }

    private void SpawnImpactParticles(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;   //Determine contact point
        impactParticlesInstance = Instantiate(impactParticles, new Vector3(contactPoint.x, contactPoint.y, 0), Quaternion.identity);    //Spawn in particles at that point
    }

    public speedTier GetSpeedTier()
    {
        return this.currentSpeedTier;
    }
}
