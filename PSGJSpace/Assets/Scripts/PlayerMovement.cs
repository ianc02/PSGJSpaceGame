using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float boostAmount;
    public float healthAmount;
    public float boostAddition;
    public float maxHealth;
    public float maxBoost;
    public float regenRate;
    public float initBoost;

    private float lastTimeBoost;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3();
        lastTimeBoost = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        float rotationInput = Input.GetAxis("Horizontal"); // "A" or "D" keys
        transform.Rotate(Vector3.forward * -rotationInput * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (Time.time - lastTimeBoost > 0.75)
            {
                velocity *= initBoost;
                lastTimeBoost = Time.time;
            }
            
        }
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (boostAmount > 0)
            {
                /*if (velocity.magnitude < 0.05)
                {
                    velocity += transform.up * speed * Time.deltaTime * initBoost;
                }
                */
                velocity *=boostAddition;
                boostAmount -= 5;
            }
        }
        else
        {
            boostAmount += 1;
            boostAmount = Mathf.Min(boostAmount, maxBoost);
        }


        healthAmount = Mathf.Min(healthAmount +regenRate, maxHealth);
        
        transform.position += velocity * Time.deltaTime;

        velocity *= .993f;
        Debug.Log(velocity);

    }
}
