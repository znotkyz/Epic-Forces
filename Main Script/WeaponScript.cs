using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    public Transform fpsCam;

    public float range = 20;

    public float impactForce = 150;

    public int damageAmount = 20;

    public int fireRate = 10;

    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    public int currentAmmo;

    public int maxAmmo = 10;

    public int magazineSize = 30;

    public float reloadTime = 2f;

    private bool isReloading;

    public Animator anim;

    InputAction shoot;

    // Start is called before the first frame update
    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");

        shoot.Enable();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        anim.SetBool("isReloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo == 0 && magazineSize == 0)
        {
            anim.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;

        bool isShooting = shoot.ReadValue<float>() == 1;

        anim.SetBool("isShooting", isShooting);

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            Fire();
        }

        if(currentAmmo == 0 && magazineSize > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        AudioManagerScript.instance.Play("Shoot");

        RaycastHit hit;

        currentAmmo--;

        muzzleFlash.Play();
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            EnemyScript e = hit.transform.GetComponent<EnemyScript>();

            if (e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        anim.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);

        anim.SetBool("isReloading", false);

        if (magazineSize >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineSize -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineSize;
            magazineSize = 0;
        }
        isReloading = false;
    }
}
