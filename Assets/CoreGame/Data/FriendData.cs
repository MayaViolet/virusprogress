using UnityEngine;
using System.Collections;

public class FriendData : ScriptableObject {

	[System.Serializable]
	public class Friend
	{
		public string name;
		public string[] possibleDescriptions;

		public Sprite friendImage;
		public int minStrength = 1;
		public int maxStrength = 1;

		private int descriptionIndex;
		private int currentStrength;

		public string description
		{
			get
			{
				if (possibleDescriptions.Length <= 0)
				{
					return "";
				}
				return possibleDescriptions[descriptionIndex];
			}
		}

		public int strength
		{
			get
			{
				return currentStrength;
			}
		}

		public void Randomise()
		{
			if (possibleDescriptions.Length <= 0)
			{
				descriptionIndex = 0;
			}
			else
			{
				descriptionIndex = Random.Range(0, possibleDescriptions.Length);
			}
			currentStrength = Random.Range(minStrength, maxStrength);
		}

		public Friend Copy()
		{
			Friend copy = new Friend();
			copy.name = name;
			copy.possibleDescriptions = possibleDescriptions;
			copy.friendImage = friendImage;
			copy.minStrength = minStrength;
			copy.maxStrength = maxStrength;
			copy.descriptionIndex = descriptionIndex;
			copy.currentStrength = currentStrength;
			return copy;
		}
	}

	public Friend[] data;
}
