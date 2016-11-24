using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Rendering/Oblique Projection")]
public class ObliqueProjection : MonoBehaviour
{
    private const float magicOrthoSizeFactor = 25f; // don't modify - has to match orthographic camera
    private const float offsetFactor = 0.70710678118654752440084436210485f; // = SIN/COS(PI/4)
    private Matrix4x4 obliqueMatrix;
    public Vector2 projectionScale = new Vector2(1, 1);
    public float size = 100;
    public readonly Dimensions dimensions = new Dimensions();
    private Matrix4x4 viewProjection;
    private Matrix4x4 inverseViewProjection;
    private Vector2 effectiveScale;

    public float Angle { get { return GetAngle(projectionScale); } set { SetFromAngleAndRatio(value, Ratio); } }

    public float Ratio { get { return GetRatio(projectionScale); } set { SetFromAngleAndRatio(Angle, value); } }

    public void OnDisable()
    {
        // Show camera again
        GetComponent<Camera>().hideFlags = 0;
        GetComponent<Camera>().ResetProjectionMatrix();
    }

    public void OnDestroy()
    {
        // Show camera again
        GetComponent<Camera>().hideFlags = 0;
        GetComponent<Camera>().ResetProjectionMatrix();
    }

    //public void Update()
    //{
    //    WorldToScreenTest();
    //}

    private void WorldToScreenTest()
    {
        Vector3 origin = ScreenToWorld(Input.mousePosition);

        Vector3 unorigin = WorldToScreen(origin);
        Vector3 unoriginShow = ScreenToWorld(unorigin);
        Debug.DrawLine(origin, unoriginShow, Color.magenta);
    }

    //public void OnDrawGizmos()
    //{
    //    RayTest();
    //}

    private void RayTest()
    {
        Ray ray = ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, 4*transform.up, Color.red);
        Debug.DrawRay(ray.origin, 4*transform.right, Color.white);
        Debug.DrawRay(ray.origin, 4*-transform.up, Color.white);
        Debug.DrawRay(ray.origin, 4*-transform.right, Color.white);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ray.origin, hitInfo.point);
            Gizmos.DrawWireSphere(hitInfo.point, 5);
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(hitInfo.point, ray.origin + ray.direction*1000);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(ray.origin, ray.direction*1000);
        }
    }

    public void SetFromAngleAndRatio(float deg, float ratio)
    {
        const float sqrt = 1.414213f;
        ratio *= sqrt;

        // If we're too low, bad stuff happens
        if (ratio < 0.01f) ratio = 0.01f;

        projectionScale.x = ratio*Mathf.Sin(deg*Mathf.Deg2Rad);
        projectionScale.y = ratio*Mathf.Cos(deg*Mathf.Deg2Rad);
    }

    private static float GetAngle(Vector2 v)
    {
        // x and y args to atan2() swapped to rotate resulting angle 90 degrees
        // (Thus angle in respect to +Y axis instead of +X axis)
        float a = Mathf.Atan2(-v.x, v.y)*Mathf.Rad2Deg;

        // Force result between 0 and 360
        // Subtract because positive degree angles go clockwise
        return (360 - a)%360;
    }

    private static float GetRatio(Vector2 v)
    {
        const float sqrt = 1.414213f;
        return v.magnitude/sqrt;
    }

    public void LateUpdate()
    {
        // Calculate dimensions
        dimensions.height = 2*magicOrthoSizeFactor;
        dimensions.width = 2*magicOrthoSizeFactor*GetComponent<Camera>().aspect;
        dimensions.depth = (GetComponent<Camera>().farClipPlane - GetComponent<Camera>().nearClipPlane);
        dimensions.left = -dimensions.width/2;
        dimensions.right = dimensions.left + dimensions.width;
        dimensions.bottom = -dimensions.height/2;
        dimensions.top = dimensions.bottom + dimensions.height;

        // Calculate projection matrix
        Matrix4x4 ortho = Matrix4x4.Ortho(dimensions.left, dimensions.right, dimensions.bottom, dimensions.top,
                                          GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
        effectiveScale.x = projectionScale.x*dimensions.depth/dimensions.width;
        effectiveScale.y = projectionScale.y*dimensions.depth/dimensions.height;
        obliqueMatrix = size.Equals(0)
                            ? Oblique(effectiveScale, 0.001f)
                            : Oblique(effectiveScale, magicOrthoSizeFactor/size);
        GetComponent<Camera>().projectionMatrix = obliqueMatrix*ortho;
        // Calculate Inverse View Projection
        viewProjection = (GetComponent<Camera>().projectionMatrix*GetComponent<Camera>().worldToCameraMatrix);
        inverseViewProjection = viewProjection.inverse;
    }

    public Ray ScreenPointToRay(Vector3 position)
    {
        Vector3 origin = ScreenToWorld(position);

        Vector3 direction = ScreenToWorld(position + Vector3.forward);

        var ray = new Ray(origin, origin - direction);
        return ray;
    }

    public Vector3 ScreenToWorld(Vector3 screenPosition)
    {
        // Normalize the screen position
        if (size.Equals(0))
        {
            screenPosition.x = screenPosition.x/Screen.width*2 - 1
                               + effectiveScale.x*offsetFactor*magicOrthoSizeFactor/0.001f;
            screenPosition.y = screenPosition.y/Screen.height*2 - 1
                               + effectiveScale.y*offsetFactor*magicOrthoSizeFactor/0.001f;
            screenPosition.z *= -1;
        }
        else
        {
            screenPosition.x = screenPosition.x/Screen.width*2 - 1
                               + effectiveScale.x*offsetFactor*magicOrthoSizeFactor/size;
            screenPosition.y = screenPosition.y/Screen.height*2 - 1
                               + effectiveScale.y*offsetFactor*magicOrthoSizeFactor/size;
            screenPosition.z *= -1;
        }

        // Apply IVP
        Vector3 worldPosition = inverseViewProjection*screenPosition;
        //    worldPosition.y -= camera.nearClipPlane;
        //worldPosition.x -= camera.nearClipPlane;
        //worldPosition.y -= camera.nearClipPlane;

        // Add translation
        return worldPosition + transform.position + transform.forward*GetComponent<Camera>().nearClipPlane;
    }

    public Vector3 WorldToScreen(Vector3 worldPosition)
    {
        return GetComponent<Camera>().WorldToScreenPoint(worldPosition);
        /*
        // Remove translation
        worldPosition -= transform.position;

        // Unappy IVP
        Vector3 screenPosition = viewProjection*worldPosition;

        // Adjust for screen
        screenPosition.x = (screenPosition.x - effectiveScale.x*offsetFactor*magicOrthoSizeFactor/size + 1)/2
                           *Screen.width;
        screenPosition.y = (screenPosition.y - effectiveScale.y*offsetFactor*magicOrthoSizeFactor/size + 1)/2
                           *Screen.height;
        screenPosition.z *= -1;

        screenPosition.x *= Screen.width;
        screenPosition.y *= Screen.height;
        return screenPosition;*/
    }

    private static Matrix4x4 Oblique(Vector2 tilt, float scale)
    {
        Matrix4x4 m = Matrix4x4.zero;
        m[0, 2] = tilt.x*offsetFactor*scale;
        m[1, 2] = tilt.y*offsetFactor*scale;
        m[0, 0] = scale;
        m[1, 1] = scale;
        m[2, 2] = scale;
        m[3, 3] = 1;
        return m;
    }

    #region Nested type: Dimensions

    [Serializable]
    public class Dimensions
    {
        public float bottom, depth;
        public float height, left, right, top;
        public float width;
    }

    #endregion
}
