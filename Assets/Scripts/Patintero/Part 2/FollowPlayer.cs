using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
private Transform Player;
    private NavMeshAgent defender;

    void Start()
    {
        // Get NavMeshAgent on this GameObject
        defender = GetComponent<NavMeshAgent>();

        // Find the player automatically by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player GameObject has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (Player != null && defender != null)
        {
            defender.SetDestination(Player.position);
        }
    }

}
