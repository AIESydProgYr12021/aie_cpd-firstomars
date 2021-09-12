using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SandBox.Staging.PlayerShoot
{
    public class ShootButton : MonoBehaviour
    {
        private Image shootBtnImg;
        private Text shootBtnTxt;

        [Header("Toggle Controls")]
        public bool showControl = true;
        public bool syncWithKeyboardInput = true;
        private bool isControllsShowing = true;

        // Start is called before the first frame update
        void Start()
        {
            shootBtnImg = GetComponent<Image>();
            shootBtnTxt = GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (showControl != isControllsShowing)
            {
                shootBtnImg.enabled = showControl;
                shootBtnTxt.enabled = showControl;
                isControllsShowing = showControl;
            }
        }
    }
}
