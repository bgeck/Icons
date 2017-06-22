using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsBlendController : MonoBehaviour {

    Animator iconAnim;

    // Use this for initialization
    void Start() {
        iconAnim = GetComponent<Animator>();

        // Call animate function every 7 - 30 seconds or so.
        //  function
        //  time until first trigger (seconds)
        //  time between each repetition (seconds)
        InvokeRepeating("Animate", randomAnim(16.0f, 30.0f), randomAnim(16.0f, 30.0f));

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
        float flagStep = 0.0f;
        float RandomChoice = randomAnim(0.0f, 1.5f);
        print(RandomChoice);

        if (RandomChoice > 1.0f) {
            iconAnim.SetBool("Trigger", true);
        }
        else if (iconAnim.GetBool("Trigger") == false) {
            // Animate a random number of times.
            for (int f = 0; f < randomAnim(5.0f, 25.0f); f++) {
                float flagStepGoal = randomAnim(0.3f, 1.0f);

                // Move flagStep to flagStepGoal number gently
                if (flagStep < flagStepGoal) {
                    for (float i = flagStep; i < flagStepGoal; i = i + 0.1f) {
                        flagStep = i;
                        iconAnim.SetFloat("Blend", flagStep);

                        // Wait for 30 - 60 milliseconds between animation steps (30 fps - 60 fps)
                        yield return new WaitForSeconds(randomAnim(0.03f, 0.06f));
                    }
                }
                else if (flagStep >= flagStepGoal) {
                    for (float i = flagStep; i > flagStepGoal; i = i - 0.1f) {
                        flagStep = i;
                        iconAnim.SetFloat("Blend", flagStep);

                        // Wait for 30 - 60 milliseconds between animation steps (30 fps - 60 fps)
                        yield return new WaitForSeconds(randomAnim(0.03f, 0.06f));
                    }
                }

                // Then move back to 0 gently
                if (flagStep > 0.0f) {
                    for (float i = flagStep; i > 0.0f; i = i - 0.1f) {
                        flagStep = i;
                        iconAnim.SetFloat("Blend", flagStep);

                        // Wait for 30 - 60 milliseconds between animation steps (60 fps - 75 fps)
                        yield return new WaitForSeconds(randomAnim(0.06f, 0.075f));
                    }
                }
            }
        }

        // Wait for 1 second so animation step triggers
        yield return new WaitForSeconds(1.0f);

        // Reset Triggers
        iconAnim.SetFloat("Blend", 0.0f);
        iconAnim.SetBool("Trigger", false);
    }
}
