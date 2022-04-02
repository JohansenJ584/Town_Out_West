using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ReasourceManager : MonoBehaviour
{
    public int rayLength = 4;
    public GameObject TextBox_F;
    public GameObject LogDepositText;
    public GameObject LogCountObject;
    public GameObject CoinCountObject;
    public GameObject Inventory;
    public GameObject StoreUI;


    CoinStats CoinScript;
    CoinCount CoinCount;
    LogDepositScript LogDepositThing;
    LogCount LogcountScript;
    PressFToUse TextBox_F_Script;
    float curTime = 0;
    float waitFor = .2f;
    bool isInventoyOpen = false;
    bool isStoreOpen = false;

    //Loading Scene
    bool isItChangingScene = false;
    [SerializeField]
    private string scene;
    [SerializeField]
    private Text loadingText;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        /*
        //Debug.Log("Level: " + scene.name);
        if (GameObject.FindGameObjectWithTag("LogBarrel") != null)
        {
            LogDepositThing = GameObject.FindGameObjectWithTag("LogBarrel").GetComponentInChildren<LogDepositScript>();
        }
        else
        {
            LogDepositThing = null;
        }
        if(scene.name == "TownTest")
        {
            SaveSystem.LoadTownBetweenScenes();
        }
        */
    }

    void Start()
    {

        TextBox_F_Script = TextBox_F.GetComponent<PressFToUse>();
        LogcountScript = LogCountObject.GetComponent<LogCount>();

        CoinCount = CoinCountObject.GetComponent<CoinCount>();
        /*
        if (GameObject.FindGameObjectWithTag("LogBarrel") != null)
        {
            LogDepositThing = GameObject.FindGameObjectWithTag("LogBarrel").GetComponentInChildren<LogDepositScript>();
        }
        else
        {
            LogDepositThing = null;
        }
        */
        //CoinCount = CoinCountObject.GetComponent<CoinCount>();
        //Debug.Log("Does this Exist      " + CoinCount);

        //This is for auto detection method run
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading()//(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        loadingText.gameObject.transform.parent.gameObject.SetActive(false);
        //For Loading Scene
        isItChangingScene = false;
        //loadingText.text = "";

        SaveSystem.LoadTownBetweenScenes();

    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
        //The Next Step
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            OpenStore(hit);
            ItemToPickup(hit);
            LogPickUp(hit);
            LogDeposit(hit);
            TempSceneSwitch(hit);
        }
        else if (Time.time - curTime >= waitFor)
        {
            curTime = Time.time;
            TextBox_F_Script.PickableBread(false);
        }
        //This is for the loading screen
        //if (isItChangingScene)
        //{
        //    loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        //}
    }


    void TempSceneSwitch(RaycastHit tempHit)
    {
        if(tempHit.collider.gameObject.tag == "SceneChanger" && Input.GetKeyDown(KeyCode.F) && !isItChangingScene)
        {
            loadingText.gameObject.transform.parent.gameObject.SetActive(true);
            //SceneManager.LoadScene("TestingForRooms");
            if (tempHit.collider.gameObject.GetComponentInChildren<TextMesh>().text == "Forest")
            {
                scene = "TestingForRooms"; //this is a temp name
                //SaveSystem.SaveTownBetweenScenes();
            }
            else if ((tempHit.collider.gameObject.GetComponentInChildren<TextMesh>().text == "Home"))
            {
                scene = "TownTest"; //This is temp name
            }
            else if ((tempHit.collider.gameObject.GetComponentInChildren<TextMesh>().text == "Test"))
            {
                //SaveSystem.SaveTownBetweenScenes(); //When you leave The Town
                scene = "SampleScene";
            }
            if(tempHit.collider.gameObject.GetComponentInChildren<TextMesh>().text != "Home")
            {
                SaveSystem.SaveTownBetweenScenes();
            }

                isItChangingScene = true;

            loadingText.text = "Loading...";

            StartCoroutine(LoadNewScene());
            //StartCoroutine(FadeLoadingScreen(0f, 3f));
            //if (isItChangingScene)
            //{
            //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            //}
        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(.1f);


        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return new WaitForSeconds(3f);
            //Debug.Log("Hello Darkness");
            OnLevelFinishedLoading();
            yield return null;
        }
        //yield return StartCoroutine(FadeLoadingScreen(0, 3f));
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    { 
        float time = 0;

        while (time < duration)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            time += Time.deltaTime;
            yield return null;
        }
    }




















    void OpenStore(RaycastHit tempHit)
    {
        if (tempHit.collider.gameObject.tag == "Store")
        {
            TextBox_F_Script.PickableBread(true);
            if (Input.GetKeyDown(KeyCode.F) && !isInventoyOpen)
            {
                //These values Are wrong
                //Debug.Log(tempHit.transform.parent.name.Substring(tempHit.transform.parent.name.Length- 1, 1));

                StoreManager.instance.activeStore = System.Convert.ToInt32(tempHit.transform.parent.name.Substring(tempHit.transform.parent.name.Length - 1, 1)) - 1;

                GameObject.Find("UiControler").GetComponent<StoreUi>().WhatItems(StoreManager.instance.activeStore);

                StoreUI.SetActive(!StoreUI.activeSelf);
                PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled = !PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled;
                PlayerManager.instance.player.GetComponentInChildren<MouseLook>().enabled = !PlayerManager.instance.player.GetComponentInChildren<MouseLook>().enabled;
                if (StoreUI.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                TextBox_F_Script.PickableBread(false);
                isStoreOpen = !isStoreOpen;
            }
        }
    }

    void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isStoreOpen)
        {
            PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled = !PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled;
            PlayerManager.instance.player.GetComponentInChildren<MouseLook>().enabled = !PlayerManager.instance.player.GetComponentInChildren<MouseLook>().enabled;
            Inventory.SetActive(!Inventory.activeSelf);
            if (Inventory.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isInventoyOpen = !isInventoyOpen;
        }
    }

    void ItemToPickup(RaycastHit tempHit)
    {
        if (tempHit.collider.gameObject.tag == "Item")
        {
            TextBox_F_Script.PickableBread(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                tempHit.transform.GetComponent<ItemPickup>().PickUp();
                TextBox_F_Script.PickableBread(false);
            }
        }
        else if(tempHit.collider.gameObject.tag == "Coin")
        {
            TextBox_F_Script.PickableBread(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                CoinScript = tempHit.transform.GetComponent<CoinStats>();
                float coinValue = CoinScript.Value;
                CoinCount.ChangeCoinCount(coinValue);
                CoinScript.PickUp();
                TextBox_F_Script.PickableBread(false);
            }
        }
    }

    void LogDeposit(RaycastHit tempHit)
    {
        if (tempHit.collider.gameObject.tag == "LogBarrel")
        {
            TextBox_F_Script.PickableBread(true);
            curTime = Time.time;
            if(Input.GetKeyDown(KeyCode.F) && LogcountScript.IsLogLeft())
            {
                tempHit.transform.GetComponentInChildren<LogDepositScript>().ChangeLogCount(1);
                //LogDepositThing.ChangeLogCount(1);
                LogcountScript.ChangeLogCount(-1);
                TextBox_F_Script.PickableBread(false);
            }
        }
    }

    void LogPickUp(RaycastHit tempHit)
    {
        if (tempHit.collider.gameObject.tag == "Log")
        {
            TextBox_F_Script.PickableBread(true);
            curTime = Time.time;
            if (Input.GetKeyDown(KeyCode.F))
            {
                LogcountScript.ChangeLogCount(1);
                Destroy(tempHit.collider.gameObject);
                TextBox_F_Script.PickableBread(false);
            }
        }
        else if (Time.time - curTime >= waitFor)
        {
            curTime = Time.time;
            TextBox_F_Script.PickableBread(false);
        }
    }
}
