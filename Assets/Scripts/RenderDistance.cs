using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDistance : MonoBehaviour
{
    Transform mainCamTransform; // Stores the FPS camera transform
    private bool visible = true;
    public float distanceToAppear = 50.0f;
    Renderer objRenderer;

    private void Start()
    {
        mainCamTransform = Camera.main.transform;//Get camera transform reference
        objRenderer = gameObject.GetComponent<Renderer>(); //Get render reference
        StartCoroutine(disappearChecker());
    }
    //private void Update()
    //{
    //    disappearChecker();
    //}
    IEnumerator disappearChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(.33f);
            float distance = Vector3.Distance(mainCamTransform.position, transform.position);
            Debug.Log(distance);
            // We have reached the distance to Enable Object
            if (distance < distanceToAppear)
            {
                if (!visible)
                {
                    if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).childCount > 0)
                    {
                        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                        {
                            gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                        }
                    }
                    else if (gameObject.transform.childCount > 0)
                    {
                        for (int i = 0; i < gameObject.transform.childCount; i++)
                        {
                            gameObject.transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    //objRenderer.enabled = true; // Show Object
                    visible = true;
                    //Debug.Log("Visible");
                }
            }
            else if (visible)
            {
                if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).childCount > 0)
                {
                    for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                    {
                        gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
                    }
                }
                else if (gameObject.transform.childCount > 0)
                {
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                //objRenderer.enabled = false; // Hide Object
                visible = false;
                //Debug.Log("InVisible");
            }
        }
    }
}
