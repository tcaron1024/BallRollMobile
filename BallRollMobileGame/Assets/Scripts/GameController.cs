using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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

    [Tooltip("Total number of environments")]
    public int numEnvironments = 3;

    [Tooltip("Number of paths at beginning of game")]
    public const int START_NUM_PATHS = 20;
    
    [Tooltip("Level speed at start of game")]
    public const float START_LEVEL_SPEED = 2f;


    //private void Awake()
    //{
    //    GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

    //    if (objs.Length > 1)
    //    {
    //        Destroy(this.gameObject);
    //    }

    //    DontDestroyOnLoad(this.gameObject);
    //}
    private void Start()
    {
        print("start of gamecontroller");
        bc = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
        ps = GameObject.Find("Path Parent").GetComponent<PathSpawner>();
        CreateLevel();
    }

    /// <summary>
    /// Creates level using given variables
    /// Can use this function to change materials/shaders/etc. as needed later
    /// </summary>
    private void CreateLevel()
    {
        ps.SpawnPaths(numPaths);
        bc.SetLevelSpeed(levelSpeed);
    }

    /// <summary>
    /// Ends the level 
    /// </summary>
    public void EndLevel()
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

        // Loads next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Resets level values
    /// </summary>
    public void ResetLevel()
    {
        levelSpeed = START_LEVEL_SPEED;
        numPaths = START_NUM_PATHS;
        currentLevelNum = 1;
    }
}
