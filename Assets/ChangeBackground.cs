using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeBackground : MonoBehaviour {


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
		var im = GetComponent<Image>();
		if (im == null)
		{
			return;
		}
		if (era == GameModel.CurrentEra.Sixteenbit) {
			GetComponent<Image>().sprite = gameModel.sixteenBitBoarderImage;
		}
		else
		{
			GetComponent<Image> ().DOColor (gameModel.eraBackgroundColor32, gameModel.backgroundEraTransitionTime);
		}
	}
}
