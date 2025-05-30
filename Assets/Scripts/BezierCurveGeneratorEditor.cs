using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurveGenerator))]
public class BezierCurveGeneratorEditor : Editor
{
    private BezierCurveGenerator generator;

    void OnEnable()
    {
        generator = (BezierCurveGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Refresh Lanes"))
        {
            generator.InitializeLines(2);
            generator.RemoveNodes();
        }
    }

    void OnSceneGUI()
    {
        if (generator.lines == null) return;

        for (int lineIndex = 0; lineIndex < generator.lines.Count; lineIndex++)
        {
            BezierLine line = generator.lines[lineIndex];

            if (line.nodes.Count != 4) continue;

            for (int nodeIndex = 0; nodeIndex < line.nodes.Count; nodeIndex++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPos = Handles.PositionHandle(line.nodes[nodeIndex].position, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(generator, "Move Bezier Node");
                    line.nodes[nodeIndex].position = newPos;
                    EditorUtility.SetDirty(generator);
                }
            }
        }
    }
}