using UnityEngine;
using System.Collections.Generic;

public class GameResources {

	public enum Type
	{
		Capacity,
		Awareness,
		Willpower
	};

	public Dictionary<Type, int> contents = new Dictionary<Type, int>();

	public bool Exceeds(GameResources b)
	{
		foreach (var key in b.contents.Keys)
		{
			if (contents.ContainsKey(key))
			{
				if (contents[key] < b.contents[key])
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		return true;
	}

	public static string[] GetNames()
	{
		return System.Enum.GetNames(typeof(Type));
	}
}
