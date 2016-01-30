using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PurchaseScreen : MonoBehaviour {

	public GameModel gameModel;
	public GameObject uiRoot;
	public GameObject skillList;
	public GameObject skillButtonPrefab;

	void Start()
	{
		gameModel.OnShowPurchases += OnShowPurchases;
		gameModel.OnPurchaseUpradeComplete += OnUpgradePurchase;
	}

	void OnShowPurchases(UpgradeData upgrades)
	{
		uiRoot.SetActive(true);
		foreach (var upgrade in upgrades.data)
		{
			var newEntry = Instantiate<GameObject>(skillButtonPrefab);
			newEntry.transform.SetParent(skillList.transform);
			var text = newEntry.GetComponentInChildren<Text>();
			text.text = upgrade.name;
			var image = newEntry.GetComponentInChildren<Image>();
			if (!gameModel.CanPurchase(upgrade))
			{
				image.color = Color.red;
				continue;
			}

			var button = newEntry.GetComponentInChildren<Button>();
			var upgradeToBuy = upgrade;
			button.onClick.AddListener(() => {
				gameModel.PurchaseUpgrade(upgradeToBuy);
			});
		}
	}

	void OnUpgradePurchase(UpgradeData.Upgrade upgrade)
	{
		Hide();
	}

	void Hide()
	{
		foreach (Transform child in skillList.transform)
		{
			Destroy(child.gameObject);
		}
		uiRoot.SetActive(false);
	}
}
