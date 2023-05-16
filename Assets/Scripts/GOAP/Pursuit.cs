using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    public override bool PrePerform() {
        return true;
    }

    public override bool PostPerform() {
        return true;
    }

    public override bool IsAchievable() {
        return Vector3.Distance(target.transform.position, transform.position) < 10;
    }

    private void Update() {
        anim.SetFloat("Speed", 2);
    }
}
