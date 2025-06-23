using System.Collections;
using UnityEngine;

public class DanceCueSpawner : MonoBehaviour
{
    [Header("Arrow Spawning")]
    public GameObject arrowPrefab;
    public Transform spawnPoint;

    [Header("Timing Settings")]
    public float bpm = 120f; // Beats per minute
    public float spawnDelay = 0f; // Optional delay before starting
    public bool loopSpawning = true;

    private float spawnInterval;
    private Coroutine spawnRoutine;

    public void StartSpawning()
    {
        spawnInterval = 60f / bpm;

        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    private IEnumerator SpawnLoop()
    {
        if (spawnDelay > 0f)
            yield return new WaitForSeconds(spawnDelay);

        while (loopSpawning)
        {
            SpawnArrow();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnArrow()
    {
        if (arrowPrefab != null && spawnPoint != null)
        {
            Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("ArrowPrefab or SpawnPoint not assigned in DanceCueSpawner.");
        }
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
}
