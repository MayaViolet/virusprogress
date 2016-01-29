using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {

	public enum ActionType
	{
		Planning
	};

	public delegate void ActionCompleteCallback(ActionType completedAction);

	public event ActionCompleteCallback OnActionComplete;

	public void PerformAction(ActionType actionToPerform)
	{
		//Do stuff
		if (OnActionComplete != null)
		{
			OnActionComplete(actionToPerform);
		}
	}
}
