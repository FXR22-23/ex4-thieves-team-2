using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SubGoal patrol = new SubGoal("Patrol", 0, false);
        SubGoal findPlayer = new SubGoal("Pursuit", 2, true);

        goals.Add(patrol, 1);
        goals.Add(findPlayer, 10);
    }
}
