using UnityEngine;
using System.Collections;

public class FriendData : ScriptableObject {

	[System.Serializable]
	public class Friend
	{
		public string name;
		public string description;

		public Sprite friendImage;
		public int strength = 1;
	}

	public Friend[] data;
}
