using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FriendAcquireView : MonoBehaviour {

	public GameModel gameModel;
	public GameObject uiRoot;
	public GameObject friendsRoot;
	public GameObject friendSelectorPrefab;

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
			var text = newElement.GetComponentInChildren<Text>();
			text.text = friend.name;
			Button friendButton = newElement.GetComponentInChildren<Button>();

			friendButton.onClick.AddListener(() => {
				gameModel.AddFriend(friend);
				Hide();
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
	}
}
