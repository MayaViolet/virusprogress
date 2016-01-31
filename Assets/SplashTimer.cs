using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SplashTimer : MonoBehaviour {




	// Use this for initialization
	void Start () {
		GetComponent<Image> ().DOFade (1.0f, 2).OnComplete (PlayFadeOut);
	}
	
	void PlayFadeOut(){
		GetComponent<Image> ().DOFade (0.0f, 2).OnComplete(loadMenu);
	}

	void loadMenu(){
	Application.LoadLevel ("Start");
	}
}
