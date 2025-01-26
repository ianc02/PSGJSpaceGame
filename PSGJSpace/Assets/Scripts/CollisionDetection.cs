using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update

    private EventInstance explosions;
    private EventInstance pickups;
    private EventInstance hurt;

    private void Start()
    {
         explosions = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Explsions);
        pickups = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Pickups);
        hurt = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Hurt);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.gameObject.layer);
        if (collision != null)
        {
            GameObject other = collision.gameObject;
            if (other.layer == 8) //player
            {
                if (gameObject.layer == 9)
                {
                    if (GameManager.Instance.boostn)
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 6f;
                    }
                    else
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 12f;
                    }
                    hurt.start();
                    explosions.start();
                    GameManager.Instance.DestroyEnemy(transform.gameObject);
                    GameManager.Instance.addToScore(100);

                }
                else if (gameObject.layer == 11) //shooterenemy
                {
                    if (GameManager.Instance.boostn)
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 4f;
                    }
                    else
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 8f;
                    }
                    hurt.start();
                    explosions.start();
                    GameManager.Instance.DestroyEnemy(transform.gameObject);
                    GameManager.Instance.addToScore(200);
                }
                else if (gameObject.layer == 12) //missle
                {
                    if (GameManager.Instance.boostn)
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 15f;
                    }
                    else
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 30f;
                    }
                    hurt.start();
                    explosions.start();
                    Destroy(gameObject);
                }
                else if (gameObject.layer == 10)// shipbit
                {
                    other.GetComponent<PlayerMovement>().healthAmount += 6f;
                    pickups.start();
                    Destroy(gameObject);
                    GameManager.Instance.addToScore(10);
                }

            }
        }
    }
}
