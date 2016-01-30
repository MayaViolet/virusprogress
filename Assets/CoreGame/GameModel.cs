using UnityEngine;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {

	public FriendData friendList;
	private int numberOfFriendsReturned = 2;


	private GameResources currentResources;


	public enum ActionType
	{
		Planning
	};

	public delegate void ActionCompleteCallback(ActionType completedAction);

	public static event ActionCompleteCallback OnActionComplete;

	public void PerformAction(ActionType actionToPerform)
	{
		//Do stuff
		if (OnActionComplete != null)
		{
			OnActionComplete(actionToPerform);
		}
	}
		

	public delegate List<FriendData.Friend> FriendsFoundCallback(List<FriendData.Friend> foundFriends);

	public static event FriendsFoundCallback OnFriendsFound;

	public void SearchForFriends(){
		List<FriendData.Friend> localFriendList = new List<FriendData.Friend>();
		if (!friendList == null) {
			for (int index = 0; index < numberOfFriendsReturned; index++) {
				int countOfFriends = friendList.data.Length;
				int friendId = Random.Range (1, countOfFriends);
				localFriendList.Add (friendList.data [friendId]);
			}

			OnFriendsFound (localFriendList);
		} else {
			//Developer screwed up exit
			Debug.LogError("Error: List of Friends is not populated");
		}
	}
		

	//Needs to return either the upgrade or a reason why it can't be 
	public delegate void PurchaseUpgradeCallback(UpgradeData.Upgrade upgrade);

	public static event PurchaseUpgradeCallback OnPurchaseUpradeComplete;

	public void PurchaseUpgrade(UpgradeData.Upgrade upgrade){
		if (CanPurchase(upgrade))
		{
			SubtractResources(upgrade.cost);
			OnPurchaseUpradeComplete (upgrade);
		}
	}

	//can purchase (onresource change)
	public delegate void ResourceChangeCallBack(GameResources newValues);

	public static event ResourceChangeCallBack OnResourceChange;


	public bool CanPurchase(UpgradeData.Upgrade upgradeData){
		return currentResources.Exceeds(upgradeData.cost);
	}

	public GameResources resources
	{
		get
		{
			return currentResources;
		}
	}

	public void AddResources(GameResources toAdd)
	{
		currentResources.Add(toAdd);
		OnResourceChange(resources);
	}

	public void SubtractResources(GameResources toTake)
	{
		currentResources.Subtract(toTake, false);
		OnResourceChange(resources);
	}
}
