using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3();
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

        transform.position += velocity * Time.deltaTime;

        velocity *= .993f;

    }
}
