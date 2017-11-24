using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MonoBehaviour
{
    public GameObject shopCanvas;
    public Movement player;
    public GameObject minimap;
    public Text currency;

	void Start() {
		OpenWeaponShop ();
	}

//    void OnCollisionEnter2D(Collision2D col)
//    {
//        if (col.gameObject.tag == "Bullet") return;
//        if (col.gameObject.tag == "Player") OpenWeaponShop();
//    }

    public void PurchaseMinimap()
    {
        if (!player.purchasedMinimap && PurchaseItem(50))
            player.purchasedMinimap = true;          
    }

    public bool PurchaseItem(int value)
    {
        var currentCurrency = System.Int32.Parse(currency.text);
        if(currentCurrency >= value)
        {
            currency.text = (currentCurrency - value).ToString();
			UIManager.points = currentCurrency - value;

			return true;
        }
        return false;

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


