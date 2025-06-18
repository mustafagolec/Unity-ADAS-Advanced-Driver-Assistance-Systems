using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Wheel Colliders
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    // Görsel tekerlek meshleri
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    // Ayarlar
    public float maxMotorTorque = 1500f;      // Gaz
    public float maxSteeringAngle = 30f;      // Direksiyon açısı
    public float brakeTorque = 3000f;         // Fren gücü

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        bool isBraking = Input.GetKey(KeyCode.Space);

        // Direksiyon
        frontLeftCollider.steerAngle = steering;
        frontRightCollider.steerAngle = steering;

        // Gaz
        rearLeftCollider.motorTorque = motor;
        rearRightCollider.motorTorque = motor;

        // Fren
        float appliedBrake = isBraking ? brakeTorque : 0f;
        frontLeftCollider.brakeTorque = appliedBrake;
        frontRightCollider.brakeTorque = appliedBrake;
        rearLeftCollider.brakeTorque = appliedBrake;
        rearRightCollider.brakeTorque = appliedBrake;

        // Tekerlek görsellerini güncelle
        UpdateWheelPose(frontLeftCollider, frontLeftWheel);
        UpdateWheelPose(frontRightCollider, frontRightWheel);
        UpdateWheelPose(rearLeftCollider, rearLeftWheel);
        UpdateWheelPose(rearRightCollider, rearRightWheel);
    }

    void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
