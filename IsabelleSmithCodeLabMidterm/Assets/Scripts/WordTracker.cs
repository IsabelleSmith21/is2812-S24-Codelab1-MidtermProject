using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

public class WordTracker : MonoBehaviour
{
    // TextMeshPro objects to display score and high scores
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI display;
    
    public int score; //public variable to store the current score

    // Directory and file paths for storing high scores
    private const string FILE_DIR = "/DATA/";
    private const string DATA_FILE = "hs.txt";
    private string FILE_FULL_PATH;
    public int ComplimentScore   // Property to access and set the score
    {
        get { return score; }
        private set
        {
            score = value;
            Debug.Log("score changed!");
        }
    }

    private string highScoresString = "";   // String to hold high scores read from file

    private List<int> highScores;  // List to store high scores

    public List<int> HighScores
    {
        get
        {
            if (highScores == null)
            {
                Debug.Log("got from file");
                highScores = new List<int>();
                if (File.Exists(FILE_FULL_PATH))
                {
                    highScoresString = File.ReadAllText(FILE_FULL_PATH);
                    highScoresString = highScoresString.Trim();
                    string[] highScoreArray = highScoresString.Split("\n");

                    for (int i = 0; i < highScoreArray.Length; i++)
                    {
                        int currentScore = Int32.Parse(highScoreArray[i]);
                        highScores.Add(currentScore);
                    }
                }
            }

            return highScores;
        }
    }


    public static WordTracker instance;    // Singleton instance of WordTracker
    
    void Awake() //the script instance is being loaded
    {
        if (instance == null) //ensures there is only one instance of the script
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //destroy duplicate instance 
        }
    }
    // Array to store words and slots to place them
    public string[] wordArray = new string[10];
    private int currentIndex = 0;

    public GameObject[] slots = new GameObject[10];

    private void Start()
    {
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;
    }

    private void UpdateComplimentScore(string tag)     // Method to update score based on collected word type
    {
        switch (tag)
        {
            case "BoringWord":
                ComplimentScore += 1;
                Debug.Log("BoringWord collected. Score +1. Current Compliment Score: " + ComplimentScore);
                break;
            case "Noun":
                ComplimentScore += 2;
                Debug.Log("Noun collected. Score +2. Current Compliment Score: " + ComplimentScore);
                break;
            case "Adjective":
                ComplimentScore += 3;
                Debug.Log("Adjective collected. Score +3. Current Compliment Score: " + ComplimentScore);
                break;
            default:
                Debug.LogWarning("Unknown tag: " + tag);
                break;
        }

        // Update the TextMeshPro score display
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()    // Method to update score display
    {
        // Check if the scoreText variable is assigned
        if (scoreText != null)
        {
            // Update the TextMeshPro text with the current score
            scoreText.text = "Compliment Score: " + ComplimentScore;
        }
        else
        {
            Debug.LogWarning("Score Text not assigned!");
        }
    }
    
    private void UpdateHighScoreDisplay() // Method to update high score display
    {
        // Check if the display variable is assigned
        if (display != null)
        {
            // Update the TextMeshPro text with the high scores
            display.text = "High Scores:\n" + highScoresString;
        }
        else
        {
            Debug.LogWarning("Display Text not assigned!");
        }
    }

    private void MoveWordToSlot()    // Method to move collected words to designated slots
    {
        for (int i = 0; i < currentIndex; i++)
        {
            GameObject wordObject = GameObject.Find(wordArray[i]);
            if (wordObject != null && i < slots.Length)
            {
                wordObject.transform.position = slots[i].transform.position;

                Rigidbody2D wordRigidbody = wordObject.GetComponent<Rigidbody2D>();
                if (wordRigidbody != null)
                {
                    wordRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX |
                                                RigidbodyConstraints2D.FreezePositionY |
                                                RigidbodyConstraints2D.FreezeRotation;
                    Debug.Log("Position and rotation frozen for: " + wordObject.name);
                }
            }
        }
    }

    private void DestroyAllLevelObjectsExceptInArray()     // Method to destroy level objects not in wordArray
    {
        // Find the "Level Objects" GameObject
        GameObject levelObjectsContainer = GameObject.Find("Level Objects");

        // Check if the container exists
        if (levelObjectsContainer != null)
        {
            // Iterate through the children of the container
            foreach (Transform child in levelObjectsContainer.transform)
            {
                GameObject levelObject = child.gameObject;

                // Check if the levelObject is not in the wordArray
                if (System.Array.IndexOf(wordArray, levelObject.name) == -1)
                {
                    Destroy(levelObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("Level Objects container not found!");
        }
    }

    private void DisplayScore()    // Method to display score
    {
        Debug.Log("Compliment Score: " + ComplimentScore);
    }

    private void CheckArrayFull()  // Method to check if wordArray is full and update 
    {
        if (currentIndex == wordArray.Length)
        {
            DisplayScore();
            DestroyAllLevelObjectsExceptInArray();
            SetHighScore();
            UpdateHighScoreDisplay();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)   // Method called when collision occurs with words
    {
        if (collision.gameObject.CompareTag("Noun") ||
            collision.gameObject.CompareTag("BoringWord") ||
            collision.gameObject.CompareTag("Adjective"))
        {
            if (currentIndex < wordArray.Length)
            {
                wordArray[currentIndex] = collision.gameObject.name;
                currentIndex++;

                Debug.Log("Collision with " + collision.gameObject.name);

                UpdateComplimentScore(collision.gameObject.tag);

                MoveWordToSlot();
                CheckArrayFull();
            }
            else
            {
                //isinGame = false;
                Debug.LogWarning("Word array is full!");
            }
        }
    }

    bool isHighScore(int score)     // Method to check if score is a high score
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i] < score)
            {
                return true;
            }
        }

        return false;
    }

    void SetHighScore()    // Method to set high score
    {
        if (isHighScore(score))
        {
            int highScoreSlot = -1;

            for (int i = 0; i < HighScores.Count; i++)
            {
                if (score > highScores[i])
                {
                    highScoreSlot = i;
                    break;
                }
            }

            highScores.Insert(highScoreSlot, score);
            highScores = highScores.GetRange(index: 0, count: 3);
            string scoreBoardText = "";

            foreach (var highScore in highScores)
            {
                scoreBoardText += highScore + "\n";
            }

            highScoresString = scoreBoardText;

            Debug.Log("Writing to file: " + FILE_FULL_PATH); 
            File.WriteAllText(FILE_FULL_PATH, highScoresString);
            
            // Update the high score display
            UpdateHighScoreDisplay();
        }
    }

}

