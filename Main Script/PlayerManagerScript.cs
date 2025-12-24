using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManagerScript : MonoBehaviour
{
    public static int playerHP = 100;

    public TextMeshProUGUI playerHPText;

    public static bool isGameOver;

    public GameObject bloodOverlay;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        playerHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + playerHP;

        if (isGameOver)
        {
            SceneManager.LoadScene("Game Over"); //display gameover screen
        }
    }

    public IEnumerator TakeDamage(int damageAmount)
    {
        bloodOverlay.SetActive(true);

        playerHP -= damageAmount;

        if(playerHP <= 0)
            isGameOver = true;

        yield return new WaitForSeconds(1.5f);
        bloodOverlay.SetActive(false);
    }
}
