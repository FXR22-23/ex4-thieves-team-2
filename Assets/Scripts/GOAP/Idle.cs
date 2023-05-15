using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : GAction
{
    public override bool PrePerform() {
        return true;
    }

    public override bool PostPerform() {
        return true;
    }

    public override void Action() {
        StartCoroutine(Timer());
    }

    IEnumerator Timer() {
        actionComplete = false;
        yield return new WaitForSeconds(duration);
        actionComplete = true;
    }
}
