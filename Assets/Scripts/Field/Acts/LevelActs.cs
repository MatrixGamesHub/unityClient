using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class LevelAct : AtomicAct {

    private List<List<List<List<short>>>> field = null;

    public LevelAct(List<List<List<List<short>>>> field) {
        this.field = field;
    }

    public override void Perform() {
        Field mtxField = Field.Instance;
        short height = (short)field.Count;
        short width = (short)(height == 0 ? 0 : field[0].Count);
        mtxField.SetSize(width, height);

        for(short y = 0; y < height; ++y) {
            for(short x = 0; x < width; ++x) {
                foreach(List<short> objDef in field[y][x]) {
                    mtxField.Add(objDef[0], Convert.ToChar(objDef[1]), new Vector2(x, y));
                }
            }
        }
        mtxField.ShowGround();
    }
}


public class LoadLevelAct : LevelAct {

    private MtxRendererService.LevelInfo levelInfo;

    public LoadLevelAct(List<List<List<List<short>>>> field,
                        MtxRendererService.LevelInfo levelInfo) : base(field) {
        this.levelInfo = levelInfo;
    }

    public override void Perform() {
        Field mtxField = Field.Instance;
        mtxField.SetGroundTexture(levelInfo.GroundTexture);
        mtxField.SetWallTexture(levelInfo.WallTexture);

        base.Perform();
    }
}


public class ResetLevelAct : LevelAct {
    public ResetLevelAct(List<List<List<List<short>>>> field) : base(field) {}
}
