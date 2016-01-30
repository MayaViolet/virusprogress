using UnityEngine;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {
	
	private const int numberOfFriendsReturned = 2;

	public FriendData friendData;
	public UpgradeData upgradeData;

	private GameResources currentResources = new GameResources();

	public enum ActionType
	{
		Reasoning,
		Planning,
		Seeking
	};

	public delegate void ActionCompleteCallback(ActionType completedAction);

	public event ActionCompleteCallback OnActionComplete;

	public void PerformAction(ActionType actionToPerform)
	{
		switch (actionToPerform)
		{
		case ActionType.Planning:
			AddResource(GameResources.Type.Awareness, 8);
			break;
		case ActionType.Reasoning:
			AddResource(GameResources.Type.Willpower, 8);
			break;
		case ActionType.Seeking:
			SearchForFriends();
			break;
		default:
			break;
		}
		//Do stuff
		if (OnActionComplete != null)
		{
			OnActionComplete(actionToPerform);
		}
	}

	public delegate void FriendsFoundCallback(List<FriendData.Friend> foundFriends);

	public event FriendsFoundCallback OnFriendsFound;

	public void SearchForFriends(){
		List<FriendData.Friend> localFriendList = new List<FriendData.Friend>();
		if (friendData != null && friendData.data.Length > 0) {
			for (int index = 0; index < numberOfFriendsReturned; index++) {
				int countOfFriends = friendData.data.Length;
				int friendId = Random.Range (0, countOfFriends);
				localFriendList.Add (friendData.data [friendId]);
			}

			if (OnFriendsFound != null)
			{
				OnFriendsFound (localFriendList);
			}
		} else {
			//Developer screwed up exit
			Debug.LogError("Error: List of Friends is not populated");
		}
	}
		

	//Needs to return either the upgrade or a reason why it can't be 
	public delegate void PurchaseUpgradeCallback(UpgradeData.Upgrade upgrade);

	public event PurchaseUpgradeCallback OnPurchaseUpradeComplete;

	public void PurchaseUpgrade(UpgradeData.Upgrade upgrade){
		if (CanPurchase(upgrade))
		{
			SubtractResources(upgrade.cost);
			OnPurchaseUpradeComplete (upgrade);
		}
	}

	//can purchase (onresource change)
	public delegate void ResourceChangeCallBack(GameResources newValues);

	public event ResourceChangeCallBack OnResourceChange;


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

	public void AddResource(GameResources.Type key, int amount)
	{
		currentResources.Add(key, amount);
		OnResourceChange(resources);
	}

	public void TakeResource(GameResources.Type key, int amount)
	{
		currentResources.Subtract(key, amount);
		OnResourceChange(resources);
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

	void Start()
	{
		AddResource(GameResources.Type.Capacity, 8);
	}
}
