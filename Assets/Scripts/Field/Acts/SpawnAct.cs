using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class SpawnAct : TemporalAct {

    private short objId = -1;
    private char symbol = ' ';
    private short positionX = 0;
    private short positionY = 0;

    public SpawnAct(short objId, sbyte symbol, short positionX, short positionY) {
        this.objId     = objId;
        this.symbol    = Convert.ToChar(symbol);
        this.positionX = positionX;
        this.positionY = positionY;
    }

    public override void Init() {
        Field.Instance.Add(objId, symbol, new Vector2(positionX, positionY));
    }

    public override void Perform(float value) {
    }
}