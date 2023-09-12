using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPlataforma : MonoBehaviour
{
    public Transform plataforma; 
    public Transform jogador; 
    public float velocidadePlataforma = 2.0f; 

    private Vector3 posicaoInicialJogador; 

    private void Start()
    {
        posicaoInicialJogador = jogador.position - plataforma.position;
    }

    private void Update()
    {
       
        float movimentoPlataforma = Mathf.Sin(Time.time * velocidadePlataforma) * 2.0f; 
        plataforma.position = new Vector3(movimentoPlataforma, plataforma.position.y, plataforma.position.z);

        
        jogador.position = plataforma.position + posicaoInicialJogador;
    }
}
