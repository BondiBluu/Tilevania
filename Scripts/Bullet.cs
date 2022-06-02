using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 20f;
    float xSpeed;
    PlayerMovement player;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        //bullet goes into the direction of the player, times the bullet speed
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        //fy to the right each frame
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Enemy"){
            //if the bullet hits an enemy, destroy it
            Destroy(other.gameObject);
        }
        //destroy the bullet if it hits something like the wall
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        //destroy the bullet if it hits anything
        Destroy(gameObject);
    }
}
