using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunScript : WeaponBase
{
    [SerializeField] private Rigidbody myBullet2;
    [SerializeField] private float force = 50;
    [SerializeField] private int numberOfBullets = 5; // Adjust the number of bullets as needed

    //Recoil
    [SerializeField] private float recoilDistance = 0.1f;
    [SerializeField] private float recoilDuration = 0.1f;

    //Audio
    AudioSource shootSound;

    protected override void Attack(float percent)
    {

        shootSound = GetComponent<AudioSource>();  //Initalize Audio
        shootSound.Play();  //Play audio

        StartCoroutine(Recoil());  // Apply recoil movement to the weapon model

        Ray camRay = InputManager.GetCameraRay();

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 randomDirection = Quaternion.Euler(Random.Range(-3f, 3f), Random.Range(-3, 3), 0) * camRay.direction;
            Rigidbody rb = Instantiate(myBullet2, camRay.origin, transform.rotation);
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * randomDirection, ForceMode.Impulse);
            Destroy(rb.gameObject, 1);
        }
    }




    //Recoil Function
    private IEnumerator Recoil()
    {
        Vector3 originalPosition = transform.localPosition;  //Saves original position
        Vector3 targetPosition = originalPosition + transform.forward * recoilDistance;  //Calculate position for recoil along Z-axis

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

