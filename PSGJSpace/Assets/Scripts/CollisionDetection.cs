using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.layer);
        if (collision != null)
        {
            GameObject other = collision.gameObject;
            if (other.layer == 6) //player
            {
                GameManager.Instance.DestroyEnemy(transform.gameObject);
            }
        }
    }
}
