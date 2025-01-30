using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using TMPro;

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
    public GameObject boostTimer;
    public GameObject boostTimerAmount;
    public Color boostTimerColorGood;
    public Color boostTimerColorBad;
    public float boostStartTime;

    public GameObject DuringGameOverlay;
    public GameObject StoryIntro;
    public GameObject speakerName;
    public GameObject DialogueText;
    public GameObject speakerSprite;
    public Sprite CapnSprite;
    public Sprite ShipSprite;
    private bool watchedIntro = false;

    public GameObject player;
    public Camera mainCamera;
    public Camera miniMapCam;
    public GameObject PlayerDeathPartSys;
    public bool playerCanMove = false;

    public float score;
    public TextMeshProUGUI scoreValue;

    public GameObject MainMenu;
    public GameObject InGameOverlay;
    public GameObject HighScoreGO;
    public GameObject HighScoreVal;
    private float highestOfScores = 0;

    public GameObject SettingsScreen;
    public GameObject SettingsScreenMainMenuButton;
    public GameObject SettingsScreenXButton;


    public GameObject BasicEnemy;
    public GameObject ShipBit;
    public GameObject Shooter;
    private int numEnemies;
    private List<GameObject> enemies;

    public bool running;

    private int maxNumEnemies = 30;
    private int lowOddShooter = 20;
    private int difficultyIncrease = 0;


    

    private Vector3 originPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        boost.GetComponent<Slider>().value = 1;
        health.GetComponent<Slider>().value = 1;    
        originPlayerPos = player.transform.position;
        running = false;
        enemies = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (running)
            {
                
                
                    running = false;
                    PauseTheGame();
                
            }
            else
            {
                if (MainMenu.active)
                {
                    StartTheGame();
                }
                else
                {
                    running = true;
                    UnpauseTheGame();
                }
            }

        }
        if (running)
        {
            difficultyIncrease = (int)Mathf.Round(score / 800);
            boost.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().boostAmount / player.GetComponent<PlayerMovement>().maxBoost;
            health.GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().healthAmount / player.GetComponent<PlayerMovement>().maxHealth;
            if (numEnemies < maxNumEnemies + difficultyIncrease)
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
            scoreValue.text = Mathf.Round(score).ToString();
            updateBoostTimer();
        }
    }

    public void PauseTheGame()
    {
        running = false;
        SettingsScreen.SetActive(true);
        SettingsScreenMainMenuButton.SetActive(true);
        SettingsScreenXButton.SetActive(false);
        
    }
    public void BackToMainMenu()
    {
        SettingsScreenMainMenuButton.SetActive(false);
        SettingsScreen.SetActive(false);
        MainMenu.SetActive(true);
        SettingsScreenXButton.SetActive(true);
        InGameOverlay.SetActive(false);
        running = false;

    }
    public void StartTheGame()
    {
        MainMenu.SetActive(false);
        SettingsScreen.SetActive(false);
        InGameOverlay.SetActive(true);
        if (!watchedIntro)
        {

            Dialogue(0);
        }
        else
        {
            Debug.Log("w");
            AudioManager.instance.InitializeMusic(FMODEvents.instance.music);
            player.transform.position = originPlayerPos;
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);

            }
            player.GetComponent<PlayerMovement>().healthAmount = player.GetComponent<PlayerMovement>().maxHealth;
            score = 0;
            numEnemies = 0;
            UnpauseTheGame();
        }
        
       
    }
    public void UnpauseTheGame()
    {
        
        playerCanMove = true;
        running = true;
        MainMenu.SetActive(false);
        SettingsScreen.SetActive(false);
        InGameOverlay.SetActive(true);
    }

    void SpawnEnemies()
    {
        Vector3 p = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f),0);
        Quaternion q = Quaternion.Euler(player.transform.position.z - p.z,0f,0f);
        if (Vector3.Distance(player.transform.position, p) > 20f)
        {
            int r = Random.Range(0, 100);
            if (r < lowOddShooter+difficultyIncrease)
            {
                enemies.Add(Instantiate(Shooter, p, q));
            }
            else
            {
                enemies.Add(Instantiate(BasicEnemy, p, q));
                
            }
            
            numEnemies++;
        }
        
    }

    public void DestroyEnemy(GameObject enemy)
    {
        Vector3 pos = enemy.transform.position;
        enemy.GetComponent<ParticleSystem>().Play();
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        
        
        for (int i = 0; i < Random.Range(2, 6); i++)
        {
            Vector3 randpos = new Vector3(pos.x + Random.Range(-5f, 5f), pos.y + Random.Range(-5f, 5f));
            Quaternion q = Quaternion.Euler(player.transform.position.z - randpos.z, 0f, 0f);
            Instantiate(ShipBit, randpos, q);
        }
        numEnemies--;
    }
    public void addToScore(float value)
    {
        if (playerCanMove)
        {
            score += value;
        }
    }

    public void GameIsOver()
    {

        PlayerDeathPartSys.GetComponent<ParticleSystem>().Play();   
        playerCanMove = false;
        StartCoroutine(WFS(5));
        
    }

    IEnumerator WFS(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        BackToMainMenu();
        HighScoreGO.SetActive(true);
        if (score > highestOfScores)
        {
            HighScoreVal.GetComponent<TextMeshProUGUI>().text = Mathf.Round(score).ToString();
            highestOfScores = score;
        }
        StopAllCoroutines();
    }
    IEnumerator ActualWFS(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }

    public void updateBoostTimer()
    {
        float amt = (Time.time - boostStartTime) / 0.75f;
        float minAmt = Mathf.Min(1, amt);
        boostTimer.GetComponent<Slider>().value = minAmt;
        if (amt <= 1)
        {
            boostTimerAmount.GetComponent<Image>().color = Color.Lerp(boostTimerColorBad, boostTimerColorGood, Mathf.Min(1, (Time.time - boostStartTime) / 0.75f));
        }
        else
        {
            boostTimerAmount.GetComponent<Image>().color = Color.Lerp(boostTimerColorBad, boostTimerColorGood, Mathf.Max(0,(2-amt)));
        }
        
    }

    public void Dialogue(int section)
       
    {
        Debug.Log(section);
        if (section == 0)
        {
            
            speakerSprite.GetComponent<Image>().sprite = CapnSprite;
            speakerName.GetComponent<TextMeshProUGUI>().text = "Captain";
            DialogueText.GetComponent<TextMeshProUGUI>().text = "$%*&!. That REALLY HURT!";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=2)
        {
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Ship. How we doin'?";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=4)
        {
            speakerSprite.GetComponent<Image>().sprite = ShipSprite;
            speakerName.GetComponent<TextMeshProUGUI>().text = "Ship";
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Friendly Fleet Destroyed. Life Support: Offline. Energy Weapons: Offline. Ballistics: Offline.  Communication: Offline. Cryo-Chamber: Offl...";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=6)
        {
            speakerSprite.GetComponent<Image>().sprite = CapnSprite;
            speakerName.GetComponent<TextMeshProUGUI>().text = "Captain";
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Okay yeah I get it. What's working?";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=8)
        {
            speakerSprite.GetComponent<Image>().sprite = ShipSprite;
            speakerName.GetComponent<TextMeshProUGUI>().text = "Ship";
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Forward and pivot engines: Online. Navigation: Damaged. Hull: Damaged. Shield: Low Power. Boosters: Low Power. Seat Warmer: Online";
            StartCoroutine(WaitForInput(section));
        }

        else if (section <=10)
        {
            speakerSprite.GetComponent<Image>().sprite = CapnSprite;
            speakerName.GetComponent<TextMeshProUGUI>().text = "Captain";
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Give 40 of my best years and this is the thanks I get. Whatever. I can make this work... for a bit.";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=12)
        {
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Connect Thrusters and Shield then direct all available power to that system. NOT the seat warmer. I'm going out comfortable.";
            StartCoroutine(WaitForInput(section));
        }
        else if (section <=14)
        {
            DialogueText.GetComponent<TextMeshProUGUI>().text = "Get ready to scavange debris, we're going to need it.";
            StartCoroutine(WaitForInput(section));
        }
        else
        {
            DuringGameOverlay.SetActive(true);
            StoryIntro.SetActive(false);
            watchedIntro = true;
            StartTheGame();
        }
    }
    
    IEnumerator WaitForInput(int prevSection)
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        Dialogue(prevSection + 1);

    }
}
