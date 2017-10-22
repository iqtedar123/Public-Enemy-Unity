using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapCam : MonoBehaviour {

    public Transform mainPlayer;

    void LateUpdate()
    {
        var position = mainPlayer.position;
        position.z = -10;
        transform.position = position;
    }
}
