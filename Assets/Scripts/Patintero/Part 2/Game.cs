using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager: MonoBehaviour
{
    // Start is called before the first frame update
  public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnPlayerTagged(DefenderAI defender)
    {
        Debug.Log($"Player tagged by defender: {defender.name}");
        // Handle logic: reset player to start, lose a life, scoreboard update, etc.
        // Example: reload scene or move player to safe area
    }

    public void OnRunnerCrossedLine(Transform line)
    {
        Debug.Log("Runner crossed a line: " + line.name);
        // handle scoring
    }
}
