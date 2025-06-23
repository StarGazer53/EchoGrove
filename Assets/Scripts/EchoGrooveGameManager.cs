using System.Collections;
using UnityEngine;

public class EchoGrooveGameManager : MonoBehaviour
{
    [Header("Audio & Gameplay")]
    public AudioSource musicSource;
    public GameObject difficultyOrbsGroup;
    public GameObject arrowSpawner;

    [Header("UI Transitions")]
    public ScreenFader screenFader;

    private bool songSelected = false;
    private bool difficultyChosen = false;

    void Start()
    {
        difficultyOrbsGroup.SetActive(false);
    }

    // Called by the "Dawn Of Change" button
    public void OnSongSelected()
    {
        songSelected = true;
        Debug.Log("Song selected!");

        difficultyOrbsGroup.SetActive(true);
    }

    // Called by grabbing an orb (triggered via XR interaction)
    public void OnDifficultyChosen()
    {
        if (!songSelected || difficultyChosen)
            return;

        difficultyChosen = true;
        Debug.Log("Difficulty chosen. Starting game...");

        StartGame();
    }

    private void StartGame()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        if (musicSource != null)
            musicSource.Play();

        if (arrowSpawner != null)
        {
            // Attempt RhythmArrowSpawner first
            RhythmArrowSpawner rhythm = arrowSpawner.GetComponent<RhythmArrowSpawner>();
            if (rhythm != null)
            {
                rhythm.BeginSpawning();
                yield break;
            }

            // Fallback to DanceCueSpawner
            DanceCueSpawner dance = arrowSpawner.GetComponent<DanceCueSpawner>();
            if (dance != null)
            {
                dance.StartSpawning();
            }
        }
    }
}
