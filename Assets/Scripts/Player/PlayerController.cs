using System;
using UnityEngine;
using  DG.Tweening;
using static Utilities;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private GameObject[] playerNumbers;
    [SerializeField] private Transform characterHips;
    
    // cached components
    private PlayerMovement movement;
    
    // private variables
    private Rigidbody[] ragdollRbs;
    private Collider[] ragdollColliders;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();

        int randomPlayerNumb = Random.Range(0, playerNumbers.Length);
        playerNumbers[randomPlayerNumb].SetActive(true);

        ragdollRbs = characterAnimator.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = characterAnimator.GetComponentsInChildren<Collider>();

        EnableRagdoll(false);
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("Start", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Finish"))
        {
            GameManager.Instance.EndGame(true);
            movement.enabled = false;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMoveX(0, .75f));
            sequence.AppendCallback(() => RandomWinAnim());
            sequence.AppendInterval(1f);
            GameManager.Instance.EndGame(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Enemy"))
        {
            EnableRagdoll(true);
            _GameReferenceHolder.cameraFollow.SetTarget(characterHips);
            _GameManager.EndGame(false);
            transform.DOMoveX(0, 1);
            movement.enabled = false;
        }
    }

    private void RandomWinAnim()
    {
        float randomNumb = Random.Range(0f, 1f);

        if (randomNumb < 1 / 3f)
        {
            characterAnimator.SetBool("Win1", true);
        }
        else if (randomNumb < 2 / 3f)
        {
            characterAnimator.SetBool("Win2", true);
        }
        else
        {
            characterAnimator.SetBool("Win3", true);
        }
    }

    private void EnableRagdoll(bool value)
    {
        characterAnimator.enabled = !value;
        
        foreach (Rigidbody rb in ragdollRbs)
        {
            rb.isKinematic = !value;
        }

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = value;
        }
    }
}
