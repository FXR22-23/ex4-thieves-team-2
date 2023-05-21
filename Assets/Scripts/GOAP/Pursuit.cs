using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    public float hearDistance = 20;
    public float detectionDistance = 10;

    public PlayerSoundManager targetSM;


    private bool enemySawPlayer;
    private bool enemyHeardPlayer;

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
        enemySawPlayer = Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance);
        if (enemySawPlayer) {
            // Check if the object hit is an enemy unit.
            if (hit.collider.gameObject.CompareTag("Player")) {
                // The character has detected an enemy unit!
                Debug.Log("Player detected!");
                targetSM.SetChaseParams(true, false);
                return true;
            }
        }

        enemyHeardPlayer = Vector3.Distance(target.transform.position, transform.position) < hearDistance;

        targetSM.SetChaseParams(false, enemyHeardPlayer);

        return enemyHeardPlayer;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
    }
}
