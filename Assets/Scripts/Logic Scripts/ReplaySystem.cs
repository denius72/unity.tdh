using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour
{
    private List<GameSnapshot> snapshots = new List<GameSnapshot>();

    private bool isRecording = false;
    private int replayIndex = 0;
    private bool isReplaying = false;
    private float replayTimer = 0f;

    private void Update()
    {
        if (isRecording)
        {
            RecordSnapshot();
        }

        if (isReplaying)
        {
            Replay();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRecording();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleReplay();
        }
    }

    private void RecordSnapshot()
    {
        GameSnapshot snapshot = new GameSnapshot();
        // Capture the necessary game state into the snapshot
        // For example, store player position, enemy positions, score, etc.

        snapshot.timeStamp = Time.realtimeSinceStartup;
        snapshots.Add(snapshot);
    }

    private void Replay()
    {
        if (replayIndex < snapshots.Count)
        {
            GameSnapshot snapshot = snapshots[replayIndex];

            if (Time.realtimeSinceStartup - replayTimer >= snapshot.timeStamp)
            {
                // Apply the captured game state from the snapshot
                // For example, restore player position, enemy positions, score, etc.

                replayIndex++;
            }
        }
        else
        {
            StopReplay();
        }
    }

    private void ToggleRecording()
    {
        isRecording = !isRecording;
        if (isRecording)
        {
            snapshots.Clear();
        }
    }

    private void ToggleReplay()
    {
        if (!isReplaying && snapshots.Count > 0)
        {
            isReplaying = true;
            replayIndex = 0;
            replayTimer = Time.realtimeSinceStartup;
            // Prepare the game state for replay
            // For example, reset player position, enemy positions, score, etc.
        }
    }

    private void StopReplay()
    {
        isReplaying = false;
        // Perform any necessary cleanup or reset after replay
    }
}

public class GameSnapshot
{
    // Define the properties and data you want to capture for the game state
    // For example, player position, enemy positions, score, etc.
    public float timeStamp;
}