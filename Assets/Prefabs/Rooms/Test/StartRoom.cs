using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.player.transform.position = gameObject.transform.position + new Vector3(0f, 10f, 0f);
        }
    }
}
