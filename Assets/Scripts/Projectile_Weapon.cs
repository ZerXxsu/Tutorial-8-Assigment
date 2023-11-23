using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{

    //Bullet Firing
    [SerializeField] private Rigidbody myBullet;
    [SerializeField] private float force = 50;

    //Recoil
    [SerializeField] private float recoilDistance = 0.1f;
    [SerializeField] private float recoilDuration = 0.1f;

    //Audio
    AudioSource shootSound;

    

    protected override void Attack(float percent)
    {
        shootSound = GetComponent<AudioSource>();  //Initalize Audio
        shootSound.Play();  //Play audio

        FireProjectile(percent);  // Fire the projectile
        StartCoroutine(Recoil());  // Apply recoil movement to the weapon model

    }



    //Firing Function
    private void FireProjectile(float percent)
    {
        Ray camRay = InputManager.GetCameraRay();
        Rigidbody rb = Instantiate(myBullet, camRay.origin, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * camRay.direction, ForceMode.Impulse);
        Destroy(rb.gameObject, 1);
    }



    //Recoil Function
    private IEnumerator Recoil()
    {
        Vector3 originalPosition = transform.localPosition;  //Saves original position
        Vector3 targetPosition = originalPosition - transform.forward * recoilDistance;  //Calculate position for recoil along Z-axis

        //Find position for smooth recoil effect
        float t = 0f;
        while (t < recoilDuration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t / recoilDuration);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);  //Pause for duration

        // Return to original position
        t = 0f;
        while (t < recoilDuration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, t / recoilDuration);
            yield return null;
        }

        transform.localPosition = originalPosition;  //Final position = original position
    }



}






