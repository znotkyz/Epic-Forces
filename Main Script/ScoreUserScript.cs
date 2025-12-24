using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUserScript : MonoBehaviour
{
    public TMP_InputField SaveUsernameInput;

    public TMP_InputField ScoreUserInput;

    public Button SaveScoreButton;

    // Start is called before the first frame update
    void Start()
    {
        SaveScoreButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainScript.Instance.WebManagerScript.ScoreUser(SaveUsernameInput.text, ScoreUserInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
