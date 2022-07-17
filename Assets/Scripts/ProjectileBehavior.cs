using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private float speed;
    private float lifespan;

    public int damage;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 20;
        lifespan = 5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;   

        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject, 0);

        if (other.gameObject.tag == "Player")
        {
            HealthManager.instance.ApplyDamage(damage);
        }
    }
}
