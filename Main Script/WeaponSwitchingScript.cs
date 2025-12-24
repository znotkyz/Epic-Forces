using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponSwitchingScript : MonoBehaviour
{
    InputAction switching;

    public int selectedWeapon = 0;

    public TextMeshProUGUI ammoInfoText;

    // Start is called before the first frame update
    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");

        switching.Enable();

        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponScript currentGun = FindObjectOfType<WeaponScript>();

        ammoInfoText.text = currentGun.currentAmmo + " / " + currentGun.magazineSize;

        float scrollValue = switching.ReadValue<Vector2>().y;

        int previousSelected = selectedWeapon;

        if (scrollValue > 0)
        {
            selectedWeapon++;
            if (selectedWeapon == 3)
                selectedWeapon = 0;
        }
        else if (scrollValue < 0)
        {
            selectedWeapon--;
            if (selectedWeapon == -1)
                selectedWeapon = 2;
        }

        if (previousSelected != selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }
}
