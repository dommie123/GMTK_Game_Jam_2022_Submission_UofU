using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    [SerializeField] private string effect;
    public int RolledNumber {get; set;}
    private Rigidbody rb;
    private bool hasLanded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasLanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO check to see if die is moving. If it stops moving or is barely moving at all, return number that faces upward.
        if (rb.velocity.magnitude <= 0 && !hasLanded)
        {
            hasLanded = true;
            EffectManager.instance.ProcessEffect(effect, RolledNumber);
        }
    }
}
