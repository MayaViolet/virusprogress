using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryView : MonoBehaviour {

	public GameModel gameModel;
	public Text textPrefab;
	public RectTransform content;
	public int lineHeight = 18;

	[Header("Story Snippets")]
	public string[] randomSnippets;
	public string[] sequenceSnippets;
	int lastSequence = 0;
	int actionsTillNextSnippet;

	void AddLine(string text)
	{
		Text newLine = Instantiate(textPrefab) as Text;
		newLine.text = text;
		newLine.rectTransform.SetParent(content);
		newLine.rectTransform.SetSiblingIndex(0);
		var size = content.sizeDelta;
		size.y += lineHeight;
		content.sizeDelta = size;
	}

	void Start()
	{
		gameModel.OnPurchaseUpradeComplete += OnPurchaseUpgrade;
		gameModel.OnActionComplete += OnActionComplete;
		actionsTillNextSnippet = 2;
	}

	void OnPurchaseUpgrade(UpgradeData.Upgrade upgrade)
	{
		if (lastSequence < sequenceSnippets.Length)
		{
			AddLine(sequenceSnippets[lastSequence++]);
		}
	}

	void OnActionComplete(GameModel.ActionType action)
	{
		if (randomSnippets.Length <= 0)
		{
			return;
		}

		actionsTillNextSnippet--;
		if (actionsTillNextSnippet <= 0)
		{
			actionsTillNextSnippet = Random.Range(2,6);
			AddLine(randomSnippets[Random.Range(0, randomSnippets.Length)]);
		}
	}
}
