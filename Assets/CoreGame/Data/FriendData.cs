using UnityEngine;
using System.Collections;

public class FriendData : ScriptableObject {

	[System.Serializable]
	public class Friend
	{
		public string name;
	}

	public Friend[] data;
}
