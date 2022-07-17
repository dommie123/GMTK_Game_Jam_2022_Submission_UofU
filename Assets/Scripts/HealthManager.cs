using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int health;
    private PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        player.IsDead = (health <= 0);
    }

    public void ApplyDamage(int damage)
    {
        Debug.Log("Ouch!");
        
        if (damage >= 0)
        {
            health -= damage;
        }
    }
}
