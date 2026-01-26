using UnityEngine;

//Gabriel Obaseki and Jonathan Ghattas
//LanderController Prototype Script
//Version 1.01

public class LanderController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [Header("Movement Settings")]
    [SerializeField] private float thrustPower = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform thrustPoint;
    [SerializeField] private float thrustSmoothness = 0.2f;
    [SerializeField] private GameObject ThrustFire;

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
        if (Input.GetKey(KeyCode.Space))
        {

            if (!isThrusting)
            {
                // enable the thrustfire fx
                ThrustFire.SetActive(true);
                isThrusting = true;

              
            }
            // Direction the engine is pointing
            Vector3 thrustDirection = -thrustPoint.up;

            // Target velocity in the direction the engine is pointing
            Vector3 targetVelocity = -thrustDirection * thrustPower;

            // Smoothly move toward that velocity
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
                // disable the thrustfire fx
                ThrustFire.SetActive(false);
                isThrusting = false;
            }
           
        }
    }


    // Handle collision events
    private void OnCollisionEnter(Collision collision)
    {

        // Only process collisions if the lander is flying
        if (state != LanderState.Flying)
            return;

        // Check if collided with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Simple crash logic
            // For now, treat all ground as crash
            //Will change later to differentiate between landing and crashing
            state = LanderState.Crashed;
            Debug.Log("CRASHED");
        }
    }

    //!
    void Start()
    {

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

