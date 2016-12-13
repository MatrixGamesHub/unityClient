using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class UpdateObjectAct : AtomicAct {

    protected GameObject obj = null;

    private short objId = -1;
    private string key = "";
    private RendererService.Value value;

    public UpdateObjectAct(short objId, string key, RendererService.Value value) {
        this.objId = objId;
        this.key   = key;
        this.value = value;
    }

    public override void Init() {
        obj = Field.Instance.GetObject(objId);

        Debug.Log("Key: " + key + "   Value: " + value);
    }

    public override void Perform() {
    }
}