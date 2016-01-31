using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void restart(){
		Application.LoadLevel ("Splash");
	}
}
