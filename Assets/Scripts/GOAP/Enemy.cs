using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("Idle", 1, true);
        SubGoal findPlayer = new SubGoal("Pursuit", 2, true);
        
        goals.Add(s1, 1);
        goals.Add(findPlayer, 10);
    }

    //private void Update() {
    //    if (Vector3.Distance(player.position, transform.position) < 10) {
    //        SubGoal pursuit = new SubGoal("Pursuit", 2, true);
    //        goals.Add(pursuit, 1);
    //        Debug.Log("hi");
    //    }
    //}
}
