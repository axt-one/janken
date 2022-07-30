using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JankenHand : MonoBehaviour
{
    private Animator animator;

    private const string key_gu = "gu";
    private const string key_choki = "choki";
    private const string key_pa = "pa";


    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetHand hand = GetComponent<GetHand>();
        switch (hand.result.Hand)
        {
            case 1:
                animator.SetBool(key_gu, true);
                animator.SetBool(key_choki, false);
                animator.SetBool(key_pa, false);
                break;
            case 2:
                animator.SetBool(key_gu, false);
                animator.SetBool(key_choki, true);
                animator.SetBool(key_pa, false);
                break;
            case 3:
                animator.SetBool(key_gu, false);
                animator.SetBool(key_choki, false);
                animator.SetBool(key_pa, true);
                break;
        }
        Vector3 pos = transform.position;
        pos.x = 0.5f - hand.result.PosX;
        pos.y = 1.8f - hand.result.PosY;

        transform.position = pos;
    }
}
