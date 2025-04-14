using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float minSpeed = 1;
    public float maxSpeed = 5;
    public int subdivisionLevel = 0;
    public GameObject[] asteroids;
    public bool RandomizeStartVelocity = true;

    void Start()
    {
        if (RandomizeStartVelocity)
        {
            // Create random point on unit circle
            float randomAngle = Random.Range(0, Mathf.PI * 2);
            Vector2 direction = new(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            // Create force vector
            float speed = Random.Range(minSpeed, maxSpeed);
            Vector2 force = direction * speed;

            // Apply force all at once (impulse)
            rb2d.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Don't run code past this check if not a bullet
        bool isBullet = collider2D.gameObject.CompareTag("Bullet");
        if (!isBullet)
            return;

        // Create subdivided asteroids
        int asteroidsIndex = subdivisionLevel + 1;
        // Spawn 2 asteroids at that level
        if (asteroidsIndex < asteroids.Length)
        {
            for (int i = 0; i < 2; i++)
            {
                // Get prefab
                GameObject smallerAsteroidPrefb = asteroids[asteroidsIndex];
                // Create a clone of prefab
                Vector3 position = transform.position;
                Quaternion rotation = transform.rotation;
                GameObject newAsteroid = Instantiate(smallerAsteroidPrefb, position, rotation);
                // Apply velocity to new asteroid, rotate velocity a bit
                Rigidbody2D asteroidRB2D = newAsteroid.GetComponent<Rigidbody2D>();
                Quaternion variance = Quaternion.Euler(0, 0, Random.Range(-45, 45));
                asteroidRB2D.linearVelocity = variance * rb2d.linearVelocity;
            }
        }
        
        // Add score
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore();

        // Destroy bullet, then this asteroid
        Destroy(collider2D.gameObject);
        Destroy(this.gameObject);

    }

    public void MoveTowardsCentre()
    {
        // Create random point on unit circle
        Vector3 target = Random.insideUnitCircle * 5;
        // Get direction to raomish point in centre
        Vector3 direction = (target - transform.position).normalized;
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector2 force = direction * speed;
        // Apply force
        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }
}
