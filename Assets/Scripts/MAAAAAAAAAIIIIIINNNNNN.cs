using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{
    [SerializeField]
    float thrustFactor;

    readonly AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    readonly AcceleratableValue pitch = new AcceleratableValue(1.0f, 0.5f);
    readonly AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        handleControls();
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
        rigidBody.AddRelativeForce(Vector3.forward * thrustFactor * 1);
    }



}
