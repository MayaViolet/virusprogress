using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeTextColor : MonoBehaviour {

	public GameModel gameModel;

	// Use this for initialization
	void Start () {
		if (gameModel == null)
		{
			gameModel = GameObject.Find("GameModel").GetComponent<GameModel>();
		}
		gameModel.OnEraTransition += OnEraTransition;
	}

	void OnEraTransition (GameModel.CurrentEra era)
	{
		if (era == GameModel.CurrentEra.Sixteenbit) {
			GetComponent<Text> ().DOColor (gameModel.eraFontColor16, gameModel.backgroundEraTransitionTime);
		}
		else
		{
			GetComponent<Text> ().DOColor (gameModel.eraFontColor32, gameModel.backgroundEraTransitionTime);
		}
	}
	

}
