using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int currentWave;
    [SerializeField] private float spawnRate;
    private float cooldown;
    public GameObject enemy;

    // Start is called before the first frame update
    void Awake()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave != 0 && cooldown <= 0f)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = transform.position;
            newEnemy.transform.rotation = transform.rotation;

            cooldown = spawnRate;
            currentWave--;
        }
        else if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
