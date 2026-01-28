using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    int score = 0;
    int highscore = 0;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // Retrieve highscore from PlayerPrefs
        highscore = PlayerPrefs.GetInt("highscore", 0);
        // Initialize score display
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    // Update is called once per frame
    public void AddPoint()
    {
        score += 250;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highscore)
        {
           PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void AddPointX2()
    {
        score += 500;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void AddPointX5()
    {
        score += 1250;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
