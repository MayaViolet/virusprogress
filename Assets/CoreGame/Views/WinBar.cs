using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinBar : MonoBehaviour {

	public GameModel gameModel;
	public GameObject root;
	public Text text;

	// Use this for initialization
	void Start () {
		gameModel.OnEraTransition += OnEraChange;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format("{0}/{1} TO WIN", gameModel.friendCount, 40);
		if (gameModel.friendCount >= 40)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("End");
		}
	}

	void OnEraChange(GameModel.CurrentEra era)
	{
		if (era == GameModel.CurrentEra.ThirtyTwoBit)
		{
			root.SetActive(true);
		}
	}
}
