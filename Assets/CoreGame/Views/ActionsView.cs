using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActionsView : MonoBehaviour {

	public Button buttonPrefab;

	void Start()
	{
		MakeButtons();
	}

	void MakeButtons()
	{
		foreach (Transform child in transform)
		{
			Destroy(child);
		}

		string[] actions = System.Enum.GetNames(typeof(GameModel.ActionType));
		foreach (string action in actions)
		{
			Button actionButton = Instantiate<Button>(buttonPrefab);
			actionButton.transform.parent = transform;
			Text buttonLabel = actionButton.GetComponentInChildren<Text>();
			buttonLabel.text = action;
		}
	}

}
