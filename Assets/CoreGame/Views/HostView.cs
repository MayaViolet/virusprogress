using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HostView : MonoBehaviour {

	public GameModel gameModel;
	public Image hostSprite;

	void Start () {
		gameModel.OnFriendAdded += OnFriendAdded;
	}
	
	void OnFriendAdded(FriendData.Friend newFriend)
	{
		hostSprite.sprite = newFriend.friendImage;
	}
}
