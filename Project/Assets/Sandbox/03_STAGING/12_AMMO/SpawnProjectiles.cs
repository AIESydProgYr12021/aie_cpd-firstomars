using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SandBox.Staging.Ammo
{
    public class SpawnProjectiles : MonoBehaviour
    {
        //===NEW
        [SerializeField] Text ammoText;
        [SerializeField] int maxAmmo = 10;
        private int currentAmmo;



        //===
        
        public GameObject firePoint;
        public List<GameObject> vfx = new List<GameObject>();

        private GameObject effectToSpawn;
        private Vector3 playerMoveDir;
        private Quaternion testplayerMoveDir;
        private float timeToFire = 0;

        [SerializeField] private Button shootBtn;
        private Image shootBtnImg;
        private Text shootBtnTxt;

        [Header("Toggle Controls")]
        public bool showControl = true;
        public bool syncWithKeyboardInput = true;
        private bool isControllsShowing = true;


        // Start is called before the first frame update
        void Start()
        {
            //NEW
            currentAmmo = maxAmmo;
            //===

            effectToSpawn = vfx[0];

            shootBtnImg = shootBtn.image;
            shootBtnTxt = shootBtn.GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            ammoText.text = currentAmmo.ToString();
            
            if (showControl != isControllsShowing)
            {
                shootBtnImg.enabled = showControl;
                shootBtnTxt.enabled = showControl;
                isControllsShowing = showControl;
            }

            if (syncWithKeyboardInput && Input.GetKeyDown(KeyCode.Space))
            {
                ShootGun();
            }
        }

        public void ShootGun()
        {
            if (Time.time >= timeToFire && currentAmmo > 0)
            {
                currentAmmo--; //NEW
                timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
                SpawnVFX();
            }
        }

        void SpawnVFX()
        {
            GameObject vfx; //what is this exactly?

            if (firePoint != null)
            {
                vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
                vfx.transform.localRotation = GetPlayerRotation();
            }
            else
            {
                Debug.Log("No fire point");
            }
        }

        private Quaternion GetPlayerRotation()
        {
            playerMoveDir = GetComponentInParent<ThirdPersonScript>().PlayerMoveDirection;
            return testplayerMoveDir = GetComponentInParent<Transform>().rotation;
        }

        public void IncreaseAmmo(int ammoAmt)
        {
            currentAmmo += ammoAmt;
        }
    }
}

