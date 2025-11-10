using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance{
        // only this class can manage Instance,  access from anywhere
        get; private set;
    }

    public float initialSpeed = 5f;
    public float speedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Button retryButton;
    public Button resumeButton;
    public Button exitButton;
    public Button pauseButton;

    [SerializeField] AudioSource musicSource;
    public AudioClip background;
    [SerializeField] AudioSource SFXSource;
    public AudioClip jumpSFX;
    public AudioClip collideSFX;
    


    //reference to player and spawner
    private Player player;
    private Spawner spawner;

    // score, time milliseconds, better counting
    private float score;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject); // only one game instance should be created
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        musicSource.clip = background;
        musicSource.Play();

        newGame();
    }

    public void newGame()
    {

        //start fresh by deleting all obstacles
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject); 
            // not just (obstacle) -> that would only remove Obstacle script from game object 
            // (obstacles.gameObject) also removes collider, sprite renderer, etc.
        }

        Time.timeScale = 1;
        gameSpeed = initialSpeed;
        score = 0f;
        enabled = true;
        
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        musicSource.Stop();
        musicSource.Play();

        UpdateHighScore();
    }

    private void Update()
    {
        // debug mode to check speed increase
        gameSpeed += speedIncrease * Time.deltaTime;

        // the faster the game = higher difficulty = more score 
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5"); // D5: ensure always 5 digits

    }
    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

        musicSource.Stop();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {

        //"stores player preferences between game sessions", stores score data 
        float highScore = PlayerPrefs.GetFloat("highScore", 0); // default: 0

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
            // passing the value that is to be saved  
        }

        highScoreText.text = Mathf.FloorToInt(highScore).ToString("D5");
    }
    
    public void OnStartClick()
    {
        SceneManager.LoadScene("DinoGame");
    }

    public void OnExitClick()
    {
        musicSource.Stop();
        Application.Quit();
    }

    public void OnPauseClick()
    {
        // open pause panel
        retryButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0;
        musicSource.Pause();

    }

    public void onResumeClick()
    {
        retryButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
        musicSource.UnPause();
    }

    public void PlaySFX(AudioClip clipToPlay)
    {
        SFXSource.PlayOneShot(clipToPlay);
    }
}
