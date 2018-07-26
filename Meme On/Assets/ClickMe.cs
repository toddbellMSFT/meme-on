using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMe : MonoBehaviour {

    /// <summary>
    ///  By default, we are not clicked. 
    ///  If we are, our state will have changed and 
    ///  we will want to be reverting back to our normal state
    /// </summary>
    private bool isClicked = false;

    /// <summary>
    /// Meme should move independent of the button
    /// </summary>
    private bool hasBeenReset = false;

    private const float fastClickSpeed = 0.1f;
    private const float mediumClickSpeed = fastClickSpeed/2;
    private const float slowClickSpeed = fastClickSpeed/4;
    private Vector3 cWaitForClickVelocity = new Vector3(0, 0, slowClickSpeed);

    private const float memeScrollSpeed = 0.1f;
    private const float slowMemeScrollSpeed = memeScrollSpeed/4;
    private Vector3 cMemeScrollVelocity = new Vector3(0, slowMemeScrollSpeed, 0);

    private Vector3 cDefaultPosition = new Vector3(0, 0, -7.5f);

    private float cDefaultZ = -0.5f;
    private float cMemeY = 1.0f;

    public GameObject PlanarPrefab;
    private GameObject planarRenderer;

    public static GameObject ManagerLogic;

    // TODO: load current count from PlayFab user account that got loaded on Start
    public UnityEngine.UI.Text currentCountText;
    private double currentCount = 0;

    private bool startOnce = false;

    // Use this for initialization
    void Start()
    {
        if (!startOnce)
        {
            planarRenderer = Instantiate(PlanarPrefab);
            // TODO: attach text to the "front" (0, 0, -0.5) of the button... OR just slide it onto another panel?
            currentCountText = GetComponent<UnityEngine.UI.Text>();
            ManagerLogic = GameObject.Find("GameManager");

            cDefaultZ = cDefaultPosition.z - 0.5f;
            cMemeY = cDefaultPosition.y + 1.0f;
            planarRenderer.transform.position = cDefaultPosition;
            startOnce = true;
        }
    }

    // Update is called once per frame
    void Update() {

        if (hasBeenReset)
        {
            if(CheckIsMemeDone())
            {
                planarRenderer.transform.position += cMemeScrollVelocity;
                hasBeenReset = CheckIsMemeDone();
            }
        }

        if (isClicked)
        {
            if(CheckIsButtonDone())
            {
                this.transform.position -= cWaitForClickVelocity;
                isClicked = CheckIsButtonDone();
            }
        }
    }

    bool CheckIsButtonDone()
    {
        return this.transform.position.z > cDefaultZ;
    }

    bool CheckIsMemeDone()
    {
        return planarRenderer.transform.position.y <= cMemeY;
    }

    private void updateClick()
    {
        var logic = ManagerLogic.GetComponent<ManagerLogic>();
        logic.UpdateUserInternalClicks();
    }

    private IEnumerator OnMouseDown()
    {
        if (!isClicked)
        {
            // Test if this actually gets called
            currentCount++;
            updateClick();

            Debug.Log("Mouse Down: currentCount " + currentCount.ToString());
            currentCountText.text = currentCount.ToString();
            this.transform.position = cDefaultPosition;
            isClicked = hasBeenReset = true;

            var logic = ManagerLogic.GetComponent<ManagerLogic>();
            string memeUrl = logic.GetCurrentMeme();

            using (WWW www = new WWW(memeUrl))
            {
                yield return www;
                Renderer renderer = planarRenderer.GetComponent<Renderer>();
                renderer.material.mainTexture = www.texture;
                planarRenderer.transform.position = cDefaultPosition;
            }
        }
    }
}
