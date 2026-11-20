using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TargetTest))]
public class TargetTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TargetTest script = (TargetTest)target;

        //GUILayout.Space(10);

        if (GUILayout.Button("Start Rotation"))
        {
            script.StartRotation();
        }
    }
}
