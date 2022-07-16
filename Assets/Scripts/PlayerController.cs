using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float mouseSensitivity;
    private Vector3 gravityVector;
    public float gravity;

    private Transform playerBody;
    private Vector3 stickToGround;
    private CharacterController controller;
    private float xRotation;
    private Vector3 playerRotation;
    public Transform playerCamera;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Awake() 
    {
        playerBody = this.transform;
        playerRotation = new Vector3(0, 0, 0);
        controller = GetComponent<CharacterController>();
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;

        groundDistance = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        stickToGround = GetGroundVelocity();
        gravityVector = GetGravityVector();
        //Debug.Log(gravityVector);

        UpdateMovement();
        UpdateRotation();
        UpdatePhysics();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump");
            Jump();
        }
    }

    private void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * speed * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        // Set the player rotation once with each change of gravity.
        if (GetPlayerRotation() != playerRotation)
        {
            playerRotation = GetPlayerRotation();
            transform.localRotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);
        }

        // FPS Camera stuff
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void UpdatePhysics()
    {
        // Check to stick player to ground if they are grounded, unless they decide to jump. Otherwise, laws of physics apply.
        if (isGrounded && PlayerVelocityIsIncreasing())
        {
            velocity = stickToGround;
        }
        else
        {
            velocity += gravityVector * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void Fire()
    {
        // TODO shoot current gun.
        Debug.Log("Firing all weapons!");
    }

    private void Jump()
    {
        switch (GravitySwitcher.instance.direction)
        {
            case GravitySwitcher.GravityDirection.UP:
                velocity.y = -Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            case GravitySwitcher.GravityDirection.DOWN:
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            case GravitySwitcher.GravityDirection.LEFT:
                velocity.x = Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            case GravitySwitcher.GravityDirection.RIGHT:
                velocity.x = -Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            case GravitySwitcher.GravityDirection.FORWARD:
                velocity.z = -Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            case GravitySwitcher.GravityDirection.BACKWARD:
                velocity.z = Mathf.Sqrt(jumpHeight * -2f * gravity);
                break;
            default:
                Debug.LogError($"{GravitySwitcher.instance.direction} is not a valid direction!");
                break;
        }
        Debug.Log(velocity);
    }

    private Vector3 GetGroundVelocity()
    {
        switch (GravitySwitcher.instance.direction)
        {
            case GravitySwitcher.GravityDirection.UP:
                return new Vector3(0, 2f, 0);
            case GravitySwitcher.GravityDirection.DOWN:
                return new Vector3(0, -2f, 0);
            case GravitySwitcher.GravityDirection.LEFT:
                return new Vector3(-2f, 0, 0);
            case GravitySwitcher.GravityDirection.RIGHT:
                return new Vector3(2f, 0, 0);
            case GravitySwitcher.GravityDirection.FORWARD:
                return new Vector3(0, 0, 2f);
            case GravitySwitcher.GravityDirection.BACKWARD:
                return new Vector3(0, 0, -2f);
            default:
                return new Vector3(0, 0, 0);   
        }
    }

    private Vector3 GetGravityVector()
    {
        switch (GravitySwitcher.instance.direction)
        {
            case GravitySwitcher.GravityDirection.UP:
                return new Vector3(0, -gravity, 0);
            case GravitySwitcher.GravityDirection.DOWN:
                return new Vector3(0, gravity, 0);
            case GravitySwitcher.GravityDirection.LEFT:
                return new Vector3(gravity, 0, 0);
            case GravitySwitcher.GravityDirection.RIGHT:
                return new Vector3(-gravity, 0, 0);
            case GravitySwitcher.GravityDirection.FORWARD:
                return new Vector3(0, 0, -gravity);
            case GravitySwitcher.GravityDirection.BACKWARD:
                return new Vector3(0, 0, gravity);
            default:
                return new Vector3(0, 0, 0);   
        }
    }

    private Vector3 GetPlayerRotation()
    {
        switch (GravitySwitcher.instance.direction)
        {
            case GravitySwitcher.GravityDirection.UP:
                return new Vector3(180, 0, 0);
            case GravitySwitcher.GravityDirection.DOWN:
                return Vector3.zero;
            case GravitySwitcher.GravityDirection.LEFT:
                return new Vector3(0, 0, -90);
            case GravitySwitcher.GravityDirection.RIGHT:
                return new Vector3(0, 0, 90);
            case GravitySwitcher.GravityDirection.FORWARD:
                return new Vector3(-90, 0, 0);
            case GravitySwitcher.GravityDirection.BACKWARD:
                return new Vector3(90, 0, 0);
            default:
                return new Vector3(0, 0, 0);   
        }
    }

    private bool PlayerVelocityIsIncreasing()
    {
        switch (GravitySwitcher.instance.direction)
        {
            case GravitySwitcher.GravityDirection.UP:
                return velocity.y > 0;
            case GravitySwitcher.GravityDirection.DOWN:
                return velocity.y < 0;
            case GravitySwitcher.GravityDirection.LEFT:
                return velocity.x < 0;
            case GravitySwitcher.GravityDirection.RIGHT:
                return velocity.x > 0;
            case GravitySwitcher.GravityDirection.FORWARD:
                return velocity.z > 0;
            case GravitySwitcher.GravityDirection.BACKWARD:
                return velocity.z < 0;
            default:
                return false;   
        }
    }
}

