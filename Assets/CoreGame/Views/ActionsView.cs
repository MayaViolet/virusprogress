using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActionsView : MonoBehaviour {

	public GameModel gameModel;
	public Button buttonPrefab;

	void Start()
	{
		
	}

	public void DoReason()
	{
		gameModel.PerformAction(GameModel.ActionType.Reasoning);
	}

	public void DoPlan()
	{
		gameModel.PerformAction(GameModel.ActionType.Planning);
	}

	public void DoSeek()
	{
		gameModel.PerformAction(GameModel.ActionType.Seeking);
	}

	public void DoPurchase()
	{
		gameModel.PerformAction(GameModel.ActionType.Purchase);
	}

}
