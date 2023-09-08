using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFol : MonoBehaviour
{
    public Transform follow;

    public float senci;

    Vector3 offset;

    float raio;

    float mouseX;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - follow.position;
        raio = offset.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + offset;

        mouseX -= Input.GetAxis("Mouse X") * senci * Time.deltaTime;
        offset = new Vector3(Mathf.Cos(mouseX) * raio, offset.y, Mathf.Sin(mouseX) * raio);
    }
}
