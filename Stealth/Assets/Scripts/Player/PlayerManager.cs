using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController characterController;
    public Camera fpsCamera;

    // floats
    // Movement
    [Header("Movement Floats")]

    public float Jump_Height;
    [HideInInspector] public float Gravity = 15;
    [HideInInspector] public float current_Speed;
    public float timeInBetweenSlides;
    public float timeInSlide;
    public float SlideSpeed;
    public float SlideFricton;
    public float Sprint_LerpScaleTime;
    public float walkSpeed;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    // Stats effected by metals
    public float strength = 10f;
    public float healthRegen = 10f;
    public float fov = 90f;
    public float visionRange = 30f;
    // Bools
    // Movement
    public bool isRunning = false;
    public bool isSliding = false;
    public bool isGrounded;

    // Vector3s
    [HideInInspector] public Vector3 velocity;
    public Vector3 primaryVelocity;
    [HideInInspector] public Vector3 move_configs;



    // GameObjects
    public GameObject cameraSlidePosition;
    public GameObject originalCameraSlidePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fpsCamera.fieldOfView = fov;
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);

        var lastTimeTillSlid = Time.time;
        // Check on grounding state
        isGrounded = characterController.isGrounded;

        // Resets velocity of player every frame
        if (isGrounded)
            velocity.y = -2f;


        // Jump function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                velocity.y = Mathf.Sqrt(Jump_Height * -2f * Gravity);
        }

        // Sprint Function
        if (Input.GetKey(KeyCode.LeftShift) && move_configs != Vector3.zero)
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        // Sliding Function
        if (Input.GetKeyDown(KeyCode.LeftControl) && lastTimeTillSlid > timeInBetweenSlides && isGrounded)
        {
            lastTimeTillSlid = 0f;
            isSliding = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
        }

        // Run Sliding Function
        switch (isSliding)
        {
            case true:
                Slide();
                break;
            default:
                //fpsCamera.transform.position = Vector3.Lerp(fpsCamera.transform.position, originalCameraSlidePosition.transform.position, Time.deltaTime * Sprint_LerpScaleTime);
                timeInSlide = 0;
                break;
        }





        // Run Running Function
        switch (isRunning)
        {
            case true:
                Sprint();
                AlterFOV(true);
                break;
            default:
                current_Speed = walkSpeed;
                AlterFOV(false);
                break;

        }

        // Takes two keyboard inputs for movement
        float Input_X = Input.GetAxis("Horizontal");
        float Input_Z = Input.GetAxis("Vertical");

        move_configs = transform.right * Input_X + transform.forward * Input_Z;

        // Relays inputs to the controller to move
        characterController.Move(move_configs * current_Speed * Time.deltaTime);


        // Apply gravity
        velocity.y -= Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        // Updating Last Time Till Slid based on the player's velocity;
        if (!isSliding)
            lastTimeTillSlid += (Time.deltaTime * primaryVelocity.magnitude) / 2f;



    }




    private void AlterFOV(bool active)
    {
        switch (active)
        {
            case true:
                fpsCamera.fieldOfView = fov + 5;

                break;
            case false:
                fpsCamera.fieldOfView = fov;
                break;
        }
    }


    private void Slide()
    {
        timeInSlide += Time.deltaTime;
        float NetSlideSpeed = SlideSpeed - (SlideFricton * timeInSlide);
        if (NetSlideSpeed < 0)
        {
            isSliding = false;
            return;
        }


        Vector3 slide = transform.forward * NetSlideSpeed;
        characterController.Move(slide * Time.deltaTime);
    }



    private void Sprint()
    {
        current_Speed = walkSpeed * 2;
    }
}
