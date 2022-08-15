using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NFCController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float clampX = 1.5f;
    [SerializeField] private GameObject[] playerNumbers;

    public float GetSpeed => forwardSpeed;
    
    public enum CharacterType
    {
        FRIENDLY, ENEMY
    }

    public CharacterType currentType = CharacterType.FRIENDLY;
    
    // cached components
    private CharacterController cc;

    // private variables
    private bool isStarted = false;
    private Vector3 speed;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        if (currentType is CharacterType.ENEMY)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        else
        {
            transform.rotation = Quaternion.identity;;
        }
        
        int randomPlayerNumb = Random.Range(0, playerNumbers.Length);
        playerNumbers[randomPlayerNumb].SetActive(true);
        
        Clamp();
    }

    internal void StartGame()
    {
        isStarted = true;
        characterAnimator.SetBool("Start", true);
    }
    
    private void Update()
    {
        if(!isStarted) return;

        if (currentType is CharacterType.ENEMY)
        {
            if (GameReferenceHolder.Instance.playerController.transform.position.z - 10 > transform.position.z)
            {
                gameObject.SetActive(false);
            }
        }
        
        speed.z = forwardSpeed * direction.z;
        cc.SimpleMove(speed);
        Clamp();
    }

    private void Clamp()
    {
        Vector3 tempPos = transform.position;
        tempPos.x = clampX;
        transform.position = tempPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("OutFinish"))
        {
            gameObject.SetActive(false);
        }
    }
}
