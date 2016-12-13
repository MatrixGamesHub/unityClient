using UnityEngine;
using System.Collections.Generic;
using System;


public class RendererHandler : Actor, RendererService.RendererService.Iface {

    public void Ping() {
    }

    public override void Freeze() {
        base.Freeze();
    }

    public override void Thaw() {
        base.Thaw();
    }

    public void Shutdown() {
        AddAct(new ShutdownAct());
    }

    public void Pause() {
    }

    public void Resume() {
    }

    public void Clear() {
    }

    public List<sbyte> GetPreferedFieldSize() {
        return new List<sbyte> {0, 0};
    }

    public void LoadLevel(List<List<List<List<short>>>> field, RendererService.LevelInfo levelInfo) {
        AddAct(new LoadLevelAct(field, levelInfo));
    }

    public void ResetLevel(List<List<List<List<short>>>> field) {
        AddAct(new ResetLevelAct(field));
    }

    public void UpdateObject(short objId, string key, RendererService.Value value) {
        AddAct(new UpdateObjectAct(objId, key, value));
    }

    public void Spawn(short objId, sbyte symbol, short positionX, short positionY) {
        AddAct(new SpawnAct(objId, symbol, positionX, positionY));
    }

    public void @Remove(short objId, short sourceId) {
        AddAct(new RemoveAct(objId, sourceId));
    }

    public void Collect(short objId, short sourceId) {
        AddAct(new CollectAct(objId, sourceId));
    }

    public void TriggerEnter(short objId, short sourceId) {
        AddAct(new TriggerEnterAct(objId, sourceId));
    }

    public void TriggerLeave(short objId, short sourceId) {
        AddAct(new TriggerLeaveAct(objId, sourceId));
    }

    public void Move(short objId, RendererService.Direction direction, short fromX, short fromY, short toX, short toY) {
        AddAct(new MoveAct(objId, direction, fromX, fromY, toX, toY));
    }

    public void Jump(short objId, RendererService.Direction direction, short fromX, short fromY, short toX, short toY) {
        AddAct(new JumpAct(objId, direction, fromX, fromY, toX, toY));
    }

}
