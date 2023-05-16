using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : GAction
{
    public override bool PrePerform() {
        anim.SetFloat("Speed", 1);
        return true;
    }

    public override bool PostPerform() {
        anim.SetFloat("Speed", 0);
        return true;
    }
}
