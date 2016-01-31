using UnityEngine;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {
	
	private const int numberOfFriendsReturned = 3;

	public FriendData friendData;
	public UpgradeData upgradeData;

	public float backgroundEraTransitionTime = 0.2f;
	public float gameStartButtonTransitionTime = 0.2f;

	public Color eraFontColor16 = Color.blue;
	public Color eraBackgroundColor16 = Color.blue;
	public Color eraFontColor32 = Color.red;
	public Color eraBackgroundColor32 = Color.red;

	public Sprite sixteenBitBoarderImage;

	private int upgradeCounter;
	public int upgradesRequiredToChangeEra
	{
		get
		{
			return upgradeData.data.Length/2;
		}
	}

	private GameResources currentResources = new GameResources();
	private Dictionary<string, UpgradeData.Upgrade> currentUpgrades = new Dictionary<string, UpgradeData.Upgrade>();
	private Dictionary<string, int> currentFriends = new Dictionary<string, int>();

	public enum CurrentEra
	{
		Eightbit,
		Sixteenbit,
		ThirtyTwoBit
	};

	private CurrentEra currentEra;

	public enum ActionType
	{
		Reasoning,
		Planning,
		Seeking,
		Purchase
	};

	public Color CurrentBackgroundColor(Color defaultC)
	{
		if (currentEra == CurrentEra.Eightbit) {
			return defaultC;
		} else if (currentEra == CurrentEra.Sixteenbit) {
			return eraBackgroundColor16;
		} else if (currentEra == CurrentEra.ThirtyTwoBit) {
			return eraBackgroundColor32;
		}
		return defaultC;
	}

	public Color CurrentFontColor(Color defaultC)
	{
		if (currentEra == CurrentEra.Eightbit) {
			return defaultC;
		} else if (currentEra == CurrentEra.Sixteenbit) {
			return eraFontColor16;
		} else if (currentEra == CurrentEra.ThirtyTwoBit) {
			return eraFontColor32;
		}
		return defaultC;
	}
		

	public delegate void ActionCompleteCallback(ActionType completedAction);

	public event ActionCompleteCallback OnActionComplete;

	public void PerformAction(ActionType actionToPerform)
	{
		int capacityFactor = currentResources.contents[GameResources.Type.Capacity];
		int upgradeBenefit = 100;
		foreach (var upgrade in currentUpgrades.Values)
		{
			if (upgrade.actionToBenefit == actionToPerform)
			{
				upgradeBenefit += upgrade.actionBenefitPercent;
			}
		}
		capacityFactor = (capacityFactor * upgradeBenefit) / 100;
		capacityFactor = capacityFactor / 4 + 1;
		switch (actionToPerform)
		{
		case ActionType.Planning:
			AddResource(GameResources.Type.Awareness, capacityFactor);
			break;
		case ActionType.Reasoning:
				AddResource(GameResources.Type.Willpower, capacityFactor);
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

			List<FriendData.Friend> viableFriends = new List<FriendData.Friend>();
			foreach (var friend in friendData.data)
			{
				FriendData.Friend copy = friend.Copy();
				copy.Randomise();
				int chance = GetFriendChance(copy);
				if (chance > 5)
				{
					viableFriends.Add(copy);
				}
			}

			int countOfFriends = viableFriends.Count;

			for (int index = 0; index < numberOfFriendsReturned; index++) {
				int friendId = Random.Range (0, countOfFriends);
				localFriendList.Add (viableFriends[friendId]);
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

	public int GetFriendChance(FriendData.Friend friend)
	{
		int min = friend.strength / 2;
		int max = friend.strength * 2;
		int capacity = currentResources.contents[GameResources.Type.Capacity];
		int chance = (capacity - min) * 100 / (max - min);

		print(string.Format("Chance for {0}: {1} vs {2} chance: {3}%", friend.name, capacity, friend.strength, chance));

		return Mathf.Clamp(chance, 0, 100);
	}

	public bool AttemptFriendTakeover(FriendData.Friend friend)
	{
		int chance = GetFriendChance(friend);
		int roll = Random.Range(0, 100);
		print(string.Format("Trying to take {0}: roll {1} vs {2}", friend.name, roll, chance));
		return roll < chance;
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
		AddResource(GameResources.Type.Capacity, newFriend.strength);
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
			if (OnPurchaseUpradeComplete != null)
			{
				OnPurchaseUpradeComplete (upgrade);
			}

			if (currentEra == CurrentEra.Eightbit && upgradeCounter >= upgradesRequiredToChangeEra)
			{
				EraTransition ();
			}

			//Check for all purchased
			bool complete = true;
			foreach (var entry in upgradeData.data)
			{
				if (!HasPurchased(entry))
				{
					complete = false;
					break;
				}
			}
			if (complete)
			{
				EraTransition();
			}
		}
	}

	public delegate void EraTransitionCallBack(CurrentEra era);

	public event EraTransitionCallBack OnEraTransition;

	public void EraTransition(){
		if (currentEra == CurrentEra.Eightbit) {
			currentEra = CurrentEra.Sixteenbit;
			OnEraTransition (currentEra);
		} else if (currentEra == CurrentEra.Sixteenbit) {
			currentEra = CurrentEra.ThirtyTwoBit;
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
		AddResource(GameResources.Type.Capacity, 1);
		currentEra = CurrentEra.Eightbit;

	}
}
