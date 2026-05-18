using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Settings")]
    public float smoothTime = 0.15f; 
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero; 

    private void Start()
    {
        if (offset == Vector3.zero && player != null)
        {
            offset = transform.position - player.position;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
