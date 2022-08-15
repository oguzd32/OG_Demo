using UnityEngine;
using  DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private bool followOnX = false;
    
    // cached components
    private Camera mainCamera;
    
    // private variables
    private Transform target = default;
    private float initialFov;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offSet = Vector3.zero;
    private Vector3 targetPosition;

    private void Start()
    {
        target = GameReferenceHolder.Instance.playerController.transform;
        offSet = transform.position - target.position;

        mainCamera = GetComponent<Camera>();

        initialFov = mainCamera.fieldOfView;
    }

    private void LateUpdate()
    {
        if(!target) return;

        targetPosition = target.position + offSet;
        targetPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (!followOnX)
        {
            targetPosition.x = transform.position.x;
        }

        transform.position = targetPosition;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    public void EnableFovEffect(bool value)
    {
        DOTween.Kill(gameObject);
        
        if (value)
        {
            DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, initialFov + 20, 1f);
        }
        else
        {
            DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, initialFov, 1f);
        }
    }
}
