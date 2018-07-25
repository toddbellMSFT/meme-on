////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

//public class LogInControl : MonoBehaviour
//{

//    private string userNameStr = string.Empty();

//    // I don't think this will be required
//    private string passStr = string.Empty();

//    private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void OnGUI()
//    {
//        GUI.Window(0, windowRect, WindowFunction, "Login");
//    }

//    void WindowFunction(int windowId)
//    {
//        // TODO: last 10 is possible length
//        userNameStr = GUI.TextField(new Rect(Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 10), userNameStr, 10);

//        passwordString = GUI.PasswordField(new Rect(Screen.width / 3, 2 * Screen.height / 3, Screen.width / 3, Screen.height / 10), passStr, "*"[0], 10);

//        if (GUI.Button(new Rect(Screen.width / 2, 4 * Screen.height / 5, Screen.width / 6, Screen.height / 8), "Log In"))
//        {
//            // Log in with specified user name

//        }

//        GUI.Label(new Rect(Screen.width / 3, 35 * Screen.height / 100, Screen.width / 5, Screen.height / 8), "Username");
//        GUI.Label(new Rect(Screen.width / 3, 62 * Screen.height / 100, Screen.width / 8, Screen.height / 8), "Password");
//    }
//}
