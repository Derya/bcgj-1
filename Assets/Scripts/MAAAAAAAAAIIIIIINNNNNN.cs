using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{
    [SerializeField]
    float thrustFactor;
    [SerializeField]
    GameObject vel_indicator;
    [SerializeField]
    float fastRotateSpeed;
    [SerializeField]
    GameObject textGameObjec;
    [SerializeField]
    GameObject cameraGameObject;

    [SerializeField]
    AudioSource layer1;
    [SerializeField]
    AudioSource layer2;
    [SerializeField]
    AudioSource layer3;

    readonly AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    readonly AcceleratableValue pitch = new AcceleratableValue(1.5f, 0.7f);
    readonly AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);

    Rigidbody rigidBody;
    TextMeshPro velocity_text;
    ParticleSystem particles;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        velocity_text = textGameObjec.GetComponent<TextMeshPro>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        updateIndicator();
    }

    float last_s_up = -100;
    Quaternion? rotateTarget = null;

    void rotateTowardsTarget(float deltaTime)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTarget.Value, deltaTime * fastRotateSpeed);

        if (Quaternion.Angle(transform.rotation, rotateTarget.Value) < 1)
        {
            rotateTarget = null;
        }
    }

    void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;

        if (Input.GetKey("left shift"))
        {
            rigidBody.AddRelativeForce(Vector3.forward * thrustFactor * 1);
        }

        if (Input.GetKey("space"))
        {
            rigidBody.drag = 1.0f;
        }
        else
        {
            rigidBody.drag = 0f;
        }

        if (rotateTarget.HasValue)
        {
            rotateTowardsTarget(deltaTime);
        }
        else
        {
            //if (Input.GetKeyUp("s"))
            //{
            //    if (Time.time - last_s_up < 0.5f)
            //    {
            //        rotateTarget = Quaternion.LookRotation(-transform.forward, Vector3.up);

            //        last_s_up = -100;
            //    }

            //    last_s_up = Time.time;
            //}

            handleStandardRotations();
        }
    }

    void handleStandardRotations()
    {
        var deltaTime = Time.fixedDeltaTime;

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

        if (transform.position.y < -7)
        {
            var updatedPos = transform.position;
            var updatedVel = rigidBody.velocity;
            updatedPos.y = -7;
            if (updatedVel.y < 1)
            {
                updatedVel.y = 1;
            }

            transform.position = updatedPos;
            rigidBody.velocity = updatedVel;
        }
    }

    void updateIndicator()
    {
        var basePosition = vel_indicator.transform.position;
        var velocityDirection = rigidBody.velocity.normalized;
        vel_indicator.transform.LookAt(velocityDirection + basePosition);

        particles.transform.position = cameraGameObject.transform.position + (velocityDirection * 20);
        particles.transform.LookAt(cameraGameObject.transform);

        float speed = rigidBody.velocity.magnitude;
        float angle = Vector3.Angle(velocityDirection, transform.forward);

        var emissionModule = particles.emission;

        emissionModule.rateOverTime = Mathf.Lerp(0, 100, (speed - 5) / 15);

        float velocityPoints = Mathf.Lerp(0, 10, Mathf.Clamp(speed / 20.0f, 0, 1));
        float anglePoints = Mathf.Lerp(0, 10, Mathf.Clamp(angle / 90.0f, 0, 1));
        float score = velocityPoints * anglePoints;

        layer1.volume = Mathf.Clamp01(score / 10);
        layer2.volume = Mathf.Clamp01(score / 25);
        layer3.volume = Mathf.Clamp01(score / 50);

        velocity_text.SetText(
            noDecimalFormat(speed) + " m/s" + 
            "\n" + noDecimalFormat(angle) + " degrees" +
            "\nvelocity points: " + noDecimalFormat(velocityPoints) +
            "\nangle points: " + noDecimalFormat(anglePoints) +
            "\nscore: " + noDecimalFormat(score)
        );

        vel_indicator.SetActive(speed >= 0.1);
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

    void OnCollisionEnter(Collision collision)
    {
        if (rigidBody.velocity.magnitude > 3)
        {
            Debug.LogError("YOU DIED");
        }
    }

}
