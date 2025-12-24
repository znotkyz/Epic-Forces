using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    /*public void PlayGame()
    {
        SceneManager.LoadScene("Final-IAM");
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoginMenu()
    {
        SceneManager.LoadScene("Login Menu");
    }
}
