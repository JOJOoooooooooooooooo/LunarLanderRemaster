using UnityEngine;
using UnityEngine.SceneManagement;

//Gabriel Obaseki and Jonathan Ghattas
//LanderController Prototype Script
//Version 1.055

public class LanderController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //These serialized fields can be adjusted in the Unity Inspector
    //This is the movement settings for the lander
    [Header("Movement Settings")]
    [SerializeField] private float thrustPower = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform thrustPoint;
    [SerializeField] private float thrustSmoothness = 0.2f;
    [SerializeField] private GameObject ThrustFire;


    //These serialized fields can be adjusted in the Unity Inspector
    //This is the fuel settings for the lander
    [Header("Fuel Settings")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuelConsumptionRate = 10f;
    [SerializeField] private UnityEngine.UI.Slider fuelBar;


    //Current fuel level
    private float currentFuel;

    //Audio Source for thrust sound effect
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource BeepAudio;
    [SerializeField] private AudioSource BGAudio;


    private Rigidbody Rb;
    private bool isThrusting = false;

    //Set state for the Lander
    private enum LanderState
    {
        Flying,
        Crashed,
        Landed
    }

    //set initial Landerstate to Flying
    private LanderState state = LanderState.Flying;

    void Awake()
    {
        //Get the Rigidbody component
        Rb = GetComponent<Rigidbody>();
    }

    public void OnLanded()
    {
        if (state != LanderState.Flying)
            return;

        state = LanderState.Landed;
        Debug.Log("LANDED");
    }

    // Handle rotation input for the lander
    private void HandleRotationInput()
    {
        float rotateInput = 0f;

        //if A or LeftArrow is pressed, rotate left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rotateInput = 1f;

        //if D or RightArrow is pressed, rotate right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rotateInput = -1f;

        // Rotate around Z axis (like classic lander)
        transform.Rotate(0f, 0f, rotateInput * rotationSpeed * Time.deltaTime);
    }

    // Handle thrust input for the lander
    private void HandleThrustInput()
    {
        //if Space is pressed, apply upward thrust
        if (Input.GetKey(KeyCode.Space) && currentFuel > 0f)
        {
            // Consume fuel
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Max(currentFuel, 0f);
            fuelBar.value = currentFuel;

           
          

            if (currentFuel <= 60f)
            {
                fuelBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }

            if (currentFuel <=30f)
            {
                BeepAudio.Play();
                fuelBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }

            // Activate thrust effects
            if (!isThrusting)
            {
                ThrustFire.SetActive(true);
                AudioSource.Play();
                isThrusting = true;
            }

            Vector3 thrustDirection = -thrustPoint.up;
            Vector3 targetVelocity = -thrustDirection * thrustPower;

            Rb.linearVelocity = Vector3.Lerp(
                Rb.linearVelocity,
                targetVelocity,
                thrustSmoothness
            );
        }
        else
        {
            if (isThrusting)
            {
                ThrustFire.SetActive(false);
                AudioSource.Stop();
                isThrusting = false;
            }
        }
      
    }


    // Handle collision events
    //Uses LandingZone script to detect successful landings
    //Prototype Script detection
    //may or may not change in future versions to include more complex collision detection
    private void OnCollisionEnter(Collision collision)
    {
        //if the lander is NOT flying, ignore collisions

        if (state != LanderState.Flying)
        {
            BeepAudio.Stop();
            BGAudio.Stop();
            return;
        }

        // If the collision is NOT the LandZone, it's a crash
        if (!collision.collider.CompareTag("LandZone"))
        {

            state = LanderState.Crashed;
            Debug.Log("CRASHED");
            BGAudio.Stop();
            // Load Game Over scene
            SceneManager.LoadScene("GameOver");

        }
    }

    
    void Start()
    {
        BGAudio.Play();
        fuelBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.green;

        currentFuel = maxFuel;
        fuelBar.maxValue = maxFuel;
        fuelBar.value = maxFuel;
    }

    // Update is called once per frame since it is a MonoBehaviour
    //Landerstate is continuously checked to see if it is flying
    void Update()
    {
        // Only process input if the lander is flying
        if (state != LanderState.Flying)
            
            return;

        //Calls HandleRotationInput and HandleThrustInput methods
        HandleRotationInput();
        HandleThrustInput();
    }
  // Update is called once per frame

}

