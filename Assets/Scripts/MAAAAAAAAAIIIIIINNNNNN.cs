using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{
    [SerializeField]
    float thrustFactor;
    [SerializeField]
    GameObject indicator;
    [SerializeField]
    GameObject textGameObjec;

    readonly AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    readonly AcceleratableValue pitch = new AcceleratableValue(1.0f, 0.5f);
    readonly AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);

    Rigidbody rigidBody;
    TextMeshPro velocity_text;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        velocity_text = textGameObjec.GetComponent<TextMeshPro>();
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

        float speed = rigidBody.velocity.magnitude;
        float angle = Vector3.Angle(velocityDirection, transform.forward);

        float velocityPoints = Mathf.Lerp(0, 10, Mathf.Clamp(speed / 20.0f, 0, 1));
        float anglePoints = Mathf.Lerp(0, 10, Mathf.Clamp(angle / 90.0f, 0, 1));
        float score = velocityPoints * anglePoints;

        velocity_text.SetText(
            noDecimalFormat(speed) + " m/s" + 
            "\n" + noDecimalFormat(angle) + " degrees" +
            "\nvelocity points: " + noDecimalFormat(velocityPoints) +
            "\nangle points: " + noDecimalFormat(anglePoints) +
            "\nscore: " + noDecimalFormat(score)
        );
    }

    string noDecimalFormat(float x)
    {
        string ret = x.ToString("#");
        if (ret.Length < 1)
        {
            return "0";
        }
        else
        {
            return ret;
        }
    }

}
