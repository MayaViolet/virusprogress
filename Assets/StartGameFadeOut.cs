using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class StartGameFadeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Image> ().DOFade (0.0f, 2).OnComplete (KillThis);
	}

	void KillThis(){
		Destroy (gameObject);
	}

}
