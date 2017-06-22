using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lidController : MonoBehaviour {
    public GameObject Lids;

    void Start() {
        InvokeRepeating("Blink", randomBlink(0.0f, 0.25f), randomBlink(6.0f, 10.0f));
    }

    float randomBlink(float min, float max) {
        return Mathf.Clamp(Random.value * max, min, max);
    }

    void Blink() {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink() {	
        if (MainUserTracker.UserCount == 0) {
            Lids.SetActive(true);
        }
        else {
            Lids.SetActive(true);
            yield return new WaitForSeconds(randomBlink(0.1f, 0.3f));
            Lids.SetActive(false);
        }
    }
}