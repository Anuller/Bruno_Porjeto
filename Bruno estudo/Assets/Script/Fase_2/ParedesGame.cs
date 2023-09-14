using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedesGame : MonoBehaviour
{
    public float tempoAparecer = 2.0f; 
    public float tempoDesaparecer = 2.0f; 
    public GameObject jogador; 

    private Renderer rend;
    private bool paredeVisivel = true;
    private float tempoDecorrido = 0.0f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    private void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoAparecer && paredeVisivel)
        {
            rend.enabled = false; 
            paredeVisivel = false;
            tempoDecorrido = 0.0f;
        }
        else if (tempoDecorrido >= tempoDesaparecer && !paredeVisivel)
        {
            rend.enabled = true; 
            paredeVisivel = true;
            tempoDecorrido = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.tag == "parede" && jogador)
        {
            Destroy(gameObject); // Destroi a parede, coloca dps para destroir o player 
        }
    }
}

