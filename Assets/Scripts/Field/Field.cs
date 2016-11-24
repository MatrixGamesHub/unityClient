using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Field : Singleton<Field> {

    public Transform root;
    public Objects objects;

    private short width = 0;
    public short Width { get { return width; } }

    private short height = 0;
    public short Height { get { return height; } }

    private Dictionary<int, GameObject> objMap = new Dictionary<int, GameObject>();
    private List<Vector2> emptyCells = new List<Vector2>();
    private int groundId = -1;

    private GameObject ground = null;
    private GameObject wall = null;

    public void SetSize(short width, short height) {
        this.width = width;
        this.height = height;
        Clear();

        root.transform.position = new Vector3(-(width - 1) / 2.0f, 0, (height - 1) / 2.0f);
    }

    public void SetGroundTexture(MtxRendererService.GroundTexture texture) {
        ground = objects.GetGround(texture);
    }

    public void SetWallTexture(MtxRendererService.WallTexture texture) {
        wall = objects.GetWall(texture);
    }

    public void Clear() {
        foreach(GameObject goObj in objMap.Values) {
            Destroy(goObj);
        }

        objMap.Clear();
        emptyCells.Clear();
        groundId = -1;
    }

    public void ShowGround() {
        Vector2 pos;
        for(int y = 0; y < height; ++y) {
            for(int x = 0; x < width; ++x) {
                pos = new Vector2(x, y);
                if (emptyCells.Contains(pos)) {
                    continue;
                }
                Add(groundId--, '·', pos);
            }
        }
    }

    public GameObject GetObject(int objId) {
        if (!objMap.ContainsKey(objId)) {
            return null;
        }
        return objMap[objId];
    }

    public void Add(int objId, char symbol, Vector2 pos) {
        GameObject prefab = null;
        switch (symbol) {
            case '#':
                prefab = wall;
                break;
            case '1':
                prefab = objects.player1;
                break;
            case 'b':
                prefab = objects.boxPrefab;
                break;
            case 't':
                prefab = objects.targetPrefab;
                break;
            case '-':
                emptyCells.Add(pos);
                return;
            case '·':
                prefab = ground;
                break;
            case '+':
                prefab = objects.tilePrefab;
                break;
            case '.':
                prefab = objects.pacDotPrefab;
                break;
        }

        if (prefab != null) {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(root);
            obj.transform.localPosition = new Vector3(pos.x, prefab.transform.position.y, -pos.y);
            objMap[objId] = obj;
        }
    }

    public void Remove(int objId) {
        GameObject obj = objMap[objId];
        Destroy(obj);
        objMap.Remove(objId);
    }

    public void Move(int objId, Vector2 from, Vector2 to) {
        GameObject obj = objMap[objId];
        obj.transform.localPosition = new Vector3(to.x, obj.transform.position.y, -to.y);
    }

    public void Jump(int objId, Vector2 from, Vector2 to) {
        GameObject obj = objMap[objId];
        obj.transform.localPosition = new Vector3(to.x, obj.transform.position.y, -to.y);
    }

    public void Update() {
    }

}
