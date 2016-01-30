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

	public void Add(GameResources toAdd)
	{
		foreach (var key in toAdd.contents.Keys)
		{
			if (contents.ContainsKey(key))
			{
				contents[key] += toAdd.contents[key];
			}
			else
			{
				contents[key] = toAdd.contents[key];
			}
		}
	}

	public void Subtract(GameResources toTake, bool allowNegative = true)
	{
		foreach (var key in toTake.contents.Keys)
		{
			if (contents.ContainsKey(key))
			{
				contents[key] -= toTake.contents[key];
			}
			else
			{
				contents[key] = -toTake.contents[key];
			}
			if (!allowNegative && contents[key] < 0)
			{
				contents[key] = 0;
			}
		}
	}

	public static string[] GetNames()
	{
		return System.Enum.GetNames(typeof(Type));
	}
}
