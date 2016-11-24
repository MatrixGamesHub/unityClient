using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (ObliqueProjection))]
[CanEditMultipleObjects]
public class ObliqueProjectionEditor : Editor
{
    // Camera
    private SerializedObject soCams;
    private SerializedProperty propBackground;
    private SerializedProperty propClearFlags;
    private SerializedProperty propCullingMask;
    private SerializedProperty propDepth;
    private SerializedProperty propFar;
    private SerializedProperty propHdr;
    private SerializedProperty propNear;
    private SerializedProperty propRenderingPath;
    private SerializedProperty propTargetTexture;
    private SerializedProperty propViewPortRectH;
    private SerializedProperty propViewPortRectW;
    private SerializedProperty propViewPortRectX;
    private SerializedProperty propViewPortRectY;
    // Iso
    private SerializedObject soIsos;
    private SerializedProperty propDepthScaleX;
    private SerializedProperty propDepthScaleY;
    private SerializedProperty propSize;

    public void OnEnable()
    {
        soIsos = new SerializedObject(targets);
        Object[] cameras = targets.Select(iso => (Object) ((ObliqueProjection) iso).GetComponent<Camera>()).ToArray();
        soCams = new SerializedObject(cameras);

        // Iso
        propDepthScaleX = soIsos.FindProperty("projectionScale.x");
        propDepthScaleY = soIsos.FindProperty("projectionScale.y");
        propSize = soIsos.FindProperty("size");

        // Camera
        propClearFlags = soCams.FindProperty("m_ClearFlags");
        propBackground = soCams.FindProperty("m_BackGroundColor");
        propCullingMask = soCams.FindProperty("m_CullingMask");
        propNear = soCams.FindProperty("near clip plane");
        propFar = soCams.FindProperty("far clip plane");
        propViewPortRectX = soCams.FindProperty("m_NormalizedViewPortRect.x");
        propViewPortRectY = soCams.FindProperty("m_NormalizedViewPortRect.y");
        propViewPortRectW = soCams.FindProperty("m_NormalizedViewPortRect.width");
        propViewPortRectH = soCams.FindProperty("m_NormalizedViewPortRect.height");
        propDepth = soCams.FindProperty("m_Depth");
        propRenderingPath = soCams.FindProperty("m_RenderingPath");
        propTargetTexture = soCams.FindProperty("m_TargetTexture");
        propHdr = soCams.FindProperty("m_HDR");
    }

    public override void OnInspectorGUI()
    {
        soCams.Update();
        soIsos.Update();
        var iso = (ObliqueProjection) target;

        if(!iso.enabled) return;

        Camera cam = iso.GetComponent<Camera>();
        EditorGUI.InspectorTitlebar(GUILayoutUtility.GetRect(GUIContent.none, GUI.skin.GetStyle("IN Title")), true, cam, true);

        cam.hideFlags = HideFlags.HideInInspector;
        cam.orthographic = true;

        // Clear Flags
        EditorGUILayout.PropertyField(propClearFlags);

        // Background Color
        if (cam.clearFlags == CameraClearFlags.Color || cam.clearFlags == CameraClearFlags.Skybox)
        {
            EditorGUILayout.PropertyField(propBackground,
                                          new GUIContent("Background", "Camera clears the screen to this color before rendering."), true);
        }

        // Culling Mask
        EditorGUILayout.PropertyField(propCullingMask);

        // Spacer
        EditorGUILayout.Space();

        // Projection stuff
        EditorGUILayout.LabelField("Oblique Projection");
        EditorGUILayout.BeginHorizontal();
        EditorGUI.indentLevel++;

        // Depth scale
        EditorGUIUtility.LookLikeControls(44, 64);
        EditorGUILayout.PropertyField(propDepthScaleX);
        EditorGUILayout.PropertyField(propDepthScaleY);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        // Angle
        EditorGUI.BeginChangeCheck();
        var angle = EditorGUILayout.FloatField("Angle", iso.Angle);
        if (EditorGUI.EndChangeCheck())
        {
            iso.Angle = angle;
            Undo.RecordObject(iso, "Inspector");
            EditorUtility.SetDirty(iso);
        }


        // Ratio
        EditorGUI.BeginChangeCheck();
       var ratio= EditorGUILayout.FloatField("Ratio", iso.Ratio);
       if (EditorGUI.EndChangeCheck())
       {
           iso.Ratio = ratio;
           Undo.RecordObject(iso, "Inspector");
           EditorUtility.SetDirty(iso);
       }
       EditorGUIUtility.LookLikeControls();

        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;

        EditorGUILayout.PropertyField(propSize);

        // Spacer
        EditorGUILayout.Space();

        // Clipping Planes
        EditorGUILayout.LabelField("Clipping Planes");
        EditorGUILayout.BeginHorizontal();
        EditorGUI.indentLevel++;

        EditorGUIUtility.LookLikeControls(44, 64);
        EditorGUILayout.PropertyField(propNear, new GUIContent("Near"));
        EditorGUILayout.PropertyField(propFar, new GUIContent("Far"));

        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;

        // Normalized Viewport Rect
        EditorGUILayout.LabelField("Normalized Viewport Rect");
        EditorGUILayout.BeginHorizontal();

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propViewPortRectX);
        EditorGUILayout.PropertyField(propViewPortRectY);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(propViewPortRectW, new GUIContent("W"));
        EditorGUILayout.PropertyField(propViewPortRectH, new GUIContent("H"));
        EditorGUIUtility.LookLikeControls();

        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
        // Spacer
        EditorGUILayout.Space();

        // Depth
        EditorGUILayout.PropertyField(propDepth);

        // Rendering Path
        EditorGUILayout.PropertyField(propRenderingPath);

        // Target Texture
        EditorGUILayout.PropertyField(propTargetTexture);

        // HDR
        EditorGUILayout.PropertyField(propHdr);

          soCams.ApplyModifiedProperties();
          soIsos.ApplyModifiedProperties();
    }
}