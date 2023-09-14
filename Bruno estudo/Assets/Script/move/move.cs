using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody body;

    public float speed, maxSpeed, drag;

    public float rotationSpeed;

    public float forcaDoPulo;

    private bool isGraund, jump;

    public LayerMask ground;

    public Transform cam;

    bool left, right, forward, backward;

    bool agachar, agachando, correr;

    public float aganhandoVelocity, correrSpeed;

    float originalSpeed;

    public float maxSlop;

    Vector3 mvtDir;
    public float maxStepHeight;

    public AudioSource pulo;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleDrag();
        CheackGrounded2();
    }

    void FixedUpdate()
    {
        HandleMovement();
        LimitiVelocity();
        HandleRotation();
        CheckStairs();
    }

    #region CheckStairs
    void CheckStairs()
    {
        if (isGraund)
        {
            Vector3 origin1 = mvtDir * .6f + Vector3.up * (maxStepHeight + .01f);
            Vector3 origin2 = Quaternion.Euler(0, 35, 0) * origin1;
            Vector3 origin3 = Quaternion.Euler(0, -35, 0) * origin1;

            RaycastHit hit1, hit2, hit3;

            Physics.Raycast(transform.position + origin1, Vector3.down, out hit1, Mathf.Infinity, ground);
            Physics.Raycast(transform.position + origin2, Vector3.down, out hit2, Mathf.Infinity, ground);
            Physics.Raycast(transform.position + origin3, Vector3.down, out hit3, Mathf.Infinity, ground);

            if (hit2.point.y > hit1.point.y)
            {
                hit1 = hit2;
            }
            if (hit3.point.y > hit1.point.y)
            {
                hit1 = hit3;
            }

            if (hit1.normal == Vector3.up && (hit1.point.y - transform.position.y) > .05f)
                transform.position += Vector3.up * (hit1.point.y - transform.position.y - .1f);
        }
    }
    #endregion

    #region CheackGrounded
    void CheackGrounded()
    {
        isGraund = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .5f, ground);
        if (isGraund)
            body.useGravity = false;
        else
            body.useGravity = true;

    }

    void CheackGrounded2()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, out hit, Mathf.Infinity, ground))
        {
            float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal);
            float sec = 1/Mathf.Cos(slopeAngle);
            float yDiff = .5f * sec - .5f;
            if ((transform.position.y - yDiff) - hit.point.y < .05f)
            {
                if (Vector3.Angle(Vector3.up, hit.normal) <= maxSlop)
                {
                    isGraund = true;
                    body.useGravity = false;
                    return;
                }
                body.AddForce(Vector3.down * 300f);
            }
        }
        isGraund = false;
        body.useGravity = true;
    }
    #endregion

    #region HandleRotation
    void HandleRotation()
    {
        if ((new Vector2(body.velocity.x, body.velocity.z)).magnitude > .1f)
        {
            Vector3 horizontalDir = new Vector3(body.velocity.x, 0, body.velocity.z);
            Quaternion rotation = Quaternion.LookRotation(horizontalDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
    }

    //void HandleRotation2()
    //{
        //Vector3 dir = body.GetAccumulatedForce();
        //if((new Vector2(dir.x, dir.z)).magnitude > .1f)
        //{
           // Vector3 horizontalDir = new Vector3(body.velocity.x, 0, body.velocity.z);
            //Quaternion rotation = Quaternion.LookRotation(horizontalDir, Vector3.up);
           // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
       // }
   // }
    #endregion

    #region HandleDrag
    void HandleDrag ()
    {
        if (!isGraund)
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z) / (1 + drag / 100) + new Vector3(0, body.velocity.y, 0);
        else
            body.velocity /= (1 + drag / 100);
    }
    #endregion

    #region LimitiVelocity
    void LimitiVelocity()
    {
        if (!isGraund)
        {
            Vector3 HorizoltalVelocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            if (HorizoltalVelocity.magnitude > maxSpeed)
            {
                Vector3 LimitiVelocity = HorizoltalVelocity.normalized * maxSpeed;
                body.velocity = new Vector3(LimitiVelocity.x, body.velocity.y, LimitiVelocity.z);
            }
        }
        else
        {
            if (body.velocity.magnitude > maxSpeed)
            {
                Vector3 LimitiVelocity = body.velocity.normalized * maxSpeed;
                body.velocity = new Vector3(LimitiVelocity.x, body.velocity.y, LimitiVelocity.z);
            }
        }
    }
    #endregion

    #region HandleInput
    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            forward = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            backward = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGraund)
        {
            pulo.Play();
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            agachar = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !agachando)
        {
            speed = correrSpeed;
            correr = true;
        }

    }
    #endregion

    # region HandleMovement
    void HandleMovement()
    {
        

        if (left)
        {
            MoveDir(Vector3.left);
            left = false;
        }
        if (right)
        {
            MoveDir(Vector3.right);
            right = false;
        }
        if (forward)
        {
            MoveDir(Vector3.forward);
            forward = false;
        }
        if (backward)
        {
            MoveDir(Vector3.back);
            backward = false;
        }

        if (jump && isGraund)
        {
            transform.position += Vector3.up * .5f;
            body.velocity = new Vector3 (body.velocity.x, 0, body.velocity.y);
            body.AddForce (Vector3.up * forcaDoPulo, ForceMode.Impulse);
            jump = false;
        }

        if (agachar && !agachando)
        {
            speed = aganhandoVelocity;
            transform.localScale -= new Vector3(0, .5f, 0);
            agachar = false;
            agachando = true;
        }
        if (agachar && agachando)
        {
            speed = originalSpeed;
            transform.localScale += new Vector3(0, .5f, 0);
            agachar = false;
            agachando = false;
        }

        if (correr && !agachando)
        {
            correr = false;
        }

        if (!correr && !agachando)
        {
            speed = originalSpeed;
        }
        

        void MoveDir(Vector3 moveDir)
        {
            Quaternion dir = Quaternion.Euler(0f, cam.rotation.eulerAngles.y, 0f);

            Vector3 planeNormal = Vector3.up;
            mvtDir = (dir * moveDir).normalized;
            if (isGraund)
            {
            if (Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, out RaycastHit hit, Mathf.Infinity, ground))
            {
                planeNormal = hit.normal;
            }
            }

            Vector3 force = Vector3.ProjectOnPlane(dir * moveDir, planeNormal) * speed;

            body.AddForce(force);
            
        }

    }
    #endregion
}
