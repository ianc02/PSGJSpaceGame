using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissleBehavior : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool fired;
    private Vector3 constDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.running)
        {
            if (!fired)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                //We use aTan2 since it handles negative numbers and division by zero errors. 
                float angle = Mathf.Atan2(direction.y, direction.x);

                //Now we set our new rotation. 
                transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg));
                fired = true;
                constDirection = direction;
            }

            transform.position += constDirection * speed * Time.deltaTime;





            
        }


    }
}
