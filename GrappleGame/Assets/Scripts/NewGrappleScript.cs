using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewGrappleScript : MonoBehaviour
{
    LineRenderer lr;

    public PlayerScript player;
    public LayerMask whatIsGrappable;

    public Vector3 gp;

    [SerializeField] Transform camera, firePoint;

    [SerializeField] private int  maxGrapple;

    private bool shoot, jump;

    public float overshootY;


    [Header("LR Anim")]

    [SerializeField] private int linePoints, waveCount, warpCount;
    [SerializeField] private float waveSize, animSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;

    }

    // Update is called once per frame
    void Update()
    {
        //shoot = player.shoot;
        jump = player.jump;

        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
            DrawRope();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
            shoot = false;
        }

        if (shoot && jump)
        {
            ExecuteGrapple();

        }


            lr.SetPosition(0, firePoint.position);
        //StartCoroutine(AnimateRope());

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
            StopGrapple();
        }

        lr.enabled = true;
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, gp);

        StartCoroutine(AnimateRope());

        if (jump)
        {
            ExecuteGrapple();
        }

    }

    private void ExecuteGrapple()
    {
        //DrawRope();

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPo = gp.y - lowestPoint.y; ;
        float highestArcPoint = grapplePointRelativeYPo + overshootY;

        if (grapplePointRelativeYPo < 0)
        {
            highestArcPoint = overshootY;
        }

        player.JumpToPosition(gp, highestArcPoint);

        //speedLines.SetActive(true);

        if(transform.position == gp)
        {
            Invoke(nameof(StopGrapple), 1f);

        }

    }

    private void StopGrapple()
    {

        /*grappling = false;

        grapplingCdTimer = grapplingCd;*/

        lr.enabled = false;

    }

    private IEnumerator AnimateRope()
    {
        float startTime = Time.time;

        Vector3 startPos = lr.GetPosition(0);
        Vector3 endPos = lr.GetPosition(1);

        Vector3 pos = startPos;

        while(pos != endPos)
        {
            float time = (Time.time - startTime) / animSpeed;

            pos = Vector3.Lerp(startPos, endPos, time);
            lr.SetPosition(1, pos);

            yield return null;

            if(pos == endPos)
            {
                Debug.Log("end pos reached");
            }
        }
    }

    private void SetPoints(Vector3 targetPos, float percent)
    {
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);
        float length = maxGrapple;

        for(int i = 0; i < linePoints; i++)
        {
            float xPos = (float)i / linePoints * length;
            float reversePercent = (1 - percent);

            float amp = Mathf.Sin(reversePercent * warpCount * Mathf.PI);

            float yPos = Mathf.Sin((float)waveCount * i / linePoints * 2 * Mathf.PI * reversePercent) * amp;

            Vector2 pos = new Vector3(xPos, yPos);
            lr.SetPosition(1, pos);
        }

        Debug.Log("draw");
    }

}
