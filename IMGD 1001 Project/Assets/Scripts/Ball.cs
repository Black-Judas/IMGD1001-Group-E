using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 200f;
    public bool hasBeenHit = false; // sees if the ball has been hit this point

    public enum speedTier
    {
        Slow,
        Medium,
        Fast,
        Lightning
    }

    private speedTier currentSpeedTier = speedTier.Slow;

    [SerializeField] private ParticleSystem impactParticles;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    public TrailRenderer _trailRenderer { get; private set; }

    private ParticleSystem impactParticlesInstance;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponent<TrailRenderer>();
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
        hasBeenHit = false;
        _rigidbody.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;

        //Set transparency back to 1
        Color color = _spriteRenderer.color;
        color.a = 1;
        _spriteRenderer.color = color;

        //Set size back to .25
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

    }

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    public Vector2 GetPosition()
    {
        return _rigidbody.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Spawn in particles
        SpawnImpactParticles(collision);

        Paddle paddle = collision.gameObject.GetComponent<Paddle>();

        if (paddle == null)
        {
            WallImpactSound();
        }

    }
    private void WallImpactSound()
    {
        string soundToPlay;

        switch (GetSpeedTier())
        {
            case speedTier.Slow:
                soundToPlay = "wallLight";
                break;
            case speedTier.Medium:
                soundToPlay = "wallMedium";
                break;
            case speedTier.Fast:
                soundToPlay = "wallHeavy";
                break;
            default:
                soundToPlay = "wallHeavy";
                break;
        }

        AudioManager.instance.PlaySFX(soundToPlay);
    }
    private void SpawnImpactParticles(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;   //Determine contact point
        impactParticlesInstance = Instantiate(impactParticles, new Vector3(contactPoint.x, contactPoint.y, 1), Quaternion.identity);    //Spawn in particles at that point
    }

    public speedTier GetSpeedTier()
    {
        return this.currentSpeedTier;
    }

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
