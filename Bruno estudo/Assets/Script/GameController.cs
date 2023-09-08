using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject telaMorte;
    public void GameOver()
    {
        telaMorte.SetActive(true);
    }

    public void Reset()
    {
        telaMorte.SetActive(false);
        SceneManager.LoadScene("GameFase1");
    }

}
