using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public int best;
    public int score;
    public int currentStage = 0;
    

    public static GameManager singleton;
    // Start is called before the first frame update
    void Awake()
    {
        // Advertisement.Initialize("349047");
        if(singleton == null) {
            singleton = this;
        } else if(singleton != this) {
            Destroy(gameObject);
        }
        best = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel() {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next level was called");
    }
    
    public void RestartLevel() {
        Debug.Log("Game restarted");
        //show ads
        // Advertisement.Show();
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd) {
        score += scoreToAdd;

        if(score > best) {
            best = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

}
