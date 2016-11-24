using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public abstract class EventAct : TemporalAct {

    protected GameObject obj = null;
    protected GameObject source = null;

    private short objId = -1;
    private short sourceId = -1;

    public EventAct(short objId, short sourceId) {
        this.objId    = objId;
        this.sourceId = sourceId;
    }

    public override void Init() {
        obj = Field.Instance.GetObject(objId);
        source = Field.Instance.GetObject(sourceId);
    }
}


public class RemoveAct : EventAct {

    private Vector3 start = Vector3.zero;
    private Vector3 end = Vector3.zero;

    public RemoveAct(short objId, short sourceId) : base(objId, sourceId) {}

    public override void Init() {
        base.Init();

//        start = obj.transform.localPosition;
//        end = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y - 10, obj.transform.localPosition.z);
        start = new Vector3(1, 1, 1);
        end = new Vector3(0, 0, 0);
    }

    public override void Perform(float step) {
        step = step * step * step;// * (step * (6.0f * step - 15.0f) + 10.0f);
        obj.transform.localScale = Vector3.Lerp(start, end, step);
    }


}


public class CollectAct : EventAct {

    public CollectAct(short objId, short sourceId) : base(objId, sourceId) {}

    public override void Perform(float step) {
    }
}


public class TriggerEnterAct : EventAct {

    public TriggerEnterAct(short objId, short sourceId) : base(objId, sourceId) {}

    public override void Perform(float step) {
    }
}


public class TriggerLeaveAct : EventAct {

    public TriggerLeaveAct(short objId, short sourceId) : base(objId, sourceId) {}

    public override void Perform(float step) {
    }
}
