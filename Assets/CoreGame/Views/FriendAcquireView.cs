using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FriendAcquireView : MonoBehaviour {

	public GameModel gameModel;
	public GameObject uiRoot;
	public GameObject friendsRoot;
	public GameObject friendSelectorPrefab;

	public GameObject popupRoot;
	public Text popupTitle;
	public Image popupImage;
	public Text popupExtraText;

	void Start()
	{
		gameModel.OnFriendsFound += OnFriendsFound;
	}

	void OnFriendsFound(List<FriendData.Friend> friends)
	{
		uiRoot.SetActive(true);
		foreach (var friend in friends)
		{
			var newElement = Instantiate<GameObject>(friendSelectorPrefab);
			newElement.transform.SetParent(friendsRoot.transform);

			Image friendSprite = newElement.GetComponentInChildren<Image>();
			friendSprite.sprite = friend.friendImage;

			int chance = gameModel.GetFriendChance(friend);
			var texts = newElement.GetComponentsInChildren<Text>();
			foreach (var text in texts)
			{
				if (text.name == "Name")
				{
					text.text = friend.name + "\n" + friend.description;
				}
				else
				{
					text.text = chance + "% chance";
				}
				text.color = gameModel.CurrentFontColor (text.color);
			}
			Button friendButton = newElement.GetComponentInChildren<Button>();

			FriendData.Friend thisFriend = friend;
			friendButton.onClick.AddListener(() => {
				AttempAcquisition(thisFriend);
			});
		}
	}

	public void Hide()
	{
		foreach (Transform child in friendsRoot.transform)
		{
			Destroy(child.gameObject);
		}
		uiRoot.SetActive(false);
		popupRoot.SetActive(false);
	}

	void AttempAcquisition(FriendData.Friend friend)
	{
		popupRoot.SetActive(true);
		popupImage.sprite = friend.friendImage;
		bool success = gameModel.AttemptFriendTakeover(friend);
		if (success)
		{
			gameModel.AddFriend(friend);
			popupTitle.text = "SUCCESS";
			popupExtraText.text = string.Format("+{0} CAPACITY", friend.strength);
		}
		else
		{
			popupTitle.text = "FAIL :(";
			popupExtraText.text = "TRY AGAIN";
		}
	}
}
