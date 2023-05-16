using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    public float hearDistance = 15;
    public float detectionDistance = 10;

    public override bool PrePerform() {
        anim.SetFloat("Speed", 2);
        return true;
    }

    public override bool PostPerform() {
        anim.SetFloat("Speed", 0);
        return true;
    }

    public override bool IsAchievable() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance)) {
            // Check if the object hit is an enemy unit.
            if (hit.collider.gameObject.CompareTag("Player")) {
                // The character has detected an enemy unit!
                Debug.Log("Player detected!");
                return true;
            }
        }

        return Vector3.Distance(target.transform.position, transform.position) < hearDistance;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
    }
}
