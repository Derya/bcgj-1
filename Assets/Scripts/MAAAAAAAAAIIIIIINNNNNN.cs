using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{
    [SerializeField]
    float thrustFactor;
    [SerializeField]
    GameObject indicator;
    [SerializeField]
    GameObject velocity_text;

    readonly AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    readonly AcceleratableValue pitch = new AcceleratableValue(1.0f, 0.5f);
    readonly AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        updateIndicator();
    }

    void FixedUpdate()
    {
        handleControls();
    }

    void Update()
    {
        updateIndicator();
    }

    void handleControls()
    {
        var deltaTime = Time.deltaTime;

        var rollRight = Input.GetKey("d");
        var rollLeft = Input.GetKey("a");

        roll.fromBools(rollRight, rollLeft, deltaTime);

        var pitchUp = Input.GetKey("w");
        var pitchDown = Input.GetKey("s");

        pitch.fromBools(pitchUp, pitchDown, deltaTime);

        var yawLeft = Input.GetKey("q");
        var yawRight = Input.GetKey("e");

        yaw.fromBools(yawLeft, yawRight, deltaTime);

        transform.Rotate(pitch.getCurrentValue(), yaw.getCurrentValue(), roll.getCurrentValue());

        //float forwardVel = transform.InverseTransformDirection(rigidBody.velocity).z;
        if (Input.GetKey("left shift"))
        {

            rigidBody.AddRelativeForce(Vector3.forward * thrustFactor * 1);
        }
    }

    void updateIndicator()
    {
        var basePosition = indicator.transform.position;
        var velocityDirection = rigidBody.velocity.normalized;
        indicator.transform.LookAt(velocityDirection + basePosition);


    }


}
