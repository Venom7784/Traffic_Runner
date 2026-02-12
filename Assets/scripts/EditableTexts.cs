using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EditableTexts : MonoBehaviour

{ 
    public Text coinText;
    
    
    public static EditableTexts Instance;

    public Text scoreText;         // UI Text to display the score
    public float score;            // Current score
    public float scoreRate = 10f; // Points per second

    PlayerMovement playerMovement;
    bool CanMove => playerMovement.canMove; // Reference to player movement state

     // Assign in Inspector

    private float highScore = 0f;


    void Start()
    {
        LoadHighScore();
    }
    void Awake()
    {
        // Singleton pattern (optional)
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (CanMove)
        {
            score += scoreRate * Time.deltaTime;
            if (scoreText != null)
                scoreText.text = Mathf.FloorToInt(score).ToString();
        }
    }

    public void ResetScore()
    {
        score = 0f;
        if (scoreText != null)
            scoreText.text = "0";
    }

    public void TextUpdate(int coins)
    {
        coinText.text = coins.ToString();
    }

    public float getScoreInfo()
    { 
        return score;
    }

    public void CheckAndUpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save(); // Save immediately

          
        }
    }

    public float LoadHighScore()
    {
        return highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

}

