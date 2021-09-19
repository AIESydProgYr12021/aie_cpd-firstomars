using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtnController : MonoBehaviour
{
    [SerializeField] private bool showMobileControl = true;
    private bool isControlShowing = true;
    private GameObject menuBtnMobile;
    private GameObject menuBtnPC;



    private void Start()
    {
        menuBtnMobile = gameObject.transform.GetChild(0).gameObject;
        menuBtnPC = gameObject.transform.GetChild(1).gameObject;

        //if (!showMobileControl) menuBtnPC.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (showMobileControl != isControlShowing)
        {
            menuBtnMobile.SetActive(showMobileControl);
            isControlShowing = showMobileControl;
        }

        if (!showMobileControl) menuBtnPC.SetActive(true);
        else menuBtnPC.SetActive(false);
    }
}
