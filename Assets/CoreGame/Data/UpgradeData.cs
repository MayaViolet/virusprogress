using UnityEngine;
using System.Collections;

public class UpgradeData : ScriptableObject {

	[System.Serializable]



	public class Upgrade
	{
		public string name;
		public GameResources cost;
		public Sprite sprite;
		public GameModel.ActionType actionToBenefit;
		public int actionBenefitPercent = 0;
		public int timeBenefitEffect = 0;
	}

	public Upgrade[] data;
}