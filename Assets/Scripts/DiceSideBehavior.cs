using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideBehavior : MonoBehaviour
{
    public int number;
    public LayerMask groundMask;
    [SerializeField] private float groundDistance;
    private bool isGrounded;
    private DiceBehavior diceBehavior;
    
    // Start is called before the first frame update
    void Start()
    {
        diceBehavior = GetComponentInParent<DiceBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded)
        {
            // Debug.Log(GetRolledNumber());
            diceBehavior.RolledNumber = GetRolledNumber();
        }
    }

    // Here, we want the side of the die opposite to the one touching the 
    // ground's value, since that will be the one facing "up".
    int GetRolledNumber()
    {
        switch (number)
        {
            case 1: 
                return 6;
            case 2:
                return 5;
            case 3:
                return 4;
            case 4:
                return 3;
            case 5:
                return 2;
            case 6:
                return 1;
            default:
                return -1;
        }
    }
}
