using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    
    private float _speed = 4.0f; //the enemy moves 4 meters per second

    Player_sc player_sc;

    Animator anim;

    void Start()
    {
        player_sc = GameObject.Find("Player").GetComponent<Player_sc>(); //bir kere bulmak yeterli olur
        if(player_sc == null)
        {
            Debug.LogError("player_sc is null");
        }

        anim = GetComponent<Animator>();
        if (anim ==null)
        {
            Debug.LogError("");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5f)  //Check if y position is less than -5f
        {
            float randomX = Random.Range(-8f,8f); //Select a random x value between -8 and 8
            transform.position = new Vector3(randomX, 7, 0); //Relocate the enemy at the top (y = 7f) with the given x value
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit: "+ other.transform.name);

        if (other.tag == "Player")
        {
            //Damage the player
            Player_sc player = other.transform.GetComponent<Player_sc>();

            if(player != null)
            {
                player.Damage();
            }

            anim.SetTrigger("OnEnemyDeath");
            _speed =0;
            Destroy(this.gameObject, 2.5f);
        }

        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(player_sc != null)
            {
                player_sc.UpdateScore(10);
            }
            anim.SetTrigger("OnEnemyDeath");
            _speed =0;
            Destroy(this.gameObject, 2.5f);
        }
    }
}
