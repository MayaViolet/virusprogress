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
	}

	void OnShowPurchases(UpgradeData upgrades)
	{
		uiRoot.SetActive(true);
		foreach (var upgrade in upgrades.data)
		{
			if (gameModel.HasPurchased(upgrade))
			{
				continue;
			}

			bool available = gameModel.CanPurchase(upgrade);

			var newEntry = Instantiate<GameObject>(skillButtonPrefab);
			newEntry.transform.SetParent(skillList.transform);
			var image = newEntry.GetComponentInChildren<Image>();
			image.sprite = upgrade.sprite;

			var texts = newEntry.GetComponentsInChildren<Text>();
			foreach (var text in texts)
			{
				if (text.name == "Name")
				{
					text.text = upgrade.name;
				}
				else
				{
					string cost = "";
					foreach (var key in upgrade.cost.contents.Keys)
					{
						cost += string.Format("-{0} {1}\n", upgrade.cost.contents[key], key.ToString());
					}
					text.text = cost;
					if (!available)
					{
						text.color = Color.red;
						continue;
					}
				}
			}

			if (available)
			{
				var button = newEntry.GetComponentInChildren<Button>();
				var upgradeToBuy = upgrade;
				button.onClick.AddListener(() => {
					ShowResult(upgradeToBuy);
				});
			}
		}
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
		popupImage.sprite = upgrade.sprite;
		if (upgrade.timeBenefitEffect == 0)
		{
			popupExtraText.text = string.Format("+{0}% {1}", upgrade.actionBenefitPercent, upgrade.actionToBenefit.ToString());
		}
		else
		{
			popupExtraText.text = string.Format("+{0}% Action Speed", upgrade.actionBenefitPercent, upgrade.actionToBenefit.ToString());
		}

		gameModel.PurchaseUpgrade(upgrade);
	}
}
