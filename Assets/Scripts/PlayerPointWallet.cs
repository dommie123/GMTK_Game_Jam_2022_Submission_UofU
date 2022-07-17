using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointWallet : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerPointWallet instance;
    public int Points {get; set;}
    void Awake()
    {
        instance = this;
    }
}
