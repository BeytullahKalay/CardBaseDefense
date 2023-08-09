using TMPro;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject newHighScoreText;

    private const string HIGHSCORE = "HighScore";
    
    private int _score;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(HIGHSCORE))
        {
            PlayerPrefs.SetInt(HIGHSCORE,0);
        }
    }

    private void OnEnable()
    {
        EventManager.WaveCompleted += IncreaseScore;
        
        EventManager.GameOver += AssignScoreToText;
        EventManager.GameOver += CheckHighScore;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= IncreaseScore;

        EventManager.GameOver -= AssignScoreToText;
        EventManager.GameOver -= CheckHighScore;
    }

    private void IncreaseScore(bool waveCleared)
    {
        if (waveCleared) _score++;
    }

    private void AssignScoreToText()
    {
        scoreText.text = "Your Score: " + _score;
    }

    private void CheckHighScore()
    {
        if (_score > PlayerPrefs.GetInt(HIGHSCORE))
        {
            PlayerPrefs.SetInt(HIGHSCORE,_score);
            newHighScoreText.SetActive(true);
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(HIGHSCORE);
    }
}
