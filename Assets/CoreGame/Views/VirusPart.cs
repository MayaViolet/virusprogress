using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VirusPart : MonoBehaviour {

	RectTransform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<RectTransform>();
		trans.localEulerAngles = Vector3.forward * Random.value * 360;
	}
	
	// Update is called once per frame
	void Update () {
		trans.Rotate(Vector3.forward * Time.deltaTime * Random.value * 60);
		trans.localScale = Vector3.one * Random.Range(0.8f, 1);
	}
}
