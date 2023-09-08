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
        CheackGrounded();
    }

    void FixedUpdate()
    {
        HandleMovement();
        LimitiVelocity();
        HandleRotation();
    }

    void CheackGrounded()
    {
        isGraund = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .5f, ground);
        if (isGraund)
            body.useGravity = false;
        else
            body.useGravity = true;

    }

    void HandleRotation()
    {
        if ((new Vector2(body.velocity.x, body.velocity.z)).magnitude > .1f)
        {
            Vector3 horizontalDir = new Vector3(body.velocity.x, 0, body.velocity.z);
            Quaternion rotation = Quaternion.LookRotation(horizontalDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
    }

    void HandleDrag ()
    {
        if (!isGraund)
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z) / (1 + drag / 100) + new Vector3(0, body.velocity.y, 0);
        else
            body.velocity /= (1 + drag / 100);
    }

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

            if (isGraund)
            {
            if (Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, out RaycastHit hit, Mathf.Infinity, ground))
            {
                planeNormal = hit.normal;
            }
            }

            Vector3 force = Vector3.ProjectOnPlane(dir * moveDir, planeNormal) * speed;

            if(Vector3.Angle(Vector3.up, planeNormal) > maxSlop)
            {
                body.AddForce(Vector3.down * 300f);
            }

            body.AddForce(force);
            
        }

    }

}
