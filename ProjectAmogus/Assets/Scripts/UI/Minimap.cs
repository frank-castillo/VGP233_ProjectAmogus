using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    [SerializeField] private BoxCollider mapBounds;
    [SerializeField] private Camera minimapCamera;

    private float xMin, xMax, zMin, zMax;
    private float camZ, camX; // Control camera movement
    private float camOrthSize; // Vertical size of FOV
    private float cameraRatio; // Horizontal camera ratio

    private void Start()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        zMin = mapBounds.bounds.min.z;
        zMax = mapBounds.bounds.max.z;

        minimapCamera = GetComponent<Camera>();

        camOrthSize = minimapCamera.orthographicSize;

        var screenAspect = (float)Screen.width / (float)Screen.height;
        cameraRatio = screenAspect * camOrthSize;
    }

    private void LateUpdate()
    {
        camZ = Mathf.Clamp(player.position.z, zMin + camOrthSize, zMax - camOrthSize);
        camX = Mathf.Clamp(player.position.x, xMin + cameraRatio, xMax - cameraRatio);

        Vector3 newPosition = new Vector3(camX - 7, 0, camZ);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
