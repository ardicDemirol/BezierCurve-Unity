# Bezier Curve Generator - Unity

This Unity project provides tools for creating and editing **Bezier Curves** using custom editor scripting. The system is designed to streamline workflows where dynamically generated curves and nodes are required, such as in path generation or road-like structures.

## Key Features

- **Custom Editor Window**:
  - A dedicated editor tool (`BezierCurveGeneratorWindow.cs`) for generating and managing Bezier curve systems.
  - Easily accessible via the Unity menu under `Tools -> Bezier Curve Generator`.

- **Interactive Scene GUI**:
  - Edit and manipulate Bezier curve control points directly in the scene view using `BezierCurveGeneratorEditor.cs`.
  - Control points are visualized and made interactive for real-time adjustments.

- **Dynamic Node Generation**:
  - Generate nodes along the Bezier curve at specified intervals (`GenerateNodesAtInterval`).
  - Useful for creating evenly spaced points along curves for pathfinding or other applications.

- **Bezier Curve Calculation**:
  - Robust implementation of the Bezier curve algorithm (`Bezier.cs`) for calculating curve points based on control nodes.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/ardicDemirol/BezierCurve-Unity.git
   ```

2. Open the project in Unity (tested with Unity version `2023.2.20f1`).

3. Navigate to the `Assets/Scripts` folder for the main scripts.

## How to Use

### 1. Open the Bezier Curve Generator Tool
- From the Unity menu, go to `Tools -> Bezier Curve Generator`.

### 2. Generate a Bezier Curve System
- Set the number of curves (`Curve Count`) and click **Create Subdivision**.
- This will create a new curve system with initialized Bezier curves.

### 3. Adjust Control Points
- Select the `Bezier Curve Generator` GameObject in the scene.
- Use the scene view handles to reposition control points of the Bezier curves.

### 4. Generate Nodes at Intervals
- Set the `Node Distance` value in the tool.
- Click **Distance Mode** to automatically generate evenly spaced nodes along the curve.

## Code Highlights

### Editor Classes
- **`BezierCurveGeneratorEditor.cs`**:
  - Provides custom inspector and scene GUI functionality.
  - Enables interactive editing of Bezier control points.

- **`BezierCurveGeneratorWindow.cs`**:
  - Offers a custom editor window for Bezier curve generation and node placement.

### Core Classes
- **`BezierCurveGenerator.cs`**:
  - Manages the core logic for creating and editing Bezier curves and nodes.
  - Implements methods like `InitializeCurves`, `GenerateNodesAtInterval`, and `OnDrawGizmos`.

- **`Bezier.cs`**:
  - Contains the static `CalculatePoint` method for computing points on a Bezier curve.

### Sample Code: Bezier Calculation
```csharp
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
```

## Contribution

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch (`feature-name`).
3. Commit your changes and push them.
4. Open a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
