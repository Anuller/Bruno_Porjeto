using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        imprimeInstru��es();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void imprimeInstru��es()
    {
        print("Bem vindo ao jogo");
        print("Mova o jogador com WASD");
        print("N�o bate nas paredes");
    }
}
