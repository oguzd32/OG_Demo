using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RoadController : MonoBehaviour
{
    [SerializeField] private int roadCount;
    [SerializeField] private float distanceBetweenRoads;
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject finalPrefab;
    [SerializeField] private BoxCollider roadCollider; 
    [Space]
    [SerializeField] private List<GameObject> currentRoads = new List<GameObject>();

    public void CreateRoad()
    {
        foreach (GameObject road in currentRoads)
        {
            DestroyImmediate(road);
        }
        
        currentRoads.Clear();

        if (roadCount < 1)
        {
            Debug.Log("Road count must be greater than 0.");
            return;
        }
        for (int i = 0; i < roadCount; i++)
        {
            GameObject roadInstance = (GameObject) PrefabUtility.InstantiatePrefab(roadPrefab, transform);
            roadInstance.transform.position = Vector3.forward * i * distanceBetweenRoads;
            currentRoads.Add(roadInstance);
        }

        roadCollider.size = new Vector3(15, 1, roadCount * distanceBetweenRoads);
        roadCollider.center = new Vector3(0, -0.5f, roadCollider.size.z / 2);
        
        GameObject finalInstance = (GameObject) PrefabUtility.InstantiatePrefab(finalPrefab, transform);
        finalInstance.transform.position = Vector3.forward * roadCount * distanceBetweenRoads;
        currentRoads.Add(finalInstance);
    }
}
