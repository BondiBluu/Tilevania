using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFXDling;
    [SerializeField] int pointsForCoinPickup = 100;

    bool wasCollected = false;

    //make the coin disapear when touched by the player
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !wasCollected){
            wasCollected = true;
            //when touching a coin, use the AddToScoreMthod in "Game Session"
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            //play the clip at the position of the camera (so you can always hear the effect near you)
            AudioSource.PlayClipAtPoint(coinSFXDling, Camera.main.transform.position);
            gameObject.SetActive(false);
            //destroy the object the script it attached to
            Destroy(gameObject);
        }
    }
}
