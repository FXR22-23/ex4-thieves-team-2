using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GAgent
{    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("Idle", 1, true);
        SubGoal findPlayer = new SubGoal("Pursuit", 2, true);
        
        goals.Add(s1, 1);
        goals.Add(findPlayer, 10);
    }
}
