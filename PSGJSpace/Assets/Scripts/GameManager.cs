using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve this object across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }



    //UI
    public GameObject boost;
    public GameObject health;

    public GameObject player;
    public Camera mainCamera;


    public GameObject BasicEnemy;
    private int numEnemies;
    
    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        boost.GetComponent<Slider>().value = 1;
        health.GetComponent<Slider>().value = 1;    
    }

    // Update is called once per frame
    void Update()
    {
        boost.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().boostAmount / player.GetComponent<PlayerMovement>().maxBoost;
        health.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().healthAmount / player.GetComponent<PlayerMovement>().maxHealth;
        if (numEnemies < 20)
        {
            SpawnEnemies();
        }

        mainCamera.transform.rotation = Quaternion.identity;
    }

    void SpawnEnemies()
    {
        Vector3 p = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f),0);
        Quaternion q = Quaternion.Euler(player.transform.position.z - p.z,0f,0f);
        if (Vector3.Distance(player.transform.position, p) > 20f)
        {
            Instantiate(BasicEnemy, p, q);
            numEnemies++;
        }
        
    }

    public void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        numEnemies--;
    }
}
