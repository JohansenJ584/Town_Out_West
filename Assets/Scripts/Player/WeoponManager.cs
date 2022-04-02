using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeoponManager : MonoBehaviour
{
    #region singleton
    public static WeoponManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public bool AnimationBooleanForColliders;
    //public bool SecondAnimationBoolean = true;
    protected GameObject arrow; //THis is going to change
    public bool IsArrowThere = true;      //This is done in the motion Dont forget that
    bool canShoot = true;
    float shootForce = 50;
    Animator anim;
    int whatWeopon = 1; //Sword
    //private IEnumerator coroutine;
    bool axeSwing = false;
    EquipmentManager equipMang;

    EquipmentSlot[] Equipedslots = new EquipmentSlot[8]; //This is hard coded right now
    public GameObject[] PlayerSlots;

    // Start is called before the first frame update
    void Start()
    {
        equipMang = EquipmentManager.instance;
        anim = GetComponent<Animator>();
        arrow = null;
        AnimationBooleanForColliders = false;
    }

    public bool GetAxeSwing()
    {
        return axeSwing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SwingAndSwitch();
        if(AnimationBooleanForColliders)
        {
            AnimationBooleanForColliders = false;
            StartCoroutine(turnColliederToOther());
        }
    }

    public void EquipingAndDequiping()
    {
        for (int i = 0; i < equipMang.currentEquipment.Length; i++)
        {
            if(equipMang.currentEquipment[i] != Equipedslots[i])
            {
                if (PlayerSlots[i].transform.childCount > 0)
                {
                    //Debug.Log("DELETE");
                    Destroy(PlayerSlots[i].transform.GetChild(0).gameObject);
                }
                GameObject tempOb = Instantiate(equipMang.currentEquipment[i].prefabEquipment, PlayerSlots[i].transform);
                if (i+1 != whatWeopon)
                {
                    tempOb.SetActive(false);
                }
            }
        }
        arrow = PlayerSlots[3].transform.GetChild(0).gameObject;

    }


    void SwingAndSwitch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (whatWeopon == 1)
            {
                anim.SetBool("SwordSwing", true);
                AnimationBooleanForColliders = true;
            }
            else if (whatWeopon == 2)
            {
                anim.SetBool("AxeSwing", true);
            }
            else if (whatWeopon == 3)
            {
                if (anim.GetBool("BowShot") == false)
                {
                    anim.SetBool("BowShot", true);
                }
                else
                {
                    ShootArrow();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("1") && whatWeopon != 1)
            {
                anim.SetInteger("StoreWhat", whatWeopon);
                anim.SetInteger("DisplayWhat", 1);
                whatWeopon = 1;
                axeSwing = false;
            }
            else if (Input.GetKeyDown("2") && whatWeopon != 2)
            {
                anim.SetInteger("StoreWhat", whatWeopon);
                anim.SetInteger("DisplayWhat", 2);
                whatWeopon = 2;
                axeSwing = true;
            }
            else if (Input.GetKeyDown("3") && whatWeopon != 3)
            {
                anim.SetInteger("StoreWhat", whatWeopon);
                anim.SetInteger("DisplayWhat", 3);
                whatWeopon = 3;
                axeSwing = false;
            }
            else
            {
                //Debug.Log("Shooting Arrow");
                ShootArrow();
            }
        }
    }

    void ShootArrow()
    {
        arrow = PlayerSlots[3].transform.GetChild(0).gameObject;
        //Debug.Log(anim.GetBool("BowShot") + " : " + whatWeopon + " : " + !IsArrowThere + " : " + canShoot);
        if (whatWeopon == 3 && !IsArrowThere && anim.GetBool("BowShot") && canShoot)
        {
            //Debug.Log("Shooting Arrow 4");
            GameObject tempArrowInBow = GameObject.FindGameObjectWithTag("ArrowInBow");
            if (tempArrowInBow != null)
            {

                //Debug.Log(tempArrowInBow);
                GameObject shot = Instantiate(arrow, tempArrowInBow.transform.position, tempArrowInBow.transform.rotation);
                shot.SetActive(true);
                Rigidbody rb = shot.transform.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = GameObject.FindGameObjectWithTag("ArrowInBow").transform.forward * shootForce;
                canShoot = false;
            }
            else
            {
                //Debug.Log(tempArrowInBow);
            }
        }
        else if (whatWeopon == 3 && IsArrowThere && !canShoot)
        {
            canShoot = true;
        }
    }

    IEnumerator turnColliederToOther()
    {
        gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = !gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<BoxCollider>().enabled;
        yield return new WaitForSeconds(.75f);
        gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = !gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<BoxCollider>().enabled;
        StopCoroutine(turnColliederToOther());
    }
}
