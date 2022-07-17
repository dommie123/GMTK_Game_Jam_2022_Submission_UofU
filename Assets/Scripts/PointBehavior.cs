using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehavior : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private float rotationSpeed;

    private void Update() 
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPointWallet.instance.Points += pointValue;
            Destroy(gameObject, 0);
        }   
    }
}
