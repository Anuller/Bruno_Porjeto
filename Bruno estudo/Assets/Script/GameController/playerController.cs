using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public GameController gameController;

    void OnCollisionEnter(Collision collsion)
    {
        Debug.Log("Eu colidi com algo" + collsion.collider.name);

        if (collsion.collider.CompareTag("Lava") == true)
        {
            gameController.GameOver();
            //liga a tela, variavel gameobject que ira ativa aqui
        }
    }
}