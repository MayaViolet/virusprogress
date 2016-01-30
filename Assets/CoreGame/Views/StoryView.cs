using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryView : MonoBehaviour {

	public Text textPrefab;
	public RectTransform content;
	public int lineHeight = 18;

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
}
