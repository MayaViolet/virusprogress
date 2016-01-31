using UnityEngine;
using System.Collections;

public class VirusView : MonoBehaviour {

	public GameModel gameModel;
	public GameObject virusPrefab;

	// Use this for initialization
	void Start () {
		gameModel.OnPurchaseUpradeComplete += OnUpgradPurchase;
	}

	void OnUpgradPurchase(UpgradeData.Upgrade upgrade)
	{
		if (upgrade == null)
		{
			return;
		}
		var virus = Instantiate<GameObject>(virusPrefab);
		virus.transform.SetParent(transform);
		virus.transform.localPosition = new Vector2(0,0);
		virus.transform.Translate(Random.insideUnitCircle * 25);
	}
}
