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

			var newEntry = Instantiate<GameObject>(skillButtonPrefab);
			newEntry.transform.SetParent(skillList.transform);
			var text = newEntry.GetComponentInChildren<Text>();
			text.text = upgrade.name;
			var image = newEntry.GetComponentInChildren<Image>();
			image.sprite = upgrade.sprite;

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
