using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenNI;

public static class ExtensionMethods {
    public static float Remap(
        this float value, 
        float from1, 
        float to1, 
        float from2, 
        float to2
    ) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

public class MainUserTracker : MonoBehaviour {
    public static int MaxUsers = 3;
    public static int UserCount;
    public static int CurrentAdjustment;
    public static bool showUsers = true;

    public static float KMinX = -900;
    public static float KMaxX = 900;
    public static float GMinX = -14;
    public static float GMaxX = 10.5f;

    public static float KMinY = -300;
    public static float KMaxY = 90;
    public static float GMinY = -15;
    public static float GMaxY = -3;

    public static float KMinZ = -300;
    public static float KMaxZ = -3000;
    public static float GMinZ = 100;
    public static float GMaxZ = 200;

    public OpenNIUserTracker UserTracker;
    public Transform UserObject;
    public static GameObject[] UserObjects = new GameObject[MaxUsers];
    private bool[] UserObjectStatus = new bool [MaxUsers];
    private int i, j, h;

    void Start() {
        if (!UserTracker) {
            UserTracker = gameObject.AddComponent<OpenNIUserTracker>();
        }
    }

    void OnGUI() {
        for (h = 0; h < UserObjects.Length; h += 1) {
            if (UserObjects[h]) {
                Vector3 TextPosition = Camera.main.WorldToScreenPoint(UserObjects[h].transform.position);
                if (!MainUserTracker.showUsers) {
                }
                else {
                    GUI.Label(new Rect(
                            TextPosition.x, 
                            Screen.height - TextPosition.y, 
                            10, 
                            10
                        ), 
                        h.ToString(), 
                        UserObjects[h].GetComponent<customNI>().customStyle
                    );
                }
            }
            else {
            }
        }
    }

    void Update() {

        for (i = 0; i < UserObjectStatus.Length; i += 1) {
            if (UserTracker.AllUsers.Contains(i) && UserObjectStatus[i] != true) {
                UserObjectStatus[i] = true;
                Transform UserCircle = Instantiate(UserObject, new Vector3((-1 + i) * 10, 0, 0), Quaternion.identity);
                UserObjects[i] = UserCircle.gameObject;
                UserObjects[i].GetComponent<customNI>().UserId = i;
            }
            else if (UserTracker.AllUsers.Contains(i) && UserObjectStatus[i] == true) {
            }
            else {
                UserObjectStatus[i] = false;
                Destroy(UserObjects[i]);
            }
        }

        UserCount = UserTracker.AllUsers.Count;
        if (UserCount > MaxUsers) {
            UserCount = MaxUsers;
        }

        if (Input.GetButtonDown("Jump")) {
            if (!showUsers) {
                showUsers = true;
                for (j = 0; j < UserObjects.Length; j += 1) {
                    if (UserObjects[j]) {
                        UserObjects[j].transform.localScale = new Vector3(2, 2, 1);
                    }
                }
            }
            else {
                showUsers = false;
                for (j = 0; j < UserObjects.Length; j += 1) {
                    if (UserObjects[j]) {
                        UserObjects[j].transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }
        }
            
        KMinX = AdjustSelect("1", 1, KMinX);
        KMaxX = AdjustSelect("2", 2, KMaxX);
        GMinX = AdjustSelect("3", 3, GMinX);
        GMaxX = AdjustSelect("4", 4, GMaxX);

        KMinY = AdjustSelect("5", 5, KMinY);
        KMaxY = AdjustSelect("6", 6, KMaxY);
        GMinY = AdjustSelect("7", 7, GMinY);
        GMaxY = AdjustSelect("8", 8, GMaxY);

        KMinZ = AdjustSelect("9", 9, KMinZ);
        KMaxZ = AdjustSelect("0", 10, KMaxZ);
        GMinZ = AdjustSelect("-", 11, GMinZ);
        GMaxZ = AdjustSelect("=", 12, GMaxZ);
    }

    static float AdjustSelect(string key, int num, float axis) {
        if (Input.GetKey(key)) {
            CurrentAdjustment = num;
        }
        if (CurrentAdjustment == num) {
            if (Input.GetKey("up")) {
                axis += 1.0f;
            }
            if (Input.GetKey("down")) {
                axis -= 1.0f;
            }
        }
        return axis;
    }
}