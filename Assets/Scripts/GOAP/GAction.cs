using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;
    public Animator anim;

    public Dictionary<string, int> preconditions; // states that can lead to this state
    public Dictionary<string, int> aftereffects; // states this state can lead into

    public WorldStates agentBeliefs;

    public bool running = false; // are we running this action at the moment?

    public GAction() {
        preconditions = new Dictionary<string, int>();
        aftereffects = new Dictionary<string, int>();
    }

    public virtual void Action() {
        agent.SetDestination(target.transform.position);
    }

    public void Awake() {
        anim = GetComponentInChildren<Animator>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if (preConditions != null) {
            foreach (WorldState w in preConditions) {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null) {
            foreach (WorldState w in afterEffects) {
                aftereffects.Add(w.key, w.value);
            }
        }
    }

    public virtual bool IsAchievable() {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions) {
        foreach (KeyValuePair<string, int> p in preconditions) {
            if (!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
