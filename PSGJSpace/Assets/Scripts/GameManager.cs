using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool boostn;
    public GameObject health;

    public GameObject player;
    public Camera mainCamera;
    public Camera miniMapCam;


    public GameObject BasicEnemy;
    public GameObject ShipBit;
    private int numEnemies;

    private Vector3 originPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        boost.GetComponent<Slider>().value = 1;
        health.GetComponent<Slider>().value = 1;    
        originPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        boost.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().boostAmount / player.GetComponent<PlayerMovement>().maxBoost;
        health.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().healthAmount / player.GetComponent<PlayerMovement>().maxHealth;
        if (numEnemies < 30)
        {
            SpawnEnemies();
        }

        mainCamera.transform.rotation = Quaternion.identity;
        miniMapCam.transform.rotation = Quaternion.identity;

        if (Vector3.Distance(originPlayerPos, player.transform.position) > 210)
        {
            player.transform.position = new Vector3(-player.transform.position.x, -player.transform.position.y);
        }
        boostn = Input.GetKey(KeyCode.LeftShift);
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
        Vector3 pos = enemy.transform.position;
        Destroy(enemy);
        
        
        for (int i = 0; i < Random.Range(2, 6); i++)
        {
            Vector3 randpos = new Vector3(pos.x + Random.Range(-5f, 5f), pos.y + Random.Range(-5f, 5f));
            Quaternion q = Quaternion.Euler(player.transform.position.z - randpos.z, 0f, 0f);
            Instantiate(ShipBit, randpos, q);
        }
        numEnemies--;
    }
}
