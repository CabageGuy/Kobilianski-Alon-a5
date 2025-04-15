using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float thrust = 3;
    [SerializeField] private float rotationSpeed = 360;
    [SerializeField] private float maxVelocity = 15;
    [SerializeField] private float bulletSpeed = 3;
    [SerializeField] private float bulletLifetime = 1;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameManager gameManager;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Spawn and shoot bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, bulletLifetime);

            // Play shooting sound
            audioSource?.PlayOneShot(shootClip);
        }
    }

    void FixedUpdate()
    {
        float rotation = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        rb2d.MoveRotation(transform.rotation.eulerAngles.z + rotation);

        if (Input.GetKey(KeyCode.Space) && rb2d.linearVelocity.magnitude < maxVelocity)
        {
            rb2d.AddForce(transform.up * thrust);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Asteroid")) return;

        gameManager.RemoveLife();

        if (gameManager.IsGameOver())
        {
            Destroy(gameObject);
            return;
        }

        rb2d.MovePosition(Vector2.zero);
    }
}
