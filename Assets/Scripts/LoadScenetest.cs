using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScenetest : MonoBehaviour {

    // Use this for initialization
    void Start() {
	
    }
	
    // Update is called once per frame
    void Update() {
	
    }

    void OnGUI() {
        if (Event.current.Equals(Event.KeyboardEvent("l"))) {
            SceneManager.LoadScene(1);
        }
    }
}
