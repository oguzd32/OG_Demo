using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  static Utilities;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnDurationInterval;
    [SerializeField] private string tag;

    // private variables
    private double lastSpawnTime;
    private bool spawn = true;

    private IEnumerator Start()
    {
        while (spawn)
        {
            float randomDuration = Random.Range(spawnDurationInterval.x, spawnDurationInterval.y);
            yield return new WaitForSeconds(randomDuration);
            Spawn();
        }
    }

    /*
    private void Update()
    {
        if(transform.position.z - _GameReferenceHolder.playerController.transform.position.z < 20) return;
        
        if (Time.time < lastSpawnTime + spawnDuration) return;

        lastSpawnTime = Time.time;
        Spawn();
    }*/

    private void Spawn()
    {
        if (transform.position.z - _GameReferenceHolder.playerController.transform.position.z < 20)
        {
            spawn = false;
            return;
        }
        
        GameObject character = _ObjectPooler.GetPooledObject(tag);
        character.transform.position = transform.position;
        character.SetActive(true);
        character.GetComponent<NFCController>().StartGame();
    }
}
