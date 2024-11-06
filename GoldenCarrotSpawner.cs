using UnityEngine;

public class GoldenCarrotSpawner : MonoBehaviour
{
    public GameObject GoldenCarrot_Sprite; // Prefab of the Golden Carrot
    public float spawnInterval = 20f; // Time between each spawn
    public float spawnRangeX = 7f; // X range for random spawn position

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Spawn the Golden Carrot at intervals
        if (timer >= spawnInterval)
        {
            SpawnGoldenCarrot();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnGoldenCarrot()
    {
        // Generate a random X position for the carrot to spawn
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, 6f, 0f); // Spawn from the top of the screen

        // Instantiate the Golden Carrot at the random position
        Instantiate(GoldenCarrot_Sprite, spawnPosition, Quaternion.identity);
    }
}
