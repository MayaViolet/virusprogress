using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameResources {

	public enum Type
	{
		Capacity,
		Awareness,
		Willpower
	};

	public int capacity;
	public int awareness;
	public int willPower;
	private Dictionary<Type, int> _contents = null;
	public Dictionary<Type, int> contents
	{
		get
		{
			if (_contents == null)
			{
				_contents = new Dictionary<Type, int>();
				if (capacity != 0)
				{
					_contents[Type.Capacity] = capacity;
				}
				if (awareness != 0)
				{
					_contents[Type.Awareness] = awareness;
				}
				if (willPower != 0)
				{
					_contents[Type.Willpower] = willPower;
				}
			}
			return _contents;
		}
	}

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

	public void Add(Type key, int quantity)
	{
		if (contents.ContainsKey(key))
		{
			contents[key] += quantity;
		}
		else
		{
			contents[key] = quantity;
		}
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

	public void Subtract(Type key, int quantity, bool allowNegative = true)
	{
		if (contents.ContainsKey(key))
		{
			contents[key] -= quantity;
		}
		else
		{
			contents[key] = quantity;
		}
		if (!allowNegative && contents[key] < 0)
		{
			contents[key] = 0;
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
