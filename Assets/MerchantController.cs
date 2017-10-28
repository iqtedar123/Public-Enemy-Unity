﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    public GameObject shopCanvas;
    public Movement player;
    public GameObject minimap;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet") return;
        if (col.gameObject.tag == "Player") OpenWeaponShop();
    }

    public void PurchaseMinimap()
    {
        player.purchasedMinimap = true;
    }

    void OpenWeaponShop()
    {
        shopCanvas.SetActive(true);
        Time.timeScale = 0;
        if (player.purchasedMinimap) minimap.SetActive(false);
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
        Time.timeScale = 1;
        if (player.purchasedMinimap) minimap.SetActive(true);
    }
}

