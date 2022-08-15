using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private int roadLenght = 15;
    [SerializeField] private float distanceBetweenRoads;
    [SerializeField] private bool randomRoadLenght = false;
    [SerializeField] private BoxCollider roadCollider;

    private void Start()
    {
        if (randomRoadLenght)
        {
            int randomRoadLenght = Random.Range(5, 20);
            GetRoads(randomRoadLenght);
        }
        else
        {
            GetRoads(roadLenght);
        }
    }

    private void GetRoads(int lenght)
    {
        roadCollider.size = new Vector3(15, 1, lenght * distanceBetweenRoads);
        roadCollider.center = new Vector3(0, -0.5f, roadCollider.size.z / 2);
        
        for (int i = 0; i < lenght; i++)
        {
            GameObject roadObj = _ObjectPooler.GetPooledObject("Chunk");
            roadObj.transform.position = Vector3.forward * i * distanceBetweenRoads;
            roadObj.SetActive(true);
        }

        GameObject finalObj = _ObjectPooler.GetPooledObject("Finish");
        finalObj.transform.position = Vector3.forward * lenght * distanceBetweenRoads;
        finalObj.SetActive(true);
    }
}
