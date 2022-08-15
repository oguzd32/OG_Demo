using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoadController))]
public class RoadControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RoadController roadController = (RoadController) target;
        DrawDefaultInspector();

        if (GUILayout.Button("Create Road"))
        {
            roadController.CreateRoad();
        }
    }
}
