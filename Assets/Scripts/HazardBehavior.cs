using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager.instance.ApplyDamage(damage);
        }
        if (other.gameObject.tag == "Enemy")
        {
            EnemyHealthManager.instance.ApplyDamage(damage);
        }
    }
}
