using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using DG;

public class EraChangeView : MonoBehaviour {

	public GameModel gameModel;
	public GameObject root;
	public Image back;
	public Text text;

	public string text16;
	public string text32;

	public AudioClip audio16;
	public AudioClip audio32;
	public AudioMixer mixer;

	bool final = false;

	void Start () {
		gameModel.OnEraTransition += OnEraChange;
		root.SetActive(true);
	}
	
	void DoEra(string text)
	{
		root.SetActive(true);
		this.text.text = text;
	}

	public void Close()
	{
		root.SetActive(false);
	}

	void Update()
	{
		var c = back.color;
		c.a = Random.Range(0.8f, 1f);
		back.color = c;
	}

	void OnEraChange(GameModel.CurrentEra newEra)
	{
		Debug.LogWarning("New era: "+newEra.ToString());
		if (newEra == GameModel.CurrentEra.Sixteenbit)
		{
			mixer.SetFloat("8bit", -80);
			mixer.SetFloat("16bit", 0);
			GetComponent<AudioSource>().PlayOneShot(audio16);
			DoEra(text16);
		}
		else if (newEra == GameModel.CurrentEra.ThirtyTwoBit)
		{
			mixer.SetFloat("8bit", -80);
			mixer.SetFloat("16bit", -80);
			mixer.SetFloat("32bit", 0);
			GetComponent<AudioSource>().PlayOneShot(audio32);
			DoEra(text32);
			final = true;
		}
	}
}
