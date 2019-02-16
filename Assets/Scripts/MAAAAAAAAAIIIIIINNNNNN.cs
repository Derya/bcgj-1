using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAAAAAAAAAIIIIIINNNNNN : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void handleControls()
    {
        bool holdingLeft = Input.GetKey(App.ROTATE_LEFT_KEY);
        bool holdingRight = Input.GetKey(App.ROTATE_RIGHT_KEY);
        bool holdingGas = Input.GetKey(App.ACCELERATE_KEY);
        bool holdingReverse = Input.GetKey(App.DECERATE_KEY);
    }

}
