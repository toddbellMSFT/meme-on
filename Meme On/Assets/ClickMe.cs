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

    private const string testUrl = "https://i.imgur.com/boOaceQ.jpg";
    private const string testMovieUrl = "http://i.imgur.com/7qWDgR0.gifv";

    public GameObject PlanarPrefab;
    private GameObject planarRenderer;

    // Use this for initialization
    void Start () {
        planarRenderer = Instantiate(PlanarPrefab);
    }

    // Update is called once per frame
    void Update() {
        if (isClicked)
        {
            Debug.Log("Clicked and Updating! " + this.transform.position.z);
            this.transform.position -= cWaitForClickVelocity;

            planarRenderer.transform.position += cMemeScrollVelocity;

            if(this.transform.position.z <= cDefaultZ)
            {
                isClicked = false;
            }
        }
    }

    private IEnumerator OnMouseDown()
    {
        if (!isClicked)
        {
            // Test if this actually gets called
            Debug.Log("Mouse Down!");
            this.transform.position = new Vector3(0, 0, 0);
            isClicked = true;

            using (WWW www = new WWW(testUrl))
            {
                yield return www;
                Renderer renderer = planarRenderer.GetComponent<Renderer>();
                renderer.material.mainTexture = www.texture;
                planarRenderer.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
