using UnityEngine;
using System.Collections;

public class UpgradeData : ScriptableObject {

	[System.Serializable]



	public class Upgrade
	{
		public string name;
		public GameResources cost;

	}

	public Upgrade[] data;
}