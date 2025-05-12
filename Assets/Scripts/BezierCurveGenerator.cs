using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierCurveGenerator : MonoBehaviour
{
    public List<BezierLine> lines = new();
    public int segmentsPerCurve = 20;

    public void InitializeLines(int lineCount)
    {
        lines.Clear();

        for (int i = 0; i < lineCount; i++)
        {
            BezierLine newLine = new();
            SetupInitialNodes(newLine, i);
            lines.Add(newLine);
        }
    }
    public void RemoveNodes()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    void SetupInitialNodes(BezierLine line, int lineIndex)
    {
        Plane ground = new(Vector3.up, Vector3.zero);
        float lineOffset = lineIndex * 3f;

        for (int i = 0; i < 4; i++)
        {
            Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3((i + 1) * 0.2f, 0.5f, 0));

            if (ground.Raycast(ray, out float enter))
            {
                Vector3 pos = ray.GetPoint(enter);
                pos.x += lineOffset;

                line.nodes.Add(new BezierNode
                {
                    position = pos,
                });
            }
        }
    }

    public void GenerateNodesAtInterval(float interval)
    {
        RemoveNodes();

        foreach (var line in lines)
        {
            float accumulatedDistance = 0f;
            Vector3 previousPoint = line.GetPoint(0);

            for (float t = 0; t <= 1; t += 0.01f)
            {
                Vector3 currentPoint = line.GetPoint(t);
                accumulatedDistance += Vector3.Distance(previousPoint, currentPoint);

                if (accumulatedDistance >= interval)
                {
                    CreateNodeMarker(currentPoint);
                    accumulatedDistance = 0f;
                }
                previousPoint = currentPoint;
            }
        }
    }


    void CreateNodeMarker(Vector3 position)
    {
        GameObject marker = new("Node");
        marker.transform.position = position;
        marker.transform.parent = transform;

        MeshFilter meshFilter = marker.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = marker.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");

        Material material = new(Shader.Find("Standard"))
        {
            color = Color.green
        };
        meshRenderer.material = material;

        marker.transform.localScale = Vector3.one * 0.1f;
    }

    void OnDrawGizmosSelected()
    {
        if (lines == null) return;

        foreach (var line in lines)
        {
            if (line.nodes.Count != 4) continue;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(line.nodes[0].position, 0.25f);
            Gizmos.DrawSphere(line.nodes[3].position, 0.25f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(line.nodes[1].position, 0.15f);
            Gizmos.DrawSphere(line.nodes[2].position, 0.15f);

            // Bezier curve
            Gizmos.color = Color.white;
            Vector3 prevPos = line.nodes[0].position;
            for (int i = 1; i <= segmentsPerCurve; i++)
            {
                float t = i / (float)segmentsPerCurve;
                Vector3 newPos = line.GetPoint(t);
                Gizmos.DrawLine(prevPos, newPos);
                prevPos = newPos;
            }
        }
    }
}

[System.Serializable]
public class BezierLine
{
    public List<BezierNode> nodes = new();

    public Vector3 GetPoint(float t)
    {
        if (nodes.Count != 4) return Vector3.zero;

        return Bezier.CalculatePoint(
            nodes[0].position,
            nodes[1].position,
            nodes[2].position,
            nodes[3].position,
            t
        );
    }
}

[System.Serializable]
public class BezierNode
{
    public Vector3 position;
}

public static class Bezier
{
    public static Vector3 CalculatePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
    }
}