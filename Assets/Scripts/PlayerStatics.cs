using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatics : MonoBehaviour
{
    public static PlayerStatics instance;
    public bool IsInMenu {get; set;}

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
