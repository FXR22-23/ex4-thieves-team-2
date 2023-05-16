using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : GAction
{
    public override bool PrePerform() {
        anim.SetFloat("Speed", 2);
        return true;
    }

    public override bool PostPerform() {
        anim.SetFloat("Speed", 0);
        return true;
    }

    public override bool IsAchievable() {
        return Vector3.Distance(target.transform.position, transform.position) < 10;
    }
}
