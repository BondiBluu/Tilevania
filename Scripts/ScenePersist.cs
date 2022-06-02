using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    //the very first thing that'll happen when we push play on Unity or restart a scene due to anything
    void Awake()
    {
        //we need only one scene persist
        int numOfScenePersists = FindObjectsOfType<ScenePersist>().Length; 
        if(numOfScenePersists > 1){
            Destroy(gameObject);
        } else{
            DontDestroyOnLoad(gameObject);
        }       
    }
    public void ResetScenePersist(){
            Destroy(gameObject);
    }
}
