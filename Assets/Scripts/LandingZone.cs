using UnityEngine;

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
        //detect if the lander has hit  ground
        if (other.CompareTag("Ground"))
        {
            lander.OnLanded();
            // load scene "next level" after 2 seconds
        }

        //detect if the lander has hit the land zone
        if (other.CompareTag("LandZone"))
        {
            lander.OnLanded();
            // add point to score manager
            ScoreManager.Instance.AddPoint();
            Debug.Log("+1 score");
        }

        if (other.CompareTag("LandZoneX2"))
        {
            lander.OnLanded();
            // add point to score manager
            ScoreManager.Instance.AddPointX2();
            Debug.Log("+1 score");
        }

        if (other.CompareTag("LandZoneX5"))
        {
            lander.OnLanded();
            // add point to score manager
            ScoreManager.Instance.AddPointX5();
            Debug.Log("+1 score");
        }
    }
}