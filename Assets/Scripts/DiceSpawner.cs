using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private float force;
    public GameObject gravityDie;
    public GameObject playerDie;
    public GameObject gunDie;
    void Awake()
    {
        // Choose type of die to spawn
        int dieType = Random.Range(0, 3);
        GameObject newDie;

        // Spawn the die.
        switch (dieType)
        {
            case 0:
                newDie = Instantiate(gravityDie);
                break;
            case 1:
                newDie = Instantiate(playerDie);
                break;
            default:
                newDie = Instantiate(gunDie);
                break;
        }

        // Toss the die into the air and hope it behaves like one.
        newDie.transform.position = transform.position;
        newDie.GetComponent<Rigidbody>().AddForce(transform.up * force);
    }
}
