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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.deltaTime = 0;
    }

    public void Reset()
    {
        telaMorte.SetActive(false);
        SceneManager.LoadScene("GameFase1");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
