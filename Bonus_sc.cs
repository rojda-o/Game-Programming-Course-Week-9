using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bonus_sc : MonoBehaviour
{
    public float speed = 3.0f;
    [SerializeField]
    int bonusID;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player_sc player_sc = other.transform.GetComponent<Player_sc>();
            if(player_sc != null)
            {
                
                switch(bonusID)
                {
                    case 0:
                        player_sc.ActivateTripleShot();
                        break;
                    case 1:
                        player_sc.ActivateSpeedBonus();
                        break;
                    case 2:              
                        player_sc.ActivateShieldBonus();
                        break;
                    default:
                        Debug.Log("default value in switch case");
                        break;
                }
                
               /*
                if(bonusID == 0)
                {
                    player_sc.ActivateTripleShot();
                }
                else if(bonusID == 1)
                {
                    player_sc.ActivateSpeedBonus();
                }
                else if(bonusID == 2)
                {
                    Debug.Log("shield bonus collected");
                }
                */
            }
            
            Destroy(this.gameObject);
        }
    }
}

