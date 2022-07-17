using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public static EnemyHealthManager instance;
    public int health;
    private EnemyController enemy;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        enemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            enemy.currentState = EnemyController.EnemyState.Dead;
        }
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= 0)
        {
            health -= damage;
        }
    }
}
