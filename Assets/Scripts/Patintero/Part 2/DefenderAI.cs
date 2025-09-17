using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenderAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform patrolPoint;           // where defender returns to / patrols
    public float patrolSpeed = 2f;

    [Header("Detection")]
    public float detectionRadius = 6f;      // set also in trigger collider
    public float chaseSpeed = 4f;
    public float tagDistance = 1.2f;        // distance to tag player
    public LayerMask sightObstaclesMask;    // obstacles that block line of sight

    Transform player;
    Vector3 startPosition;
    bool playerDetected = false;
    Collider detectionTrigger;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPosition = patrolPoint ? patrolPoint.position : transform.position;

        // Ensure trigger collider is present and sized to detectionRadius
        detectionTrigger = GetComponent<Collider>();
        if (!detectionTrigger.isTrigger)
            Debug.LogWarning("Defender's collider should be set to 'Is Trigger' for detection to work reliably.");
    }

    void Update()
    {
        if (playerDetected && player != null)
        {
            // if line of sight blocked, consider lost (optional)
            if (!HasLineOfSightToPlayer())
            {
                playerDetected = false;
                StartCoroutine(ReturnToPatrol());
                return;
            }

            // chase player
            Vector3 dir = (player.position - transform.position);
            dir.y = 0;
            float dist = dir.magnitude;

            if (dist <= tagDistance)
            {
                // tag!
                OnTagPlayer();
            }
            else
            {
                Vector3 move = dir.normalized * chaseSpeed * Time.deltaTime;
                transform.position += move;
                transform.forward = dir.normalized;
            }
        }
        else
        {
            // patrol / idle near patrol point
            Vector3 dir = (startPosition - transform.position);
            dir.y = 0;
            if (dir.magnitude > 0.1f)
            {
                transform.position += dir.normalized * patrolSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, dir.normalized, Time.deltaTime * 6f);
            }
        }
    }

    bool HasLineOfSightToPlayer()
    {
        if (player == null) return false;
        Vector3 origin = transform.position + Vector3.up * 1.0f;
        Vector3 target = player.position + Vector3.up * 1.0f;
        Vector3 dir = (target - origin);
        float dist = dir.magnitude;
        if (Physics.Raycast(origin, dir.normalized, out RaycastHit hit, dist, sightObstaclesMask))
        {
            // something blocking view
            return false;
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // optional: check distance and LOS before committing
            if (HasLineOfSightToPlayer())
            {
                playerDetected = true;
                StopAllCoroutines();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // lose player when they exit trigger
            playerDetected = false;
            StartCoroutine(ReturnToPatrol());
        }
    }

    IEnumerator ReturnToPatrol()
    {
        // briefly look toward last known player position, then go back
        float wait = 0.2f;
        float t = 0;
        Vector3 lastDir = player ? (player.position - transform.position) : transform.forward;
        lastDir.y = 0;
        while (t < wait)
        {
            transform.forward = Vector3.Lerp(transform.forward, lastDir.normalized, Time.deltaTime * 4f);
            t += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    void OnTagPlayer()
    {
        // notify the GameManager
        GameManager.Instance?.OnPlayerTagged(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tagDistance);
    }
}
