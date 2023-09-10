using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGame : MonoBehaviour
{

    public GameObject tuto;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
