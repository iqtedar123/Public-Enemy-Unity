using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    public Transform player;

    // TODO: Update camera position on collision
    void Update()
    {
        var playerPosition = player.position;
        transform.position = new Vector3
        {
            x = playerPosition.x,
            y = playerPosition.y,
            z = -10
        };

    }
}
