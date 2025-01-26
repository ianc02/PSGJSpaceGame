using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    public float partSystemAngleChange;
    public GameObject partSystemGO;

    private float lastTimeBoost;
    //Sound
    private EventInstance boostSFX;
    private float boostSFXValue;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3();
        lastTimeBoost = Time.time;
        boostSFX = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Boost);
        boostSFXValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.running)
        {
            if (GameManager.Instance.playerCanMove)
            {

                float rotationInput = Input.GetAxis("Horizontal"); // "A" or "D" keys
                transform.Rotate(Vector3.forward * -rotationInput * rotationSpeed * Time.deltaTime);

                float totalAngle = 0f;
                if (Input.GetKey(KeyCode.A))
                {
                    totalAngle += partSystemAngleChange;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    totalAngle -= partSystemAngleChange;
                }
                Quaternion psAngle = Quaternion.Euler(totalAngle, -90, partSystemGO.transform.rotation.z);
                partSystemGO.transform.localRotation = psAngle;

                if (Input.GetKey(KeyCode.W))
                {
                    velocity += transform.up * speed * Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (Time.time - lastTimeBoost > 0.75)
                    {

                        boostSFXValue = 1f;
                        boostSFX.setParameterByName("Boost_Fade", boostSFXValue);
                        velocity *= initBoost;
                        lastTimeBoost = Time.time;
                        boostSFX.start();
                        partSystemGO.GetComponent<ParticleSystem>().Play();

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
                        velocity *= boostAddition;
                        boostAmount -= 5;
                        GameManager.Instance.addToScore(0.01f);
                       
                    }
                }

                else
                {
                    boostAmount += 1;
                    boostAmount = Mathf.Min(boostAmount, maxBoost);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift) || boostAmount <= 5)
                {
                    while (boostSFXValue > 0.01)
                    {
                        boostSFX.setParameterByName("Boost_Fade", boostSFXValue);
                        boostSFXValue -= 0.12f;
                        partSystemGO.GetComponent<ParticleSystem>().Stop();
                    }
                    partSystemGO.GetComponent<ParticleSystem>().Stop();


                }


                healthAmount = Mathf.Min(healthAmount - regenRate, maxHealth);
                if (healthAmount <= 0 && GameManager.Instance.playerCanMove)
                {
                    GameManager.Instance.GameIsOver();
                }
            }
        

            transform.position += velocity * Time.deltaTime;

            velocity *= .993f;
            //Debug.Log(velocity);
        }

    }

    
}
