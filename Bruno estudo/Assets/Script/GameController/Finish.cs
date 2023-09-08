using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject Win;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.tag == "Win")
        {
            Win.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //liga a tela, variavel gameobject que ira ativa aqui

        }
    }
}
