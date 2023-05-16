using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal {
    public Dictionary<string, int> sgoals;
    public bool remove;
    public SubGoal(string s, int i, bool r) {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    protected virtual void Start() {
        GAction[] acts = GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
    }

    bool invoked = false;
    void CompleteAction() {
        currentAction.running = false;
        currentAction.anim.SetFloat("Speed", 0);
        currentAction.PostPerform();
        invoked = false;
    }

    private void LateUpdate() {
        if (currentAction != null && currentAction.running) {
            float distToTarget = Vector3.Distance(currentAction.target.transform.position, transform.position);
            if(currentAction.agent.hasPath && distToTarget < 1) {
                if (!invoked) {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null) {
            planner = new GPlanner();
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals) {
                actionQueue = planner.plan(actions, sg.Key.sgoals, null);
                if (actionQueue != null) {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0) {
            Debug.Log("complete");
            if (currentGoal.remove) {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0) {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform()) {
                if (currentAction.target == null && currentAction.targetTag != "") {
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);
                }

                if (currentAction.target != null) {
                    currentAction.running = true;
                    //currentAction.agent.SetDestination(currentAction.target.transform.position);
                    currentAction.Action();
                }
            }
            else {
                actionQueue = null;
            }
        }
    }
}
