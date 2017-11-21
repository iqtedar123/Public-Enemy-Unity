using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolCircle : MonoBehaviour
{
    EnemyState enemyState;

    private float rotateSpeed = 2f;
    private float radius = 2f;

    private Vector2 centre;
    private float angle;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Change Traingle");
        enemyState = gameObject.GetComponent<EnemyState>();
        enemyState.viewAngleHalf = 30f;
        enemyState.escapeCopSeconds = 0.3f;
        centre = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState.getPatrolMode())
        {
            updatePos();
        }
    }

    

    private void updatePos()
    {
        angle += rotateSpeed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = centre + offset;
        gameObject.transform.transform.up = offset;


    }
    
    
}