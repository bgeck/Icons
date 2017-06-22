using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadSceneAsync("icon2", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync("icon3", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
