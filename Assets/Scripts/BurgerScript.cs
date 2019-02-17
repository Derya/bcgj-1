using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerScript : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 6.0f * 5 * Time.deltaTime, 0);
    }

}
