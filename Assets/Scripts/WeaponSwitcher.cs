using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;

    public Camera pistolCamera;
    public Camera shotgunCamera;

    void Start()
    {
        // Initialize the scene with only the pistol active
        EnablePistol();
    }

    void Update()
    {
        // Check for input to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnablePistol();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableShotgun();
        }
    }

    void EnablePistol()
    {
        pistol.SetActive(true);
        shotgun.SetActive(false);

        pistolCamera.enabled = true;
        shotgunCamera.enabled = false;
    }

    void EnableShotgun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(true);

        pistolCamera.enabled = false;
        shotgunCamera.enabled = true;
    }
}
