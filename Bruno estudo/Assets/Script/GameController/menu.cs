using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{

    public Transform pauseMenu;
    public Transform tuto;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1; 
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (tuto.gameObject.activeSelf)
            {
                tuto.gameObject.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                tuto.gameObject.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        tuto.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
