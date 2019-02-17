using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript2 : MonoBehaviour
{
    [SerializeField]
    GameObject HOW_DOES_UNITY_NOT_SUPOIRT_THIS_REEEEE;
    [SerializeField]
    Target target;
    [SerializeField]
    float rotationsPerMinute = 10.0f;

    MAAAAAAAAAIIIIIINNNNNN playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<MAAAAAAAAAIIIIIINNNNNN>();
    }

    void Update()
    {
        transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);

        if (Vector3.Distance(playerScript.transform.position, transform.position) < 15)
        {
            playerScript.pickupTarget(target);
            Destroy(HOW_DOES_UNITY_NOT_SUPOIRT_THIS_REEEEE);
        }
    }

}
