using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsController : MonoBehaviour {

    Animator iconAnim;

    // Use this for initialization
    void Start() {
        iconAnim = GetComponent<Animator>();

        // Call animate function every 15 - 30 seconds or so.
        //  function
        //  time until first trigger (seconds)
        //  time between each repetition (seconds)
        InvokeRepeating("Animate", randomAnim(15.0f, 30.0f), randomAnim(15.0f, 30.0f));

    }

    // Random Numbers for how often to do animation
    float randomAnim(float min, float max) {
        return Mathf.Clamp(Random.value * max, min, max);
    }

    // Animate
    void Animate() {
        StartCoroutine(DoAnimate());
    }

    IEnumerator DoAnimate() {
        // Trigger Animation
        iconAnim.SetBool("trigger", true);

        // Wait for 1 second so animation step triggers
        yield return new WaitForSeconds(1.0f);

        // Reset Trigger
        iconAnim.SetBool("trigger", false);
    }
}
