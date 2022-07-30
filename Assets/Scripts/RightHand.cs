using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetHand hand = GetComponent<GetHand>();

        if (hand.result.IsLeft)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
