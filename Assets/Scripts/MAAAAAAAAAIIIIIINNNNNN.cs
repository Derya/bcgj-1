using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{
    AcceleratableValue roll = new AcceleratableValue(3.0f, 0.5f);
    AcceleratableValue pitch = new AcceleratableValue(1.0f, 0.5f);
    AcceleratableValue yaw = new AcceleratableValue(1.0f, 0.5f);
    AcceleratableValue throttle = new AcceleratableValue(1.0f, 0.5f);

    void Start()
    {

    }

    void Update()
    {
        handleControls();
    }


    float curShipSpeed = 0.0f;
    float mouse_rotate = 0.5f;
    float dist_fr_ship = 2.85f;
    int step = 2;
    float current_speed = 0.0f;
    float maxSpeed = 100f;
    int agility = 30;

   

    void handleControls()
    {
        var deltaTime = Time.deltaTime;

        var rollRight = Input.GetKey("d");
        var rollLeft = Input.GetKey("a");

        if (rollRight && !rollLeft) roll.decrease(deltaTime);
        else if (rollLeft && !rollRight) roll.increase(deltaTime);
        else roll.equalize(deltaTime);

        var pitchUp = Input.GetKey("s");
        var pitchDown = Input.GetKey("w");

        if (pitchUp && !pitchDown) pitch.decrease(deltaTime);
        else if (pitchDown && !pitchUp) pitch.increase(deltaTime);
        else pitch.equalize(deltaTime);

        var yawLeft = Input.GetKey("q");
        var yawRight = Input.GetKey("e");

        if (yawLeft && !yawRight) yaw.decrease(deltaTime);
        else if (yawRight && !yawLeft) yaw.increase(deltaTime);
        else yaw.equalize(deltaTime);

        transform.Rotate(pitch.getCurrentValue(), yaw.getCurrentValue(), roll.getCurrentValue());

        //if ((Input.GetKey("up") || Input.GetKey("w")) && (current_speed < maxSpeed))
        //{
        //    current_speed += step;
        //}
        //else if ((Input.GetKey("down") || Input.GetKey("s")) && (current_speed > 0))
        //{
        //    current_speed -= step;
        //}

        //Vector3 MousePosition = Input.mousePosition;
        //MousePosition.x = (Screen.height / 2) - Input.mousePosition.y;
        //MousePosition.y = -(Screen.width / 2) + Input.mousePosition.x;
        //transform.Rotate(MousePosition * Time.deltaTime * mouse_rotate, Space.Self);

        //curShipSpeed = Mathf.Lerp(curShipSpeed, current_speed, Time.deltaTime * step);
        //transform.position += transform.TransformDirection(Vector3.forward) * curShipSpeed * Time.deltaTime;

    }



}
