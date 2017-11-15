using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private GameObject quitbutton;
    private Text levelText;
    private GameObject levelImage;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;
    private Text foodText;
    private GameObject restartbutton;
    private GameObject infobutton; //
    private bool destroy = false;
    
    IEnumerator Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        yield return new WaitForSeconds(3);
        canvas.SetActive(true);
        GameObject loading = GameObject.Find("Loading");
        Destroy(loading);
        destroy = true;
    }

    public void LevelReset()
    {
        Player.FoodReset();

        enabled = true;
        level = 0;
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        foodText.text = "Food: " + playerFoodPoints;
        SceneManager.LoadScene("main");
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        quitbutton = GameObject.Find("QuitButton");
        restartbutton = GameObject.Find("ReStartButton");
        infobutton = GameObject.Find("InfoButton"); //
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        if (levelText.text != "Day 1")
        {
            GameObject loading = GameObject.Find("Loading");
            Destroy(loading);
        }
        else if (destroy == true) 
        {
            GameObject loading = GameObject.Find("Loading");
            Destroy(loading);
        }

        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        quitbutton.SetActive(true);
        restartbutton.SetActive(true);
        infobutton.SetActive(true); //
        levelImage.SetActive(true);
        enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            float volume = SoundManager.instance.musicSource.volume;
            SoundManager.instance.musicSource.volume = (float)System.Math.Round((volume - 0.1f), 1);
        }
        else if (Input.GetKeyUp(KeyCode.PageUp))
        {
            float volume = SoundManager.instance.musicSource.volume;
            SoundManager.instance.musicSource.volume = (float)System.Math.Round((volume + 0.1f), 1);
        }
        
        Text volumeText = GameObject.Find("VolumeText").GetComponent<Text>();
        volumeText.text = "Volume: " + SoundManager.instance.musicSource.volume * 100;

        if (playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
}