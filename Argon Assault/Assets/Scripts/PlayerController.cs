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
}
