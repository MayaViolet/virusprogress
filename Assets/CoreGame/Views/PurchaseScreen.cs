using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PurchaseScreen : MonoBehaviour {

	public GameModel gameModel;
	public GameObject uiRoot;
	public GameObject skillList;
	public GameObject skillButtonPrefab;

	public GameObject popupRoot;
	public Text popupTitle;
	public Image popupImage;
	public Text popupExtraText;

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

			if (gameModel.HasPurchased(upgrade))
			{
				text.color = Color.white;
				continue;
			}
			if (!gameModel.CanPurchase(upgrade))
			{
				text.color = Color.red;
				continue;
			}

			var button = newEntry.GetComponentInChildren<Button>();
			var upgradeToBuy = upgrade;
			button.onClick.AddListener(() => {
				ShowResult(upgradeToBuy);
			});
		}
	}

	void OnUpgradePurchase(UpgradeData.Upgrade upgrade)
	{
	}

	public void Hide()
	{
		foreach (Transform child in skillList.transform)
		{
			Destroy(child.gameObject);
		}
		uiRoot.SetActive(false);
		popupRoot.SetActive(false);
	}

	void ShowResult(UpgradeData.Upgrade upgrade)
	{
		popupRoot.SetActive(true);
		popupTitle.text = upgrade.name;
		gameModel.PurchaseUpgrade(upgrade);
	}
}
