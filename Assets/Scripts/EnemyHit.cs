using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

    void OnCollisionEnter(Collision collisionInfo)
    {
        //TODO: Detect Collision with Wall, Enemy, etc.
        Debug.Log("Collision");
    }
}
