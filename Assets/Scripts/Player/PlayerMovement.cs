using System;
using UnityEngine;
using static Utilities;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float attackMovementSpeed;
    [SerializeField] private float defenceMovementSpeed;
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private Vector2 clampX;
    [SerializeField] private GameObject windParticle;
    [SerializeField] private LayerMask friendLayerMask;
    
    // cached components
    private Rigidbody m_Rigidbody;
    private CharacterController cc;
    
    // private variables
    private bool isStarted = false;
    private float _ForwardSpeed;
    private float initialDefenceSpeed;
    private Vector3 speed;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        initialDefenceSpeed = defenceMovementSpeed;
    }

    internal void StartGame()
    {
        isStarted = true;
    }
    
    void Update()
    {
        if(!isStarted) return;

        if(Input.GetMouseButton(0)) AttackStatus();
        else DefenceStatus();
        
        speed.z = _ForwardSpeed;

        cc.SimpleMove(speed);
        
        Clamp();
    }
    
    private void FixedUpdate()
    {
        RaycastHit hit;

        if(Input.GetMouseButton(0)) return;
        
        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out hit, 5, friendLayerMask.value))
        {
            defenceMovementSpeed = hit.transform.GetComponent<NFCController>().GetSpeed;
        }
        else
        {
            defenceMovementSpeed = initialDefenceSpeed;
        }
    }

    private void AttackStatus()
    {
        speed.x = -horizontalSpeed;
        
        _ForwardSpeed = Mathf.Min(_ForwardSpeed + Time.deltaTime * 10, attackMovementSpeed);
        
        if (!windParticle.activeInHierarchy)
        {
            _GameReferenceHolder.cameraFollow.EnableFovEffect(true);
            windParticle.SetActive(true);
        }
    }

    private void DefenceStatus()
    {
        speed.x = horizontalSpeed;
        
        _ForwardSpeed = Mathf.Max(_ForwardSpeed - Time.deltaTime * 10, defenceMovementSpeed);
        if (windParticle.activeInHierarchy)
        {
            _GameReferenceHolder.cameraFollow.EnableFovEffect(false);
            windParticle.SetActive(false);
        }
    }
    
    private void Clamp()
    {
        Vector3 tempPos = transform.position;

        tempPos.x = Mathf.Clamp(tempPos.x, clampX.x, clampX.y);

        transform.position = tempPos;
    }
    
    private void OnDisable()
    {
        windParticle.SetActive(false);
        _GameReferenceHolder.cameraFollow.EnableFovEffect(false);
    }
}
