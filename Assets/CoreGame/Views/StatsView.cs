using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatsView : MonoBehaviour {

	public GameModel gameModel;
	public GameObject statEntryPrefab;

	Dictionary<string, GameObject> statEntries = new Dictionary<string, GameObject>();

	void Start () {
		ResourceChangeHandler(gameModel.resources);
		gameModel.OnResourceChange += ResourceChangeHandler;
	}

	void ResourceChangeHandler(GameResources newValues)
	{
		foreach (var key in newValues.contents.Keys)
		{
			if (!statEntries.ContainsKey(key.ToString()))
			{
				var entry = Instantiate<GameObject>(statEntryPrefab);
				statEntries[key.ToString()] = entry;
				entry.transform.SetParent(transform);
				var text = entry.GetComponentInChildren<Text>();
				text.text = key.ToString();
			}
			SetValue(key.ToString(), newValues.contents[key]);
		}
	}

	void SetValue(string key, int value)
	{
		if (!statEntries.ContainsKey(key))
		{
			return;
		}
		var texts = statEntries[key].GetComponentsInChildren<Text>();
		foreach (var text in texts)
		{
			if (text.name == "Amount")
			{
				text.text = value.ToString();
			}
		}
	}
}
