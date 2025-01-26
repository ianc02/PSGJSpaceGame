using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
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
                        other.GetComponent<PlayerMovement>().healthAmount -= 9f;
                    }
                    else
                    {
                        other.GetComponent<PlayerMovement>().healthAmount -= 18f;
                    }
                    GameManager.Instance.DestroyEnemy(transform.gameObject);
                }
                else if (gameObject.layer == 10)
                {
                    other.GetComponent<PlayerMovement>().healthAmount += 6f;
                    Destroy(gameObject);
                }

            }
        }
    }
}
