using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject fade;
    Animator animFade;



    //---------------------------------------------SCORE

    [SerializeField] Text txtScore;
    [SerializeField] Text txtHScore;

    public int score;

    public string totalScoreLevel;

    const int SCORE_ENEMY_CHIC = 15;
    const int SCORE_ENEMY_PIG = 20;
    const int SCORE_ENEMY_EG = 25;
    const int SCORE_ENEMY_VU = 25;
    const int SCORE_ENEMY_PRAL = 50;
    const int SCORE_ITEM_CAR = 5;
    const int SCORE_ITEM_GEM = 15;

    const string DATA_FILE = "data.json";

    GameData gameData;


    //---------------------------------------------MESSAGE

    [SerializeField] Text txtMessage;


    bool gameOver;
    bool paused;

    public bool isGameOver() { return gameOver; }
    public bool isGamePaused() { return paused; }

    [SerializeField] Image mesPanelEnd;
    [SerializeField] Image mesPanelTitle;


    //-----------------------------------------------GEM

    [SerializeField] Image[] imgGem;

    int gem = 0;

    const int MAX_GEM = 3;


    //-----------------------------------------------LIVES


    [SerializeField] Image[] imgLives;
    const int LIVES = 3;
    public int lives = LIVES;

    const int EXTRA_LIVE = 200;
    bool extra;

    [SerializeField] private AudioClip sfxExtra;

    [SerializeField] private AudioClip sfxGameOver;

    [SerializeField] private GameObject player;

    [SerializeField] private Image extraLive;

    Collider2D col;

    Animator anim;

    public static GameManager GetInstance()
    {
        return instance;
    }




    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                totalScoreLevel = "410";
                break;
            case 2:
                totalScoreLevel = "790";
                break;
            case 3:
                totalScoreLevel = "620";
                break;
            case 4:
                totalScoreLevel = "810";
                break;
        }
    }

    private void Start()
    {
        animFade = fade.GetComponent<Animator>();
        Cursor.visible = false;

        col = player.GetComponent<Collider2D>();
        anim = player.GetComponent<Animator>();

        mesPanelTitle.gameObject.SetActive(!mesPanelTitle.gameObject.activeSelf);
        gameData = LoadData();
        StartCoroutine(DelayMsg(mesPanelTitle));
    }

    void Update()
    {
        
        if (gameOver && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DelayFadeOut(0));

        }

        if (Input.GetAxis("Mouse X") > 1 || Input.GetAxis("Mouse Y") > 1)
        {

           Cursor.visible = true;

        }

    }

    GameData LoadData()
    {
        animFade.SetTrigger("isGameIn");

        if (File.Exists(DATA_FILE))
        {
            string fileText = File.ReadAllText(DATA_FILE);
            return JsonUtility.FromJson<GameData>(fileText);
        }
        else
            return new GameData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(DATA_FILE, json);

    }

    public void AddScore(string tag)
    {
        int pts = 0;

        switch (tag)
        {
            case "Chameleon":
                pts = SCORE_ENEMY_CHIC;
                break;
            case "Pig":
                pts = SCORE_ENEMY_PIG;
                break;
            case "Eagle":
                pts = SCORE_ENEMY_EG;
                break;
            case "Vulture":
                pts = SCORE_ENEMY_VU;
                break;
            case "EnemyPral":
                pts = SCORE_ENEMY_PRAL;
                break;
            case "Carrot":
                pts = SCORE_ITEM_CAR;
                break;
            case "Gem":
                pts = SCORE_ITEM_GEM;
                break;
        }

        score += pts;

        if (!extra && score >= EXTRA_LIVE)
        {
            ExtraLife();
        }


        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            if (score > gameData.hscore1)
            {
                gameData.hscore1 = score;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {

            if (score > gameData.hscore2)
            {
                gameData.hscore2 = score;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {

            if (score > gameData.hscore3)
            {
                gameData.hscore3 = score;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (score > gameData.hscore4)
            {
                gameData.hscore4 = score;
            }
        }


        gameData.hscore = gameData.hscore1 + gameData.hscore2 + gameData.hscore3 + gameData.hscore4;

        
    }

    public void ExtraLife()

    {
        extra = true;
        lives++;
        AudioSource.PlayClipAtPoint(sfxExtra, Camera.main.transform.position, 0.5f);
        extraLive.gameObject.SetActive(!extraLive.gameObject.activeSelf);



        StartCoroutine(DelayMsg(extraLive));
    }

    public void LoseLife()
    {
        lives--;

        if (lives == 0)
        {
            StartCoroutine(GameOver());
        }

    }

    public IEnumerator GameOver()
    {

        col.enabled = false;

        anim.SetTrigger("isDead");

        Destroy(player, 1f);

        Camera.main.GetComponent<AudioSource>().Stop();

        AudioSource.PlayClipAtPoint(sfxGameOver, Camera.main.transform.position, 0.3f);

        mesPanelEnd.gameObject.SetActive(!mesPanelEnd.gameObject.activeSelf);

        txtMessage.text = "GAME OVER\n PULSA <INT>";

        yield return new WaitForSeconds(5.0f);

        gameOver = true;

        Cursor.visible = true;

        SaveData();

    }


    public void GemCol()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            if (gem < MAX_GEM)
            {
                gem++;

                gameData.gem1 = gem;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {

            if (gem < MAX_GEM)
            {
                gem++;

                gameData.gem2 = gem;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (gem < MAX_GEM)
            {
                gem++;

                gameData.gem3 = gem;
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {

            if (gem < MAX_GEM)
            {
                gem++;

                gameData.gem4 = gem;
            }
        }


        gameData.hgem = gameData.gem1 + gameData.gem2 + gameData.gem3 + gameData.gem4;

    }



    private void OnGUI()
    {
        //ACTIVAR ICONOS DE LAS VIDAS QUE TENGAMOS

        for (int i = 0; i < imgLives.Length; i++)
        {

            imgLives[i].enabled = i < lives;

        }


        for (int i = 0; i < imgGem.Length; i++)
        {

            imgGem[i].enabled = i < gem;

        }

        //MOSTRAR PUNTUACIÓN JUGADOR

        txtScore.text = string.Format("{0,4:D4}", score + "/" + totalScoreLevel);


        //MOSTRAR PUNTUACIÓN MÁXIMA

        txtHScore.text = string.Format("{0,4:D4}", gameData.hscore);

    }

    public void PauseGame()
    {
        if (!gameOver && paused == false)
        {
            paused = true;

            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if (!gameOver && paused)
        {
            paused = false;

            Time.timeScale = 1;
        }


    }

    public void MenuInicial()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayFadeOut(0));
    }

    public IEnumerator DelayMsg(Image panel)
    {
        yield return new WaitForSeconds(2.0f);
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);

    }
    public IEnumerator DelayFadeOut(int scene)
    {
        animFade.SetTrigger("isGameOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(scene);
    }
}
