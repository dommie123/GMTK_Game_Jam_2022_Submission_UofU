using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;

    private float cooldown;

    public GameObject projectile;
    public Transform projectileSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (cooldown <= 0)
        {
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.position = projectileSpawnPoint.transform.position + projectileSpawnPoint.transform.forward;
            newProjectile.transform.forward = projectileSpawnPoint.transform.forward;
            newProjectile.GetComponent<ProjectileBehavior>().damage = damage;

            cooldown = fireRate;
        }
    }
}
