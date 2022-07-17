using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
        exitButton.onClick.AddListener(ExitShop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterShop()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerStatics.instance.IsInMenu = true;
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerStatics.instance.IsInMenu = false;
    }
}
