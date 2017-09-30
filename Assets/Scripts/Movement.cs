using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public Transform playerTransform;
    public Camera povCam;
    public LineRenderer shootLine;
    private WaitForSeconds gunTimeOut = new WaitForSeconds(0.5f);

    // Use this for initialization
    void Start()
    {
        shootLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update position of player
        var currentPosition = playerTransform.position;
        currentPosition.x += Input.GetAxisRaw("Horizontal") * 5 * Time.deltaTime;
        currentPosition.y += Input.GetAxisRaw("Vertical") * 5 * Time.deltaTime;
        playerTransform.position = new Vector3(currentPosition.x, currentPosition.y);

        //TODO: Configure with Mouse position
        if (Input.GetButton("Fire1"))
        {
            RaycastHit gunShot;
            Vector3 focus = povCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            StartCoroutine(Bullet());
            shootLine.SetPosition(0, playerTransform.position);
            if(Physics.Raycast(focus, new Vector3(0, 5), out gunShot, 100))
            {
                Debug.Log("Hit");
                shootLine.SetPosition(1, gunShot.point);
                var enemy = gunShot.collider.GetComponent<Health>();
                if (enemy != null) enemy.DisableEnemy();
            }
            else
            {
                shootLine.SetPosition(1, focus + (new Vector3 (0, 5)* 100));
            }
        }
    }

    public IEnumerator Bullet()
    {
        shootLine.enabled = true;
        yield return gunTimeOut;
        shootLine.enabled = false;
    }
}
