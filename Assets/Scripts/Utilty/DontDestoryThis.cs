using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryThis : MonoBehaviour
{
    private static DontDestoryThis playerInstance;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
