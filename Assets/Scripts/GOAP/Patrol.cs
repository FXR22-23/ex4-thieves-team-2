using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : GAction
{
    public override bool PrePerform() {
        return true;
    }

    public override bool PostPerform() {
        return true;
    }

    private void Update() {
        anim.SetFloat("Speed", 1);
    }
}
