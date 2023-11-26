using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrappleScript : MonoBehaviour
{
    LineRenderer lr;

    public PlayerScript player;
    
    public Vector3 gp;

    [SerializeField] Transform camera, firePoint;

    [SerializeField] private int  maxGrapple;

    private bool shoot, jump;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;

    }

    // Update is called once per frame
    void Update()
    {
        shoot = player.shoot;
        jump = player.jump;

        /* i tried uing the new input system but it updates the rope end point constantly and that's not what i want.
        if (shoot)
        {
            DrawRope();
        }
        else
        {
            lr.enabled = false;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            DrawRope();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }

        lr.SetPosition(0, firePoint.position);
    }

    private void DrawRope()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, maxGrapple))
        {
            gp = hit.point;
        }
        else
        {
            gp = camera.position + camera.forward * maxGrapple;
        }

        lr.enabled = true;
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, gp);
    }

}
