using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
        

        //We use aTan2 since it handles negative numbers and division by zero errors. 
        float angle = Mathf.Atan2(direction.y, direction.x);

        //Now we set our new rotation. 
        transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) +90);


    }
}
