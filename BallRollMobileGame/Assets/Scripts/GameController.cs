using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Used to determine what material the paths should be using. 0 for default, 1 for desert. 2 for arctic. etc..
    /// </summary>
    public static int scenerySettings = 1; 

    /// <summary>
    /// Speed multiplier for ball acceleration this level
    /// </summary>
    public static float levelSpeed = 2f;

    /// <summary>
    /// Current level number
    /// </summary>
    public static int currentLevelNum = 1;

    /// <summary>
    /// Total number of paths in this level, including start and end paths
    /// </summary>
    public static int numPaths = 20;

    /// <summary>
    /// Player's current score
    /// </summary>
    public static int score = 0;

    /// <summary>
    /// Number of coins player has picked up this run
    /// </summary>
    public static int coins = 50;

    /// <summary>
    /// Number of times player has run each environment
    /// </summary>
    public static int[] environmentRuns;

    [Tooltip("Amount levelSpeed should increase each level completed")]
    [SerializeField] private float levelSpeedIncrease = 1f;

    [Tooltip("Amount numPaths should increase each time player goes through an environment again")]
    [SerializeField] private int numPathsIncrease = 5;

    /// <summary>
    /// Script that creates the path
    /// </summary>
    private PathSpawner ps;

    /// <summary>
    /// Script that controls ball movement
    /// </summary>    
    private BallController bc;

    /// <summary>
    /// Pause button
    /// </summary>
    public Button pauseButton;


    /// <summary>
    /// The play button
    /// </summary>
    public Button playButton;


    [Tooltip("Total number of environments")]
    public int numEnvironments = 3;

    [Tooltip("Number of paths at beginning of game")]
    public const int START_NUM_PATHS = 20;
    
    [Tooltip("Level speed at start of game")]
    public const float START_LEVEL_SPEED = 2f;

    private void Awake()
    {
        Debug.Log("awake of gamecontroller");
        bc = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
        bc.SetLevelSpeed(levelSpeed);
        ps = GameObject.Find("Path Parent").GetComponent<PathSpawner>();

        if (environmentRuns == null)
            environmentRuns = new int[numEnvironments];
    }

    private void OnEnable()
    {
        EventManager.OnLevelComplete += CompleteLevel;
    }

    private void OnDisable()
    {
        EventManager.OnLevelComplete -= CompleteLevel;
    }

    private void Start()
    {
        CreateLevel();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Creates level using given variables
    /// Can use this function to change materials/shaders/etc. as needed later
    /// </summary>
    private void CreateLevel()
    {
        ps.SpawnPaths(numPaths);
    }

    /// <summary>
    /// Ends the level 
    /// </summary>
    public void CompleteLevel()
    {
        // Increases current level number
        currentLevelNum++;

        // Increases level speed
        levelSpeed += levelSpeedIncrease;

        // Increases number of paths if player has seen all environments already
        if (currentLevelNum % numEnvironments == 0)
        {
            numPaths += numPathsIncrease;
        }

        // Increases run count for environment player just completed
        environmentRuns[scenerySettings]++;

        //scenerySettings++;
        scenerySettings = (scenerySettings % numEnvironments) + 1;

        StartCoroutine(StartNextLevel());
    }

    private IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(.15f);
        // Loads next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Resets level values
    /// </summary>
    public void ResetLevel()
    {
        Debug.Log("Reset Level");

        // Resets level progression variables
        levelSpeed = START_LEVEL_SPEED;
        numPaths = START_NUM_PATHS;
        currentLevelNum = 1;

        // Resets score and coins
        score = 0;
        coins = 0;

        // Resets environment run number counters
        for (int i = 0; i < environmentRuns.Length; i++)
        {
            environmentRuns[i] = 0;
        }
    }

    public void Pause(bool paused)
    {
        if(!paused)
        {
            Time.timeScale = 0;
            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
        else if(paused)
        {
            Time.timeScale = 1;
            pauseButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }
    }


}
