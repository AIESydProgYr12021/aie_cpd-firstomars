using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnProjectiles : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private Transform ammoSpawnPoint;
    [SerializeField] private int maxAmmoSpawn = 1;
    [Range(0, 20)]
    [SerializeField] private int spawnAtMinAmmoAmount;
    private int ammoSpawnCounter = 0;
    [SerializeField] private TMP_Text ammoTxt;
    [SerializeField] private TMP_Text maxAmmoTxt;
    [SerializeField] int maxAmmo = 10;
    public int CurrentAmmo { get; private set; }

    [Header("GameObjects")]
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    [SerializeField] private Button shootBtn;
    private Image shootBtnImg;
    private Text shootBtnTxt;

    [Header("Toggle Controls")]
    public bool showControl = true;
    public bool syncWithKeyboardInput = true;
    private bool isControllsShowing = true;

    private Vector3 playerMoveDir;
    private Quaternion testplayerMoveDir;
    private float timeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = maxAmmo;
        
        effectToSpawn = vfx[0];

        shootBtnImg = shootBtn.image;
        shootBtnTxt = shootBtn.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoTxt.text = CurrentAmmo.ToString();
        maxAmmoTxt.text = "/" + maxAmmo.ToString();

        if (CurrentAmmo < 1) SpawnAmmo(ammoSpawnPoint.position);

        if (showControl != isControllsShowing)
        {
            shootBtnImg.enabled = showControl;
            shootBtnTxt.enabled = showControl;
            isControllsShowing = showControl;
        }

        if (syncWithKeyboardInput && Input.GetKeyDown(KeyCode.Space)) ShootGun();
    }

    public void SpawnAmmo(Vector3 spawnPos)
    {
        if (ammoSpawnCounter < maxAmmoSpawn && CurrentAmmo < spawnAtMinAmmoAmount)
        {
            ammoSpawnCounter++;
            Instantiate(ammoPrefab, spawnPos, Quaternion.identity);
            //Debug.Log("Increased ammo spawn counter " + ammoSpawnCounter);
        }
    }

    public void DecreaseAmmoSpawnCounter()
    {
        ammoSpawnCounter--;
        if (ammoSpawnCounter < 0) ammoSpawnCounter = 0;
        //Debug.Log("Decreased ammo spawn counter " + ammoSpawnCounter);
    }

    public void ShootGun()
    {
        if (Time.time >= timeToFire && CurrentAmmo > 0)
        {
            CurrentAmmo--;
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
        if (CurrentAmmo + ammoAmt > maxAmmo) 
            ammoAmt = maxAmmo - CurrentAmmo;

        CurrentAmmo += ammoAmt;
    }



}

