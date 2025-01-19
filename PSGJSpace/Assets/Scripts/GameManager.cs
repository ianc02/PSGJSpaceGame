using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject player;
    public Camera mainCamera;


    public GameObject BasicEnemy;
    private int numEnemies;
    
    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
