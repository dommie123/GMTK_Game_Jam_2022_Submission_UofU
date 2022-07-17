using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatics : MonoBehaviour
{
    public static EnemyStatics instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
