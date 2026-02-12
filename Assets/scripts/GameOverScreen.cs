using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text CoinText;
    public Text ScoreText;
    public Text highScoreText;
    public GameObject editableText;
    public void Setup (int coins , float score , float highScore )
    {
        gameObject.SetActive (true);
        CoinText.text = "Coins: "+coins .ToString ();
        ScoreText.text = "Score: "+ Mathf.FloorToInt(score).ToString();
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();

        editableText.gameObject.SetActive (false);


    }
    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
        EditableTexts.Instance.ResetScore();
        editableText.gameObject.SetActive(false);


    }

    public void ExitButton()
    {

        SceneManager.LoadScene("MainMenu");

    }
}
