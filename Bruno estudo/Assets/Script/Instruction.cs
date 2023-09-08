using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        imprimeInstruções();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void imprimeInstruções()
    {
        print("Bem vindo ao jogo");
        print("Mova o jogador com WASD");
        print("Não bate nas paredes");
    }
}
