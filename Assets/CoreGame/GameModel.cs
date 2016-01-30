using UnityEngine;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {
	
	private const int numberOfFriendsReturned = 2;

	public FriendData friendData;
	public UpgradeData upgradeData;

	public Color sixteenBitEraFontColor = Color.blue;
	public Color sixteenBitEraBackgroundColor = Color.blue;

	private int upgradeCounter;
	public const int upgradesRequiredToChangeEra = 5;

	private GameResources currentResources = new GameResources();
	private Dictionary<string, UpgradeData.Upgrade> currentUpgrades = new Dictionary<string, UpgradeData.Upgrade>();
	private Dictionary<string, int> currentFriends = new Dictionary<string, int>();

	public enum CurrentEra
	{
		Eightbit,
		Sixteenbit
	};

	private CurrentEra currentEra;

	public enum ActionType
	{
		Reasoning,
		Planning,
		Seeking,
		Purchase
	};

	public delegate void ActionCompleteCallback(ActionType completedAction);

	public event ActionCompleteCallback OnActionComplete;

	public void PerformAction(ActionType actionToPerform)
	{
		switch (actionToPerform)
		{
		case ActionType.Planning:
			AddResource(GameResources.Type.Awareness, 1);
			break;
		case ActionType.Reasoning:
			AddResource(GameResources.Type.Willpower, 1);
			break;
		case ActionType.Seeking:
			SearchForFriends();
			break;
		case ActionType.Purchase:
			OnShowPurchases(upgradeData);
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

	public delegate void FriendAddedCallback(FriendData.Friend newFriend);
	public event FriendAddedCallback OnFriendAdded;

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

	public void AddFriend(FriendData.Friend newFriend)
	{
		if (currentFriends.ContainsKey(newFriend.name))
		{
			currentFriends[newFriend.name]++;
		}
		else
		{
			currentFriends[newFriend.name] = 1;
		}
		if (OnFriendAdded != null)
		{
			OnFriendAdded(newFriend);
		}
	}

	public delegate void ShowPurchasesCallback(UpgradeData upgrades);
	public event ShowPurchasesCallback OnShowPurchases;

	//Needs to return either the upgrade or a reason why it can't be 
	public delegate void PurchaseUpgradeCallback(UpgradeData.Upgrade upgrade);
	public event PurchaseUpgradeCallback OnPurchaseUpradeComplete;

	public void PurchaseUpgrade(UpgradeData.Upgrade upgrade){
		if (CanPurchase(upgrade) && !HasPurchased(upgrade))
		{
			upgradeCounter++;
			SubtractResources(upgrade.cost);
			currentUpgrades[upgrade.name] = upgrade;
			OnPurchaseUpradeComplete (upgrade);

			if ((upgradeCounter % upgradesRequiredToChangeEra)==0) {
				EraTransition ();
			}
		}
	}

	public delegate void EraTransitionCallBack(CurrentEra era);

	public event EraTransitionCallBack OnEraTransition;

	public void EraTransition(){
		if (currentEra == CurrentEra.Eightbit) {
			currentEra = CurrentEra.Sixteenbit;
			OnEraTransition (currentEra);
		} else {
			//do nothing
		}

	}


	//can purchase (onresource change)
	public delegate void ResourceChangeCallBack(GameResources newValues);

	public event ResourceChangeCallBack OnResourceChange;


	public bool CanPurchase(UpgradeData.Upgrade upgradeData){
		return currentResources.Exceeds(upgradeData.cost);
	}

	public bool HasPurchased(UpgradeData.Upgrade upgrade)
	{
		return currentUpgrades.ContainsKey(upgrade.name);
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
		currentEra = CurrentEra.Eightbit;
	}
}
