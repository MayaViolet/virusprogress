using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeBackgroundColor : MonoBehaviour {

	public GameModel gameModel;

	// Use this for initialization
	void Start () {
		gameModel.OnEraTransition += OnEraTransition;
	}

	void OnEraTransition (GameModel.CurrentEra era)
	{
		if (era == GameModel.CurrentEra.Sixteenbit) {
			GetComponent<Image> ().color = gameModel.sixteenBitEraBackgroundColor;
		}
	}
	

}
