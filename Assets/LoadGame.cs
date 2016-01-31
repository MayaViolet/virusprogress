using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(){
		Application.LoadLevel ("Game");
	}
}
