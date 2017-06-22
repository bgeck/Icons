using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenNI;

public class customNI : MonoBehaviour {
    public OpenNIUserTracker UserTracker;
    public int UserId;
    public GUIStyle customStyle = new GUIStyle();
    private Vector3 thisUser;
    private Vector3 lerpedUser;

    void Start() {
        if (!UserTracker) {
            UserTracker = gameObject.AddComponent<OpenNIUserTracker>();
        }
    }

    void Update() {
        foreach (int userId in UserTracker.AllUsers) {
            if (userId == UserId) {
                thisUser = UserTracker.GetUserCenterOfMass(userId);
                thisUser.x = -thisUser.x;
                lerpedUser.x = thisUser.x.Remap(
                    MainUserTracker.KMinX, 
                    MainUserTracker.KMaxX, 
                    MainUserTracker.GMinX, 
                    MainUserTracker.GMaxX
                );
                lerpedUser.y = thisUser.y.Remap(
                    MainUserTracker.KMinY, 
                    MainUserTracker.KMaxY, 
                    MainUserTracker.GMinY, 
                    MainUserTracker.GMaxY
                );
                lerpedUser.z = thisUser.z.Remap(
                    MainUserTracker.KMinZ, 
                    MainUserTracker.KMaxZ, 
                    MainUserTracker.GMinZ, 
                    MainUserTracker.GMaxZ
                );
                gameObject.transform.position = lerpedUser;
            }
        }
    }

    void OnGUI() {
        if (!MainUserTracker.showUsers) {
        }
        else {
            GUI.Label(new Rect(0, 0, 300, 100), "KMinX:" + MainUserTracker.KMinX, customStyle);
            GUI.Label(new Rect(0, 20, 300, 100), "KMaxX:" + MainUserTracker.KMaxX, customStyle);
            GUI.Label(new Rect(0, 40, 300, 100), "GMinX:" + MainUserTracker.GMinX, customStyle);
            GUI.Label(new Rect(0, 60, 300, 100), "GMaxX:" + MainUserTracker.GMaxX, customStyle);
            GUI.Label(new Rect(120, 0, 300, 100), "KminY:" + MainUserTracker.KMinY, customStyle);
            GUI.Label(new Rect(120, 20, 300, 100), "KMaxY:" + MainUserTracker.KMaxY, customStyle);
            GUI.Label(new Rect(120, 40, 300, 100), "GMinY:" + MainUserTracker.GMinY, customStyle);
            GUI.Label(new Rect(120, 60, 300, 100), "GMaxY:" + MainUserTracker.GMaxY, customStyle);
            GUI.Label(new Rect(240, 0, 300, 100), "KMinZ:" + MainUserTracker.KMinZ, customStyle);
            GUI.Label(new Rect(240, 20, 300, 100), "KMaxZ:" + MainUserTracker.KMaxZ, customStyle);
            GUI.Label(new Rect(240, 40, 300, 100), "GMinZ:" + MainUserTracker.GMinZ, customStyle);
            GUI.Label(new Rect(240, 60, 300, 100), "GMaxZ:" + MainUserTracker.GMaxZ, customStyle);
        }
    }
}