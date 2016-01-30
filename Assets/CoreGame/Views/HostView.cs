using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HostView : MonoBehaviour {

	public GameModel gameModel;
	public Transform hostsRoot;
	public Image hostSpritePrefab;

	void Start () {
		gameModel.OnFriendAdded += OnFriendAdded;
	}
	
	void OnFriendAdded(FriendData.Friend newFriend)
	{
		var newSprite = Instantiate<Image>(hostSpritePrefab);
		newSprite.rectTransform.SetParent(hostsRoot);
		newSprite.sprite = newFriend.friendImage;
		newSprite.rectTransform.localScale = Vector3.one * 0.3f;
		newSprite.rectTransform.localPosition = (Vector3)Random.insideUnitCircle * 50;
	}
}
