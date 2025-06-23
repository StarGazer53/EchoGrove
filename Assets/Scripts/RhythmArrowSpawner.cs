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
    public List<float> beatTimings = new List<float>()
    {
        0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f,
        5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f, 8.5f, 9.0f, 9.5f, 10.0f,
        10.5f, 11.0f, 11.5f, 12.0f, 12.5f, 13.0f, 13.5f, 14.0f, 14.5f, 15.0f,
        15.5f, 16.0f, 16.5f, 17.0f, 17.5f, 18.0f, 18.5f, 19.0f, 19.5f, 20.0f,
        20.5f, 21.0f, 21.5f, 22.0f, 22.5f, 23.0f, 23.5f, 24.0f, 24.5f, 25.0f,
        25.5f, 26.0f, 26.5f, 27.0f, 27.5f, 28.0f, 28.5f, 29.0f, 29.5f, 30.0f
    };

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
