using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public GameObject missle;
    public float missleWaitTime;


    private float lastMissleFireTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        lastMissleFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.running)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            if (Vector3.Distance(transform.position, player.transform.position) > 15)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                transform.position += -direction * speed * Time.deltaTime;
            }
            else
            {

                if (Time.time - lastMissleFireTime > missleWaitTime)
                {
                    float a = Mathf.Atan2(direction.y, direction.x);

                    //Now we set our new rotation. 

                    Quaternion q = Quaternion.Euler(0f, 0f, (a * Mathf.Rad2Deg)-90);


                    Instantiate(missle, transform.position + (direction * speed * 0.2f), q);
                    lastMissleFireTime = Time.time;
                }
            }



            //We use aTan2 since it handles negative numbers and division by zero errors. 
            float angle = Mathf.Atan2(direction.y, direction.x);

            //Now we set our new rotation. 
            transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg)-90);

        }
    }
}
