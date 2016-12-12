using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public abstract class MotionAct : TemporalAct {

    protected GameObject obj = null;
    protected Vector3 start = Vector3.zero;
    protected Vector3 end = Vector3.zero;

    protected short objId = -1;
    protected RendererService.Direction direction = RendererService.Direction.UP;
    protected short fromX = 0;
    protected short fromY = 0;
    protected short toX = 0;
    protected short toY = 0;

    public MotionAct(short objId, RendererService.Direction direction, short fromX, short fromY,
                     short toX, short toY) {
        this.objId     = objId;
        this.direction = direction;
        this.fromX     = fromX;
        this.fromY     = fromY;
        this.toX       = toX;
        this.toY       = toY;
    }

    public override void Init() {
        obj = Field.Instance.GetObject(objId);
        start = obj.transform.localPosition;
        end = new Vector3(toX, obj.transform.localPosition.y, -toY);
    }

    public override void Perform(float step) {
        obj.transform.localPosition = Vector3.Lerp(start, end, step);
    }
}


public class MoveAct : MotionAct {
    public MoveAct(short objId, RendererService.Direction direction, short fromX, short fromY,
                   short toX, short toY) : base(objId, direction, fromX, fromY, toX, toY) {}
}


public class JumpAct : MotionAct {
    protected Vector3 middle = Vector3.zero;

    public override void Init() {
        base.Init();
        middle = Vector3.Lerp(start, end, 0.5f);
        middle.y += 2;
    }


    public JumpAct(short objId, RendererService.Direction direction, short fromX, short fromY,
                   short toX, short toY) : base(objId, direction, fromX, fromY, toX, toY) {}

    public override void Perform(float step) {
        if (step < 0.5) {
            obj.transform.localPosition = Vector3.Slerp(start, middle, 2 * step);
        } else {
            obj.transform.localPosition = Vector3.Slerp(middle, end, 2 * step);
        }
    }

}
