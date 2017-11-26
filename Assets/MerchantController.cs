using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MerchantController : MonoBehaviour
{
    public GameObject shopCanvas;
    public Movement player;
    public GameObject minimap;
    public Text currency;

	void Start() {
		var curScene = SceneManager.GetActiveScene ().name;
		// Assign points to the user.
		if (CarryOverState.levelSelected) {
			CarryOverState.points = 100;
			CarryOverState.pointsAtStartOfLevel = 100;
		}
		// Restart level.
		else if (curScene == CarryOverState.currentScene) {
			CarryOverState.points = CarryOverState.pointsAtStartOfLevel;
		} else {
			CarryOverState.pointsAtStartOfLevel = CarryOverState.points;
		}
		CarryOverState.currentScene = curScene;
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
			CarryOverState.points = currentCurrency - value;

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


