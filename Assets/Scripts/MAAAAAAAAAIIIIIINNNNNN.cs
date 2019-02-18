using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    GameObject epicTextGameObjec;
    [SerializeField]
    GameObject cameraGameObject;
    [SerializeField]
    GameObject particlesGameObject;
    [SerializeField]
    GameObject otherParticlesGameObject;

    [SerializeField]
    GameObject bunBottom;
    [SerializeField]
    GameObject cheese;
    [SerializeField]
    GameObject patty;
    [SerializeField]
    GameObject lettuce;
    [SerializeField]
    GameObject bunTop;

    [SerializeField]
    GameObject bunBottomDisplay;
    [SerializeField]
    GameObject cheeseDisplay;
    [SerializeField]
    GameObject pattyDisplay;
    [SerializeField]
    GameObject lettuceDisplay;
    [SerializeField]
    GameObject bunTopDisplay;
    [SerializeField]
    LineRenderer predictionLineRenderer;

    int hasBunBottom;
    int hasCheese;
    int hasPatty;
    int hasLettuce;
    int hasBunTop;

    bool won = false;
    GameObject currTarget = null;

    [SerializeField]
    AudioSource layer1;
    [SerializeField]
    AudioSource layer2;
    [SerializeField]
    AudioSource layer3;
    [SerializeField]
    AudioSource layer3_LYRICS;

    readonly AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    readonly AcceleratableValue pitch = new AcceleratableValue(1.5f, 0.7f);
    readonly AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);

    Rigidbody rigidBody;
    TextMeshPro velocity_text;
    TextMeshPro epic_text;
    ParticleSystem particles;
    ParticleSystem otherParticles;
    Vector3 epicBasePos;
    float jitterAmount = 0.1f;

    double TOTAL_SCORE = 0;
    double TOTAL_TIME = 0;
    double FINAL_SCORE;

    void Start()
    {

        currTarget = bunBottom;

        rigidBody = GetComponent<Rigidbody>();
        velocity_text = textGameObjec.GetComponent<TextMeshPro>();
        epic_text = epicTextGameObjec.GetComponent<TextMeshPro>();
        particles = particlesGameObject.GetComponent<ParticleSystem>();
        otherParticles = otherParticlesGameObject.GetComponent<ParticleSystem>();

        epicBasePos = epicTextGameObjec.transform.localPosition;

        bunBottomDisplay.SetActive(false);
        cheeseDisplay.SetActive(false);
        pattyDisplay.SetActive(false);
        lettuceDisplay.SetActive(false);
        bunTopDisplay.SetActive(false);
    }

    void Update()
    {
        won = true;
        updateIndicator();

        var newPos = epicBasePos;
        newPos.x = newPos.x + Random.Range(-jitterAmount, jitterAmount);
        newPos.y = newPos.y + Random.Range(-jitterAmount, jitterAmount);
        newPos.z = newPos.z + Random.Range(-jitterAmount, jitterAmount);
        epicTextGameObjec.transform.localPosition = newPos;

        if (won && (Input.GetKeyUp("enter") || Input.GetKeyUp("return")))
        {
            SceneManager.LoadScene(0);
        }
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

        var emissionSubsystem = otherParticles.emission;
        if (Input.GetKey("left shift"))
        {
            rigidBody.AddRelativeForce(Vector3.forward * thrustFactor);
            emissionSubsystem.rateOverTime = 100;


            drawLine(transform.position, rigidBody.velocity, transform.forward.normalized * thrustFactor * 1f);
        }
        else
        {
            emissionSubsystem.rateOverTime = 0;
            drawLine(transform.position, rigidBody.velocity, Vector3.zero);
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

    void drawLine(Vector3 position, Vector3 velocity, Vector3 acceleration) {
        LineRenderer line = predictionLineRenderer;

        int verts = 2000;
        float timeStep = 0.01f;

        line.positionCount = verts;

        var velTemp = velocity;
        var positionTemp = position;
        for (var i = 0; i < verts; i++)
        {
            velTemp += acceleration * timeStep;
            positionTemp += velTemp * timeStep;

            line.SetPosition(i, positionTemp);
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

        particles.transform.position = cameraGameObject.transform.position + (velocityDirection * 20);
        particles.transform.LookAt(cameraGameObject.transform);

        float speed = rigidBody.velocity.magnitude;
        float angle = Vector3.Angle(velocityDirection, transform.forward);

        var emissionModule = particles.emission;

        emissionModule.rateOverTime = Mathf.Lerp(0, 100, (speed - 5) / 15);

        float velocityPoints = Mathf.Lerp(0, 10, Mathf.Clamp(speed / 40.0f, 0, 1));
        float anglePoints = Mathf.Lerp(0, 10, Mathf.Clamp(angle / 90.0f, 0, 1));
        float score = velocityPoints * anglePoints;

        TOTAL_SCORE += score * Time.deltaTime;
        TOTAL_TIME += Time.deltaTime;

        jitterAmount = Mathf.Lerp(0, 0.1f, (score - 30) / 70);

        if (!won)
        {
            if (score > 90)
            {
                epic_text.text = "AAAAAAAAAAAAAAAAAA";
            }
            else if (score > 70)
            {
                epic_text.text = "INCOMPREHENSIBLE DRIFT";
            }
            else if (score > 50)
            {
                epic_text.text = "HUGE DRIFT";
            }
            else if (score > 30)
            {
                epic_text.text = "BIG DRIFT";
            }
            else if (score > 10)
            {
                epic_text.text = "NICE DRIFT";
            }
            else
            {
                epic_text.text = "";
            }

            layer1.volume = Mathf.Clamp01(score / 10);
            layer2.volume = Mathf.Clamp01(score / 25);
            layer3.volume = Mathf.Clamp01(score / 50);

            if (score >= 90)
            {
                layer3.volume = 0;
                layer3_LYRICS.volume = 1;
            }
            else
            {
                layer3_LYRICS.volume = 0;
            }
        }
        else
        {
            layer1.volume = 1;
            layer2.volume = 1;
            layer3.volume = 0;
            layer3_LYRICS.volume = 1;
        }

        velocity_text.SetText(
            noDecimalFormat(speed) + " m/s" + 
            "\n" + noDecimalFormat(angle) + " degrees" +
            "\nvelocity points: " + noDecimalFormat(velocityPoints) +
            "\nangle points: " + noDecimalFormat(anglePoints) +
            "\nscore: " + noDecimalFormat(score)
        );

        //vel_indicator.transform.LookAt(velocityDirection + basePosition);

        if (currTarget == null)
        {
            vel_indicator.SetActive(false);
        }
        else
        {
            vel_indicator.SetActive(true);
            vel_indicator.transform.LookAt(currTarget.transform);
        }

        //vel_indicator.SetActive(speed >= 0.1);
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

    void updateNextTarget() {
        if (hasCheese == 0) {
            currTarget = cheese;
        } else if (hasPatty == 0) {
            currTarget = patty;
        } else if (hasLettuce == 0) {
            currTarget = lettuce;
        } else if (hasBunTop == 0) {
            currTarget = bunTop;
        }
    }

    public void pickupTarget(Target target)
    {
        switch(target)
        {
            case Target.bottombun:
                hasBunBottom = 1;
                bunBottomDisplay.SetActive(true);
                break;

            case Target.cheese:
                hasCheese = 1;
                cheeseDisplay.SetActive(true);
                break;

            case Target.patty:
                hasPatty = 1;
                pattyDisplay.SetActive(true);
                break;

            case Target.lettuce:
                hasLettuce = 1;
                lettuceDisplay.SetActive(true);
                break;

            case Target.topbun:
                hasBunTop = 1;
                bunTopDisplay.SetActive(true);
                break;
        }

        if (hasBunBottom + hasCheese + hasLettuce + hasPatty + hasBunTop == 5)
        {
            won = true;
            currTarget = null;
            FINAL_SCORE = (TOTAL_SCORE / TOTAL_TIME) + Mathf.Clamp((60f - (float) TOTAL_TIME), 0, 60);
            epic_text.text = "YOU WIN! SCORE: " + FINAL_SCORE.ToString("#.00") + " PRESS ENTER TO PLAY AGAIN";
        }
        else
        {
            updateNextTarget();
        }
    }

    string noDecimalFormat(double x)
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
