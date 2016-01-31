using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


public class ChangeBackgroundColor : MonoBehaviour {

	public GameModel gameModel;

	// Use this for initialization
	void Start () {
		if (gameModel == null)
		{
			gameModel = GameObject.Find("GameModel").GetComponent<GameModel>();
		}
		gameModel.OnEraTransition += OnEraTransition;
	}

	void OnDisable()
	{
		gameModel.OnEraTransition -= OnEraTransition;
	}

	void OnEraTransition (GameModel.CurrentEra era)
	{
		if (era == GameModel.CurrentEra.Sixteenbit) {
			//if (gameModel) {
				GetComponent<Image> ().DOColor (gameModel.eraBackgroundColor16, gameModel.backgroundEraTransitionTime);
			//}
		}
		else
		{
			GetComponent<Image> ().DOColor (gameModel.eraBackgroundColor32, gameModel.backgroundEraTransitionTime);
		}
	}
	

}
