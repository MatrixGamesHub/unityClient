using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

    public int amount = 100;
    public Vector3 range = new Vector3(400, 0, 400);
    public Vector3 minSize = new Vector3(1, 1, 1);
    public Vector3 maxSize = new Vector3(10, 10, 10);

    public void Awake () {
        for (int i = 0; i < 100; i++)
        {
            SpawnCube();
        }
	}

    private void SpawnCube()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.Scale(Random.insideUnitSphere + Vector3.one, (maxSize - minSize)) + minSize;
        cube.transform.position = Vector3.Scale(Random.insideUnitSphere,range/2)+cube.transform.localScale.y/2*Vector3.up;
        cube.AddComponent<CubeTest>();
        cube.transform.parent = transform;
    }
}
