using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SandBox.Staging.PlayerShoot
{
    public class SpawnProjectiles : MonoBehaviour
    {
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
            effectToSpawn = vfx[0];

            shootBtnImg = shootBtn.image;
            shootBtnTxt = shootBtn.GetComponentInChildren<Text>();
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

            if (syncWithKeyboardInput && Input.GetKeyDown(KeyCode.Space))
            {
                ShootGun();
                
                //timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
                //SpawnVFX();
            }
        }

        public void ShootGun()
        {
            if (Time.time >= timeToFire)
            {
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
    }
}

