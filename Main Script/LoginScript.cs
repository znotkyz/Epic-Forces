using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField UsernameInput;

    public TMP_InputField PasswordInput;

    public Button LoginButton;

    public GameObject register;

    //public GameObject register;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainScript.Instance.WebManagerScript.Login(UsernameInput.text, PasswordInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Trigger()
    {
        if (register.activeInHierarchy == false)
        {
            register.SetActive(true);

        }
        /*else
        {
            register.SetActive(false);
        }*/
    }
}
