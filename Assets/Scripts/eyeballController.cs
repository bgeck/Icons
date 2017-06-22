using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeballController : MonoBehaviour {
    public GameObject Eyeballs;
    public GameObject background;
    public float xMin, xMax;
    public float yMin, yMax;
    public float eyeSpeed;
    private GameObject[] ClosestUser = new GameObject[MainUserTracker.MaxUsers];
    private float UserDistance;
    private float LowestDistance = 100000.0f;
    private Vector3 thisUser;
    private Vector3 lerpedUser;
    private Vector3 clampedPosition;
    private int i;

    private Animator maryAnimation1;
    private Animator maryAnimation2;
    private Animator icon2Animation;
    private Animator icon3Animation;

    void Start() {
        maryAnimation1 = GameObject.Find("/icon1/icon1Animation1").GetComponent<Animator>();
        maryAnimation2 = GameObject.Find("/icon1/icon1Animation2").GetComponent<Animator>();

        icon2Animation = GameObject.Find("/icon2/icon2Animations").GetComponent<Animator>();
        icon3Animation = GameObject.Find("/icon3/icon3Animation").GetComponent<Animator>();
    }

    void Update() {
        for (i = 0; i < MainUserTracker.UserObjects.Length; i += 1) {
            if (MainUserTracker.UserObjects[i]) {

                UserDistance = Vector3.Distance(
                    MainUserTracker.UserObjects[i].transform.position, 
                    Eyeballs.transform.position
                );

                if (LowestDistance > UserDistance) {
                    LowestDistance = UserDistance;
                    ClosestUser[i] = MainUserTracker.UserObjects[i];
                }

                lerpedUser = ClosestUser[i].transform.position;

                // Mary and Jesus
                if (
                    ClosestUser[i].transform.position.x > -1 &&
                    ClosestUser[i].transform.position.x < 1) {
                    maryAnimation1.SetBool("trigger", true);
//                    print(ClosestUser[i].transform.position.x);
                    lerpedUser.x = 0.0f;
                }
                else {
                    maryAnimation1.SetBool("trigger", false);
                }

                // Sassy
                if (
                    ClosestUser[i].transform.position.x > -12.5 &&
                    ClosestUser[i].transform.position.x < -8.5) {
//                    icon3Animation.SetBool("trigger", true);
//                    print(ClosestUser[i].transform.position.x);
                    lerpedUser.x = -10.7f;
                }
                else {
//                    icon3Animation.SetBool("trigger", false);
                }

                // Laughy
                if (
                    ClosestUser[i].transform.position.x > 8 &&
                    ClosestUser[i].transform.position.x < 12) {
//                    icon2Animation.SetBool("trigger", true);
//                    print(ClosestUser[i].transform.position.x);
                    lerpedUser.x = 10.0f;
                }
                else {
//                    icon2Animation.SetBool("trigger", false);
                }
            }
            else {
                LowestDistance = 100000.0f;
            }
        }

        float backgroundX = background.transform.position.x;
        float backgroundY = background.transform.position.y;
        clampedPosition.x = Mathf.Clamp(transform.position.x, backgroundX + xMin, backgroundX + xMax);
        clampedPosition.y = Mathf.Clamp(transform.position.y, backgroundY + yMin, backgroundY + yMax);
        transform.position = Vector3.MoveTowards(clampedPosition, lerpedUser, eyeSpeed);
    }

    void OnGUI() {
        for (i = 0; i < MainUserTracker.UserObjects.Length; i += 1) {
            if (MainUserTracker.UserObjects[i]) {
                if (Eyeballs.name == "maryEyeballs") {
                    if (!MainUserTracker.showUsers) {
                    }
                    else {
                        GUI.Label(new Rect(0, 100, 300, 100), 
                            ClosestUser[i].GetComponent<customNI>().UserId.ToString(), 
                            ClosestUser[i].GetComponent<customNI>().customStyle
                        );
                    }
                }
               
            }
        }
    }
}