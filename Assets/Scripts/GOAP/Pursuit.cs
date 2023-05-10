using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    Transform player;
    [SerializeField] float hearingDist = 5;
    [SerializeField] float sightDist = 10;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override bool PrePerform() {
        return true;
    }

    public override bool PostPerform() {
        return true;
    }

    public override bool IsAchievable() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, sightDist)) {
            // Check if the object hit is an enemy unit.
            if (hit.collider.gameObject.CompareTag("Player")) {
                // The character has detected an enemy unit!
                return true;
            }
        }
        return Vector3.Distance(player.position, transform.position) < hearingDist;
    }

    private void Update() {
        // running aniamtion
    }
}
