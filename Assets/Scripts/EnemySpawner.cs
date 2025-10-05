using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnerRatePerMinute = 30;
    public float spawnerRateIncrement = 1f;
    public float xlimit;
    public float maxLifeTime = 4f;

    private float spawnNext = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnerRatePerMinute;

            spawnerRatePerMinute += spawnerRateIncrement;

            float rand = Random.Range(-xlimit, xlimit);

            Vector2 spawnPosition = new Vector2(rand, 8f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxLifeTime);
        }
        
    }
}
