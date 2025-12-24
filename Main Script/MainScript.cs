using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public static MainScript Instance;

    public WebManagerScript WebManagerScript;

    public UserInfoScript UserInfo;

    public LoginScript Login;

    public GameObject UserProfile;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        WebManagerScript = GetComponent<WebManagerScript>();

        UserInfo = GetComponent<UserInfoScript>();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("EXIT GAME");
    }

    public void LoginMenu()
    {
        SceneManager.LoadScene("Login Menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
