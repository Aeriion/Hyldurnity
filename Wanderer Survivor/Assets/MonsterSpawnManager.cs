using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class MonsterData
    {
        public GameObject monsterPrefab;
        public float probability;
    }

    public class Wave
    {
        public MonsterData[] monsters;
        public int numberOfMonsters;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 1f;
    public float spawnRateIncreasePerWave = 0.1f;
    public float maxSpawnDistance = 20f; // Maximum distance from the player to spawn monsters
    public LayerMask terrainLayerMask; // Layer mask for terrain objects
    public Text waveCountText; // Reference to the Text for the wave counter

    private int currentWaveIndex = 0;
    private int waveCount = 1;
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateWaveCountText();
        StartNextWave();
    }

    private void Update()
    {
        // Check if all monsters of the current wave are defeated
        if (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            if (AreAllMonstersDefeated(currentWave))
            {
                waveCount++;
                UpdateWaveCountText();

                if (waveCount % 10 == 0 && waveCount < 90) // Check if it's a multiple of 10 (mini-boss wave) and not the final boss wave (vague 90)
                {
                    StartMiniBossWave();
                }
                else
                {
                    StartNextWave();
                }
            }
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            int totalMonstersCount = currentWave.numberOfMonsters;
            foreach (MonsterData monsterData in currentWave.monsters)
            {
                totalMonstersCount -= Mathf.FloorToInt(monsterData.probability * totalMonstersCount);
            }

            List<GameObject> monstersToSpawn = new List<GameObject>();
            foreach (MonsterData monsterData in currentWave.monsters)
            {
                int instancesCount = Mathf.FloorToInt(monsterData.probability * currentWave.numberOfMonsters);
                if (totalMonstersCount > 0)
                {
                    instancesCount += 1;
                    totalMonstersCount--;
                }

                for (int i = 0; i < instancesCount; i++)
                {
                    monstersToSpawn.Add(monsterData.monsterPrefab);
                }
            }

            while (monstersToSpawn.Count < currentWave.numberOfMonsters)
            {
                monstersToSpawn.Add(currentWave.monsters[Random.Range(0, currentWave.monsters.Length)].monsterPrefab);
            }

            for (int i = 0; i < currentWave.numberOfMonsters; i++)
            {
                GameObject monsterPrefab = monstersToSpawn[i];
                Transform spawnPoint = GetRandomSpawnPoint();
                Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            }

            currentWaveIndex++;
            timeBetweenSpawns *= (1f + spawnRateIncreasePerWave);
        }
    }

    private void StartMiniBossWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            int totalMonstersCount = currentWave.numberOfMonsters;
            foreach (MonsterData monsterData in currentWave.monsters)
            {
                totalMonstersCount -= Mathf.FloorToInt(monsterData.probability * totalMonstersCount);
            }

            List<GameObject> monstersToSpawn = new List<GameObject>();
            foreach (MonsterData monsterData in currentWave.monsters)
            {
                int instancesCount = Mathf.FloorToInt(monsterData.probability * currentWave.numberOfMonsters);
                if (totalMonstersCount > 0)
                {
                    instancesCount += 1;
                    totalMonstersCount--;
                }

                for (int i = 0; i < instancesCount; i++)
                {
                    monstersToSpawn.Add(monsterData.monsterPrefab);
                }
            }

            while (monstersToSpawn.Count < currentWave.numberOfMonsters)
            {
                monstersToSpawn.Add(currentWave.monsters[Random.Range(0, currentWave.monsters.Length)].monsterPrefab);
            }

            // Spawn additional mini-boss
            Wave miniBossWave = waves[currentWaveIndex - 1]; // Previous wave
            GameObject miniBossPrefab = miniBossWave.monsters[Random.Range(0, miniBossWave.monsters.Length)].monsterPrefab;
            Transform spawnPoint = GetRandomSpawnPoint();
            Instantiate(miniBossPrefab, spawnPoint.position, Quaternion.identity);

            for (int i = 0; i < currentWave.numberOfMonsters; i++)
            {
                GameObject monsterPrefab = monstersToSpawn[i];
                spawnPoint = GetRandomSpawnPoint();
                Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            }

            currentWaveIndex++;
            timeBetweenSpawns *= (1f + spawnRateIncreasePerWave);
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        Vector3 randomPosition = Vector3.zero;
        int maxAttempts = 10;
        int currentAttempt = 0;

        do
        {
            randomPosition = GetRandomPositionAroundPlayer();
            currentAttempt++;
        } while (!IsPositionValid(randomPosition) && currentAttempt < maxAttempts);

        if (currentAttempt >= maxAttempts)
        {
            // Fallback to a random spawn point if the maxAttempts are reached and no valid position is found
            return spawnPoints[Random.Range(0, spawnPoints.Length)];
        }

        GameObject terrainObject = GetTerrainObject(randomPosition);
        if (terrainObject != null)
        {
            return terrainObject.transform;
        }

        // Fallback to a random spawn point if no terrain object is found
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * maxSpawnDistance;
        return playerTransform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check if the position is within the maxSpawnDistance from the player
        return Vector3.Distance(playerTransform.position, position) <= maxSpawnDistance;
    }

    private GameObject GetTerrainObject(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private bool AreAllMonstersDefeated(Wave wave)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        int count = 0;
        foreach (GameObject monster in monsters)
        {
            if (IsMonsterInWave(monster, wave))
            {
                count++;
            }
        }
        return count == 0;
    }

    private bool IsMonsterInWave(GameObject monster, Wave wave)
    {
        foreach (MonsterData monsterData in wave.monsters)
        {
            if (monster.CompareTag(monsterData.monsterPrefab.tag))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateWaveCountText()
    {
        waveCountText.text = "Wave: " + waveCount;
    }
}