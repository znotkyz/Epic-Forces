using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveUserScript : MonoBehaviour
{
    public TMP_InputField SaveUsernameInput;

    public Button SaveButton;

    // Start is called before the first frame update
    void Start()
    {
        SaveButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainScript.Instance.WebManagerScript.SaveUser(SaveUsernameInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
