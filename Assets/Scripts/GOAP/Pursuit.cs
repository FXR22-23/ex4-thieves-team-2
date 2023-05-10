using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    Transform player;

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
        return Vector3.Distance(player.position, transform.position) < 10;
    }
}
