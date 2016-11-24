using UnityEngine;
using System.Collections;

public class CubeTest : MonoBehaviour {

    public void Click()
    {
        Vector3 color = (Random.onUnitSphere + Vector3.one*2)/2;
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1);
    }
}
