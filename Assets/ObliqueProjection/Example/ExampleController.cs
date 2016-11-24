using UnityEngine;

/// <summary>A quick handler for controlling the camera.</summary>
public class ExampleController : MonoBehaviour
{
    public ObliqueProjection cameraView;
    public Transform viewTarget;

    private Vector3 clickMousePos;
    private Vector3 clickTargetPos;
    private Vector3 worldUpperLeft;
    private Vector3 worldLowerRight;

    private bool scrolling;

    // Controls
    private Rect rectControl = new Rect(10, 10, 350, 200);
    protected GUIStyle controlLine;
    private Vector2 cabinet = new Vector2(0.25f, 0.3f);
    private Vector3 cameraRot = new Vector3(360, 0, 0);
    private float zoom = 100f;

    public void Start()
    {
        QualitySettings.shadowDistance = 1500;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Clicking on objects
        if(Input.GetMouseButtonDown(0))
        {
            var ray = cameraView.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, 1000))
            {
                hitInfo.transform.SendMessage("Click", SendMessageOptions.DontRequireReceiver);
            }
        }

        if (!cameraView) return;

        Scrolling();
        Zooming();

        // Camera
        viewTarget.rotation = Quaternion.Euler(cameraRot);
        cameraView.projectionScale = cabinet;
        cameraView.size = zoom;
    }

    private const float minZoom = 10;
    private const float maxZoom = 150;
    private const float damping = 5;
    private const int dragSpeed = 5;

    private void Zooming()
    {
        if (zoom < minZoom)
        {
            zoom = minZoom;
        }

        if (zoom > maxZoom)
        {
            zoom = maxZoom;
        }
        // Over GUI?
        if (Input.mousePosition.x < rectControl.width + rectControl.x
            && Screen.height - Input.mousePosition.y < rectControl.height + rectControl.y) return;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.Alpha2))
        {
            if (zoom < maxZoom)
            {
                zoom = Mathf.Lerp(zoom, maxZoom, Time.deltaTime*damping);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.Alpha1))
        {
            if (zoom > minZoom)
            {
                zoom = Mathf.Lerp(zoom, minZoom, Time.deltaTime*damping);
            }
        }
    }

    private void Scrolling()
    {
        // Start
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
            if (Input.mousePosition.x > rectControl.width + rectControl.x
                || Screen.height - Input.mousePosition.y > rectControl.height + rectControl.y)
            {
                scrolling = true;
            }
        // Do it
        if (scrolling)
            if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
            {
                viewTarget.Translate(
                    -new Vector3(Input.GetAxis("Mouse X")*dragSpeed, 0, Input.GetAxis("Mouse Y")*dragSpeed));
            }
        // End
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(2))
        {
            scrolling = false;
        }
    }

    public void OnGUI()
    {
        controlLine = GUI.skin.textArea;
        controlLine.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginArea(rectControl, GUI.skin.box);
        GUILayout.BeginVertical();
        {
            // Tilt
            GUILayout.BeginHorizontal(controlLine);
            GUILayout.Label("Camera Tilt");
            cameraRot.x = GUILayout.HorizontalSlider(cameraRot.x, 270, 360);
            GUILayout.EndHorizontal();
            // Rotation
            GUILayout.BeginHorizontal(controlLine);
            GUILayout.Label("Camera Rotation");
            cameraRot.y = GUILayout.HorizontalSlider(cameraRot.y, 0, 270);
            GUILayout.EndHorizontal();
            // Oblique X
            GUILayout.BeginHorizontal(controlLine);
            GUILayout.Label("Oblique X");
            cabinet.x = GUILayout.HorizontalSlider(cabinet.x, -2, 2);
            GUILayout.EndHorizontal();
            // Oblique Y
            GUILayout.BeginHorizontal(controlLine);
            GUILayout.Label("Oblique Y");
            cabinet.y = GUILayout.HorizontalSlider(cabinet.y, -2, 2);
            GUILayout.EndHorizontal();
            // Zoom
            GUILayout.BeginHorizontal(controlLine);
            GUILayout.Label("Zoom");
            zoom = GUILayout.HorizontalSlider(zoom, minZoom, maxZoom);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
