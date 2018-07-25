using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ManagerLogic : MonoBehaviour
{
    public GameObject SphereButtonPrefab;
    private GameObject sphereButton;

    public Text UserName;

    // Use this for initialization
    void Start()
    {

        // set up playfab username log in here
        initPlayFab();
    }

    void initPlayFab()
    {
        // Get the text that the user entered
        //UserName.text;
        Debug.Log("user name" + UserName.text);

        string user = UserName.text;
        if (user == null || user == "")
        {
            user = "DefaultUser";
        }

        var request = new LoginWithCustomIDRequest { CustomId = UserName.text, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //body.transform.position = new Vector3(0, 0.5f, 0);
        body.transform.position = new Vector3(0, 0, 0);
        //body.transform.position = new Vector3(0, 0, 0);

        //sphereButton = Instantiate(sphereButton, transform.position, transform.rotation);
        sphereButton = Instantiate(SphereButtonPrefab);

        //buttonPush.collider.
        sphereButton.transform.position = new Vector3(0, 0, -0.5f);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
