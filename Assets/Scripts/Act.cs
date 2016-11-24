using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public enum ActType {
    ATOMIC = 0,
    TEMPORAL = 1,
    GROUP = 2,
}


public abstract class Act {
    public abstract ActType Type { get; }
    public virtual void Init() {}
    public abstract void Perform(float value);
}


public abstract class AtomicAct : Act {
    private bool finished = false;

    public override ActType Type { get { return ActType.ATOMIC; } }

    public override void Perform(float value) {
        if (finished) {
            return;
        }

        Perform();
        finished = true;
    }

    public abstract void Perform();
}


public abstract class TemporalAct : Act {
    public override ActType Type { get { return ActType.TEMPORAL; } }
}


public class ActGroup : Act {

    public override ActType Type { get { return ActType.GROUP; } }

    private float curTime = 0.0f;
    private float transTime = 0.2f;

    private bool finished = false;
    public bool Finished { get { return finished; } }

    private List<Act> acts = new List<Act>();

    public ActGroup() {
    }

    public ActGroup(Act act) {
        acts.Add(act);
    }

    public ActGroup(List<Act> acts) {
        this.acts.AddRange(acts);
    }

    public void Add(Act act) {
        acts.Add(act);
    }

    public override void Init() {
        foreach(Act act in acts) {
            act.Init();
        }
    }

    public override void Perform(float deltaTime) {
        if (finished) {
            return;
        }

        curTime += deltaTime;

        float step = Mathf.Clamp(curTime / transTime, 0.0f, 1.0f);
        foreach(Act act in acts) {
            act.Perform(step);
        }

        if (curTime >= transTime) {
            finished = true;
        }

    }
}


public class ActQueue {
    private List<ActGroup> actGroups = new List<ActGroup>();

    public bool Empty { get { return actGroups.Count == 0; } }

    public void Add(ActGroup actGroup) {
        actGroups.Add(actGroup);
    }

    public ActGroup CurrentActGroup { get { return actGroups.Count == 0 ? null : actGroups[0]; } }

    public ActGroup Pop() {
        if (actGroups.Count == 0) {
            return null;
        }

        ActGroup result = actGroups[0];
        actGroups.RemoveAt(0);
        return result;
    }
}


public class Actor : MonoBehaviour {

    private int freezeCount = 0;
    private ActQueue actQueue = new ActQueue();
    private ActGroup curActGroup = null;
    private ActGroup frozenActGroup = null;

    protected void AddAct(Act act) {
        if (freezeCount == 0) {
            lock (actQueue) {
                actQueue.Add(new ActGroup(act));
            }
        } else {
            lock (frozenActGroup) {
                frozenActGroup.Add(act);
            }
        }
    }

    public virtual void Freeze() {
        if (freezeCount == 0) {
            frozenActGroup = new ActGroup();
        }

        freezeCount++;
    }

    public virtual void Thaw() {
        if (freezeCount == 0) {
            return;
        }

        freezeCount--;

        if (freezeCount > 0) {
            return;
        }

        lock (actQueue) {
            actQueue.Add(frozenActGroup);
        }

        frozenActGroup = null;
    }

    void Update()
    {
        if (curActGroup == null && !actQueue.Empty) {
            lock (actQueue) {
                curActGroup = actQueue.Pop();
                curActGroup.Init();
            }
        }

        if (curActGroup != null) {
            curActGroup.Perform(Time.deltaTime);
            if (curActGroup.Finished) {
                curActGroup = null;
            }
        }
    }

}
