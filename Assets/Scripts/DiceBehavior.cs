using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    [SerializeField] private string effect;
    public int RolledNumber {get; set;}
    private Rigidbody rb;
    private bool hasLanded;
    private float wakeTime;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hasLanded = false;
        wakeTime = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        wakeTime -= Time.deltaTime;
        // TODO check to see if die is moving. If it stops moving or is barely moving at all, return number that faces upward.
        if (rb.velocity.magnitude <= 0 && !hasLanded && wakeTime <= 0)
        {
            hasLanded = true;
            EffectManager.instance.ProcessEffect(effect, RolledNumber);
            Destroy(gameObject, 0);
        }
    }
}
