using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed;
    [SerializeField] float xRange;
    [SerializeField] float yRange;

    [SerializeField] float positionPitchFactor;
    [SerializeField] float controlPitchFactor;
    [SerializeField] float positionYawFactor;
    [SerializeField] float controlRollFactor;

    [SerializeField] GameObject[] lasers;

    float horizontal, vertical;

    Transform cachedTransform;

    void Start()
    {
        cachedTransform = transform;
    }

    void Update()
    {
        Move();
        Rotate();
        Fire();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        float xOffset = horizontal * controlSpeed * Time.deltaTime;
        float newPosX = cachedTransform.localPosition.x + xOffset;
        float clampedPosX = Mathf.Clamp(newPosX, -xRange, xRange);

        float yOffset = vertical * controlSpeed * Time.deltaTime;
        float newPosY = cachedTransform.localPosition.y + yOffset;
        float clampedPosY = Mathf.Clamp(newPosY, -yRange, yRange);

        cachedTransform.localPosition = new Vector3(clampedPosX, clampedPosY, cachedTransform.localPosition.z);
    }

    void Rotate()
    {
        float pitch = cachedTransform.localPosition.y * positionPitchFactor + vertical * controlPitchFactor;
        float yaw = cachedTransform.localPosition.x * positionYawFactor;
        float roll = horizontal * controlRollFactor * Time.deltaTime;

        cachedTransform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
    }

    void ActivateLasers(bool active)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            if (active == true)
            {
                emissionModule.enabled = true;
            }
            else
            {
                emissionModule.enabled = false;
            }
        }
    }
}
