using UnityEngine;
using TMPro;

public class PausePlaythroughData : MonoBehaviour
{
    #region -- Gameplay UI --
    [Header("Gameplay UI Objects")]
    [Tooltip("Playthrough Data object shown during gameplay")]
    [SerializeField] private GameObject gamePlaythroughData;

    [Tooltip("Score text shown during gameplay")]
    [SerializeField] private TextMeshProUGUI gameScoreText;

    [Tooltip("Score text shown during gameplay")]
    [SerializeField] private TextMeshProUGUI gameCoinText;

    [Tooltip("Life hearts shown during gameplay")]
    [SerializeField] private Animator[] gameHearts;
    #endregion

    #region -- Pause UI -- 
    [Header("Pause UI Objects")]
    [Tooltip("Score text shown while paused")]
    [SerializeField] private TextMeshProUGUI pauseScoreText;

    [Tooltip("Score text shown while paused")]
    [SerializeField] private TextMeshProUGUI pauseCoinText;

    [Tooltip("Life hearts shown while paused")]
    [SerializeField] private GameObject[] pauseHearts;
    #endregion

    private void OnEnable()
    {
        // Hides gameplay playthrough data while keeping heart data 
        foreach (Animator heart in gameHearts)
        {
            heart.keepAnimatorControllerStateOnDisable = true;
        }
        gamePlaythroughData.SetActive(false);

        // Sets pause playthrough data equal to current gameplay playthrough data
        pauseScoreText.text = gameScoreText.text;
        pauseCoinText.text = gameCoinText.text;
        for(int i = 0; i < gameHearts.Length; i++)
        {
            pauseHearts[i].SetActive(!gameHearts[i].GetBool("break"));
            Debug.Log("heart " + i + " is active = " + !gameHearts[i].GetBool("break"));
        }
    }

    private void OnDisable()
    {
        // Shows gameplay playthrough data
        gamePlaythroughData.SetActive(true);
    }
}
