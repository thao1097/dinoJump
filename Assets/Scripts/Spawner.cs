using UnityEngine;

public class Spawner : MonoBehaviour
{
    // reference to prefabs to spawn
    // spawn chance


    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    // min and max spawn rate, range: 1-2seconds
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke(); // stops spawning
    }

    private void Spawn()
    {
        // pick random float 0-1
        float spawnChance = Random.value;
        // loop through each object
        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                // instantiate creates copy of the prefab
                GameObject obstacle = Instantiate(obj.prefab);
                // set position of obstacle: offset of Spawner's position (right)
                obstacle.transform.position += transform.position;
                break; // cancel loop, don't want multiple 
            }

            spawnChance -= obj.spawnChance;
            // example loop
            // random value = 1, not less than 0.2:
            // 1-0.2 = 0.8, not less than 0.18:
            // 0.8 - 0.18 = 0.62, not less than 0.16:
            // 0.62-0.16 = 0.46, not less than 0.14:
            // 0.32 not less than 0.12:
            // 0.2 not less 0.1:
            // 0.1 not less than 0.08:
            // didn't match any of them, so nothing is spawned 
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    
    }
    


}
