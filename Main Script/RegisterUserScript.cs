using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterUserScript : MonoBehaviour
{
    public TMP_InputField UsernameInput;

    public TMP_InputField PasswordInput;

    public TMP_InputField ConfirmPasswordInput;

    public Button SubmitButton;

    //public GameObject login;

    public GameObject Register;

    // Start is called before the first frame update
    void Start()
    {
        SubmitButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainScript.Instance.WebManagerScript.RegisterUser(UsernameInput.text, PasswordInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Trigger()
    {
        if (Register.activeInHierarchy == true)   //true
        {
            Register.SetActive(false);   //false

        }
        /*else
        {
            Register.SetActive(false);
        }*/
    }
}
