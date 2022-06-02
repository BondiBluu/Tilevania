using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag =="Player"){
        //used to create delays in the game
        StartCoroutine(LoadNextLevel());
        }
    }

    //Coroutines need IEnumerator methods to work
    IEnumerator LoadNextLevel(){
        //yield return needed to execute IEnumerator. Waiting an amount of time to execute something
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        //the current variable is the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        //if there are no more scene levels to go through, start back at the first.
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex); //loading the next level, the next num in the scene index
    }
}
