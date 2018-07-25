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

    private const float fastClickSpeed = 0.1f;
    private const float mediumClickSpeed = fastClickSpeed/2;
    private const float slowClickSpeed = fastClickSpeed/4;
    private Vector3 cWaitForClickVelocity = new Vector3(0, 0, slowClickSpeed);

    private const float memeScrollSpeed = 0.1f;
    private Vector3 cMemeScrollVelocity = new Vector3(0, memeScrollSpeed, 0);

    private const float cDefaultZ = -0.5f;

    public GameObject PlanarPrefab;
    private GameObject planarRenderer;

    public static GameObject ManagerLogic;

    // TODO: load current count from PlayFab user account that got loaded on Start
    public UnityEngine.UI.Text currentCountText;
    private double currentCount = 0;

    // Use this for initialization
    void Start () {
        planarRenderer = Instantiate(PlanarPrefab);
        // TODO: attach text to the "front" (0, 0, -0.5) of the button... OR just slide it onto another panel?
        currentCountText = GetComponent<UnityEngine.UI.Text>();
        ManagerLogic = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update() {
        if (isClicked)
        {
            this.transform.position -= cWaitForClickVelocity;

            planarRenderer.transform.position += cMemeScrollVelocity;

            if(this.transform.position.z <= cDefaultZ)
            {
                isClicked = false;
            }
        }
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
            this.transform.position = new Vector3(0, 0, 0);
            isClicked = true;

            var logic = ManagerLogic.GetComponent<ManagerLogic>();
            string memeUrl = logic.GetCurrentMeme();

            using (WWW www = new WWW(memeUrl))
            {
                yield return www;
                Renderer renderer = planarRenderer.GetComponent<Renderer>();
                renderer.material.mainTexture = www.texture;
                planarRenderer.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
