using UnityEngine;
using UnityEngine.SceneManagement;

//Gabriel Obaseki and Jonathan Ghattas
//LandingZone Prototype Script
//Version 1.07

//LandZone Object is used to detect successful landings
//LanderController is required for this script to function
//This Landing Zone script detects when the lander has landed successfully
//If the player lands on the landing zone, it calls the OnLanded method in the LanderController script
//It uses a trigger collider to detect when the lander enters the landing zone
//If the player lands anywhere else that's not on the lander zone, it will not call the OnLanded method, and will therefore be a crash

public class LandingZone : MonoBehaviour
{
    private LanderController lander;

    private void Awake()
    {
        lander = GetComponentInParent<LanderController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react once
        if (lander == null)
            return;

        if (
            other.CompareTag("Ground") ||
            other.CompareTag("LandZone") ||
            other.CompareTag("LandZoneX2") ||
            other.CompareTag("LandZoneX5")
        )
        {
            lander.OnLanded();

            // Score handling
            if (other.CompareTag("LandZone"))
                ScoreManager.Instance.AddPoint();
            else if (other.CompareTag("LandZoneX2"))
                ScoreManager.Instance.AddPointX2();
            else if (other.CompareTag("LandZoneX5"))
                ScoreManager.Instance.AddPointX5();

            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "LevelThree")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

}