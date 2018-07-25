using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;

public class ManagerLogic : MonoBehaviour
{
    public GameObject SphereButtonPrefab;
    private GameObject sphereButton;

    public Text UserName;

    private string playerPlayFabId = "";

    // Use this for initialization
    void Start()
    {
        // set up playfab username log in here
        initPlayFab();
    }

    void initPlayFab()
    {
        string user = UserName.text;
        if (user == null || user == "")
        {
            user = "DefaultUser";
        }

        var request = new LoginWithCustomIDRequest { CustomId = user, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        playerPlayFabId = result.PlayFabId;

        setupMemePrinter();
    }

    private void setupMemePrinter()
    {
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.transform.position = new Vector3(0, 0, 0);

        sphereButton = Instantiate(SphereButtonPrefab);
        sphereButton.transform.position = new Vector3(0, 0, -0.5f);
    }

    public void GetMeme()
    {
        if (playerPlayFabId != "")
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest();
            request.FunctionName = "GetMeme";
            request.SpecificRevision = 11;

            //PlayFabClientAPI.ExecuteCloudScript(request, );
            PlayFabServerAPI.UpdateUserInternalData(new UpdateUserInternalDataRequest()
            {
                PlayFabId = playerPlayFabId,
                Data = new Dictionary<string, string>() {
                    // TODO: add value to an "inventory" OR this may just be a kvp, not an expanding table?
                    // May need to GET the current inventory value first before adding the next value
                    //{"Clicks", "0"},
                    //{"InventoryValue", addValue.ToString() }
            },
            },
            result => Debug.Log("Set internal user data successful"),
            error =>
            {
                Debug.Log("Got error updating internal user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }

    public void UpdateUserInternalInventoryValue(double addValue)
    {
        if (playerPlayFabId != "")
        {
            PlayFabServerAPI.UpdateUserInternalData(new UpdateUserInternalDataRequest()
            {
                PlayFabId = playerPlayFabId,
                Data = new Dictionary<string, string>() {
                    // TODO: add value to an "inventory" OR this may just be a kvp, not an expanding table?
                    // May need to GET the current inventory value first before adding the next value
                    //{"Clicks", "0"},
                    {"InventoryValue", addValue.ToString() }
            },
            },
            result => Debug.Log("Set internal user data successful"),
            error =>
            {
                Debug.Log("Got error updating internal user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }

    public void UpdateUserInternalClicks()
    {
        if (playerPlayFabId != "")
        {
            PlayFabServerAPI.UpdateUserInternalData(new UpdateUserInternalDataRequest()
            {
                PlayFabId = playerPlayFabId,
                Data = new Dictionary<string, string>() {
                {"Clicks", "0"},
            },
            },
            result => Debug.Log("Set internal user data successful"),
            error =>
            {
                Debug.Log("Got error updating internal user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }

    public void GetUserInternalData()
    {
        if (playerPlayFabId != "")
        {
            PlayFabServerAPI.GetUserInternalData(new PlayFab.ServerModels.GetUserDataRequest()
            {
                PlayFabId = playerPlayFabId,
            },
            result =>
            {
                if (result.Data == null || !result.Data.ContainsKey("Clicks")) Debug.Log("No Clicks");
                else Debug.Log("Clicks: " + result.Data["Clicks"].Value);
            },
            error =>
            {
                Debug.Log("Got error getting internal user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
