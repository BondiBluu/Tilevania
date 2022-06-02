using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;


    //the very first thing that'll happen when we push play on Unity or restart a scene due to anything
    void Awake()
    {
        //we need only one game session
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length; 
        if(numOfGameSessions > 1){
            Destroy(gameObject);
        } else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();

    }

    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        } else{
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd){
        //add points to total score and printing it to score text
        score += 100;
        scoreText.text = score.ToString();
    }
    void TakeLife(){
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

    }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        //go back to the first scene
        SceneManager.LoadScene(0);
        Destroy(gameObject); //destroying the instance of the game session- a whole restart
    }

    
}
