using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject largeAsteroid;
    public int spawnPerDestroyedAsteroids = 4;
    private int spawnedAsteroidsCount = 0;

    public void CheckSpawnAsteroid(int score)
    {
        int shouldBeSpawnedCount = score / spawnPerDestroyedAsteroids;
        int diff = shouldBeSpawnedCount - spawnedAsteroidsCount;
        if (diff > 0)
        {
            spawnedAsteroidsCount++;

            float angle = Random.Range(0, Mathf.PI * 2);
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 45;
            GameObject newAsteroid = Instantiate(largeAsteroid, pos, Quaternion.identity);
            Asteroid asteroid = newAsteroid.GetComponent<Asteroid>();
            asteroid.RandomizeStartVelocity = false;
            asteroid.MoveTowardsCentre();
        }
    }
}
