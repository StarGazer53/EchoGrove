using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmArrowSpawner : MonoBehaviour
{
    [Header("Script")]
    public AudioSource musicSource;
    public GameObject arrowPrefab;
    public Transform spawnPoint;

    [Header("Beat Timings (in seconds)")]
    public List<float> beatTimings = new List<float>();

    private int currentIndex = 0;
    private float songStartTime;
    private bool isSpawning = false;

    void Start()
    {
        // Optional: Auto-start if audio source is already playing
        if (musicSource != null && musicSource.isPlaying)
        {
            BeginSpawning();
        }
    }

    public void BeginSpawning()
    {
        if (musicSource == null || arrowPrefab == null || spawnPoint == null || beatTimings.Count == 0)
        {
            Debug.LogWarning("RhythmArrowSpawner: Missing references or beat timings.");
            return;
        }

        songStartTime = Time.time;
        currentIndex = 0;
        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (currentIndex < beatTimings.Count)
        {
            float targetTime = songStartTime + beatTimings[currentIndex];
            float waitTime = targetTime - Time.time;

            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            SpawnArrow();
            currentIndex++;
        }

        isSpawning = false;
    }

    private void SpawnArrow()
    {
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
