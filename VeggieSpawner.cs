using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieSpawner : MonoBehaviour
{
    public GameObject[] vegetables;         // Array to hold regular vegetable prefabs
    public GameObject RottenCucumberPrefab;  // Prefab for the rotten cucumber
    public float initialSpawnRate = 2f;      // Initial time between spawns
    public float maxSpawnRate = 1.5f;        // Minimum possible time between spawns (controls spawn rate progression)
    public float xMin = -5f;                 // Min x position to spawn
    public float xMax = 5f;                  // Max x position to spawn
    public float ySpawnPosition = 8f;        // Y position above the screen to spawn
    public float yDespawnPosition = -6f;     // Y position below which vegetables despawn
    public float minFallSpeed = 20f;         // Slower initial falling speed
    public float maxFallSpeed = 30f;         // Reduced max fall speed for smoother gameplay
    public float maxFallSpeedIncrement = 15f; // Lowered max fall speed increment for gradual difficulty increase
    public float difficultyIncreaseInterval = 60f; // Increased interval for slower scaling
    public float rottenSpawnChance = 0.1f;   // 10% chance to spawn a rotten cucumber
    public int maxSimultaneousSpawns = 1;    // Controls the maximum number of vegetables to spawn at once

    private float nextSpawnTime;
    private float elapsedTime = 0f;          // Tracks time for difficulty scaling
    private List<GameObject> activeVegetables = new List<GameObject>();

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Time.time >= nextSpawnTime)
        {
            SpawnVegetables();
            // Adjust spawn rate dynamically
            float spawnInterval = Mathf.Clamp(initialSpawnRate - (elapsedTime / difficultyIncreaseInterval), maxSpawnRate, initialSpawnRate);
            nextSpawnTime = Time.time + spawnInterval;
        }

        CheckForFallenVegetables();
    }

    void SpawnVegetables()
    {
        int numVegetablesToSpawn = Mathf.Min(Random.Range(1, maxSimultaneousSpawns + 1), vegetables.Length);

        for (int i = 0; i < numVegetablesToSpawn; i++)
        {
            GameObject veg;

            // Decide if the next vegetable should be rotten
            if (Random.value < rottenSpawnChance)
            {
                veg = Instantiate(RottenCucumberPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            }
            else
            {
                int randomIndex = Random.Range(0, vegetables.Length);
                veg = Instantiate(vegetables[randomIndex], GetRandomSpawnPosition(), Quaternion.identity);
            }

            // Add VegetableStatus and set up the object
            VegetableStatus status = veg.GetComponent<VegetableStatus>() ?? veg.AddComponent<VegetableStatus>();
            status.hasSpawned = true;
            activeVegetables.Add(veg);

            // Set random fall speed, adjusted for difficulty
            Rigidbody2D rb = veg.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomSpeed = Mathf.Clamp(minFallSpeed + (elapsedTime / difficultyIncreaseInterval) * maxFallSpeedIncrement, minFallSpeed, maxFallSpeed);
                rb.velocity = new Vector2(0, -randomSpeed);
            }

            StartCoroutine(ActivateVegetable(status, veg));
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(xMin, xMax);
        return new Vector3(randomX, ySpawnPosition, 0);
    }

    IEnumerator ActivateVegetable(VegetableStatus status, GameObject veg)
    {
        yield return new WaitForSeconds(1f);  // Ensure full visibility before activation
        status.isActive = true;
    }

    void CheckForFallenVegetables()
{
    for (int i = activeVegetables.Count - 1; i >= 0; i--)
    {
        GameObject veg = activeVegetables[i];
        VegetableStatus status = veg.GetComponent<VegetableStatus>();

        if (status == null) continue;

        if (status.hasSpawned && status.isActive && veg.transform.position.y < yDespawnPosition && !status.isSliced)
        {
            // Check if it's the Rotten Cucumber and skip life deduction if it is
            if (veg.name == "RottenCucumber(Clone)")
            {
                // Just destroy the Rotten Cucumber without deducting a life
                Destroy(veg);
                activeVegetables.RemoveAt(i);
                continue;
            }

            // Deduct a life for other vegetables
            LivesManager livesManager = FindObjectOfType<LivesManager>();
            if (livesManager != null)
            {
                livesManager.MissVegetable();
            }
            Destroy(veg);
            activeVegetables.RemoveAt(i);
        }
        else if (status.isSliced)
        {
            activeVegetables.RemoveAt(i);
            Destroy(veg);
        }
    }
}

}
