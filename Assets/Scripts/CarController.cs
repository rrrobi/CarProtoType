using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public List<AxleInfo> axleInfos;     // info about each axle
    public float maxMotorTorque;        //  maximum torque the motor can apply to wheel
    public float maxSteeringAngle;      // max steering agnle the wheel can have

    //float motor = 0f;
    float steeringModifier = 0f;
    bool leftPressed = false;
    bool rightPressed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //motor = maxMotorTorque * Input.GetAxis("Vertical");
        //steering = maxSteeringAngle * Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.LeftArrow))
            leftPressed = true;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            leftPressed = false;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            rightPressed = true;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            rightPressed = false;

        Debug.Log("rightPressed: " + rightPressed);
        Debug.Log("leftPressed: " + leftPressed);
    }

    void SetSteering()
    {
        float sensitivity = 3f;
        //float value = steeringModifier;

        float target = 0f;
        if (leftPressed)
            target = -1f;
        else if (rightPressed)
            target = 1;

        steeringModifier = Mathf.MoveTowards(steeringModifier, target, sensitivity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //bool pressed = false;
        //float turnTarget = (pressed == false) ? 0 : 1f;
        //SetSteering();

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = /*steeringModifier * maxSteeringAngle;*/maxSteeringAngle * Input.GetAxis("Horizontal");
       // Debug.Log("steering input: " + Input.GetAxis("Horizontal"));
       // Debug.Log("steeringRaw input: " + Input.GetAxisRaw("Horizontal"));
       // Debug.Log("steering value: " + steeringModifier);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                //Debug.Log("steering value: " + steering);
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            ApplyPositionToVisuals(axleInfo.leftWheel);
            ApplyPositionToVisuals(axleInfo.rightWheel);
        }
    }

    // find wheel in question, apply transform
    public void ApplyPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
            return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to the motor?
    public bool steering; // does this wheel apply steer angle?
}
