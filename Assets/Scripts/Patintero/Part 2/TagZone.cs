using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagZone : MonoBehaviour
{
    // Start is called before the first frame update
   public enum ZoneType { RunnerLine, DefenderLine }
    public ZoneType zoneType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && zoneType == ZoneType.RunnerLine)
        {
            GameManager.Instance?.OnRunnerCrossedLine(transform);
        }
    }
}
