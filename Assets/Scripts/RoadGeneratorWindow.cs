using UnityEditor;
using UnityEngine;

public class RoadGeneratorWindow : EditorWindow
{
    [MenuItem("Tools/Road Generator")]
    public static void ShowWindow()
    {
        GetWindow<RoadGeneratorWindow>("Road Generator");
    }

    private float nodeDistance = 1f;
    private int initializeLineCount = 2;
    private RoadGenerator roadGenerator;

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
        GameObject roadSystem = new("Road System");
        roadGenerator = roadSystem.AddComponent<RoadGenerator>();
        roadGenerator.InitializeLines(lineCount);
    }

    void GenerateDistanceNodes()
    {
        if (roadGenerator == null) return;

        roadGenerator.GenerateNodesAtInterval(nodeDistance);
    }
}