using UnityEditor;
using UnityEngine;

public class BezierCurveGeneratorWindow : EditorWindow
{
    [MenuItem("Tools/Bezier Curve Generator")]
    public static void ShowWindow()
    {
        GetWindow<BezierCurveGeneratorWindow>("Bezier Curve");
    }

    private float nodeDistance = 1f;
    private int initializeLineCount = 2;
    private BezierCurveGenerator roadGenerator;

    void OnGUI()
    {
        initializeLineCount = EditorGUILayout.IntField("Line Count", initializeLineCount);
        if (GUILayout.Button("Create Subdivision"))
        {
            CreateNewRoadSystem(initializeLineCount);
        }

        nodeDistance = EditorGUILayout.FloatField("Node Distance", nodeDistance);

        if (GUILayout.Button("Distance Mode"))
        {
            GenerateDistanceNodes();
        }
    }

    void CreateNewRoadSystem(int lineCount)
    {
        GameObject roadSystem = new("Bezier Curve System");
        roadGenerator = roadSystem.AddComponent<BezierCurveGenerator>();
        roadGenerator.InitializeLines(lineCount);
    }

    void GenerateDistanceNodes()
    {
        if (roadGenerator == null) return;

        roadGenerator.GenerateNodesAtInterval(nodeDistance);
    }
}