using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour
{
    // This serves to make managing gravity change more understandable by humans.
    public enum GravityDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        FORWARD,
        BACKWARD
    }

    public GravityDirection direction;
    public static GravitySwitcher instance;
    // Start is called before the first frame update
    private float currentTime;

    void Awake() 
    {
        instance = this;
        currentTime = 0f;
        direction = GravityDirection.DOWN; 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        CheckGravity();

        // For now, just change the gravity every 20 seconds. We'll worry about enemies when we cross that bridge.
        if (currentTime >= 20)
        {
            ChangeGravity(Random.Range(0, 6));
            currentTime = 0f;
            Debug.Log($"Gravity has changed direction to {direction}!");
        }
    }

    void CheckGravity()
    {
        // Update Physics.gravity according to the currently specified direction.
        switch (direction) {
            case GravityDirection.DOWN:
                Physics.gravity = new Vector3(0, -9.81f, 0);
                break;
            case GravityDirection.UP:
                Physics.gravity = new Vector3(0, 9.81f, 0);
                break;
            case GravityDirection.LEFT:
                Physics.gravity = new Vector3(-9.81f, 0, 0);
                break;
            case GravityDirection.RIGHT:
                Physics.gravity = new Vector3(9.81f, 0, 0);
                break;
            case GravityDirection.FORWARD:
                Physics.gravity = new Vector3(0, 0, 9.81f);
                break;
            case GravityDirection.BACKWARD:
                Physics.gravity = new Vector3(0, 0, -9.81f);
                break;
            default:
                Debug.LogError($"{direction} is not a valid direction for gravity!");
                break;
        }
    }

    void ChangeGravity(int numberSelected)
    {
        // Change the direction of gravity according to the randomly selected integer.
        switch (numberSelected) {
            case 0:
                direction = GravityDirection.DOWN;
                break;
            case 1:
                direction = GravityDirection.UP;
                break;
            case 2:
                direction = GravityDirection.LEFT;
                break;
            case 3:
                direction = GravityDirection.RIGHT;
                break;
            case 4:
                direction = GravityDirection.FORWARD;
                break;
            case 5:
                direction = GravityDirection.BACKWARD;
                break;
            default :
                Debug.Log($"Number must be between 0 and 5! Current number is {numberSelected}.");
                break;
        }
    }
}
