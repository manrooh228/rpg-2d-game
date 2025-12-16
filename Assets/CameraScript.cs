using System.Runtime.CompilerServices;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    [SerializeField] private Transform player;

    [SerializeField] private float minX = -20f;
    [SerializeField] private float maxX = 105f;
    void Awake()
    {
        
    }


    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned to the CameraFollow script.");
            return;
        }

        Vector3 newPosition = transform.position;
        newPosition.x = player.position.x;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }
}
