using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JankenAnimation : MonoBehaviour
{
    // Animator コンポーネント
    private Animator animator;

    private const string key_janken = "janken";
    private bool is_janken = false;
    private int state_hash = -1;
    private int state_count = 0;
    public bool start_janken = false;

    private const string key_gu = "gu";
    private const string key_choki = "choki";
    private const string key_pa = "pa";

    private IEnumerator SwitchHand()
    {
        yield return new WaitForSeconds(2f);

        JankenPonText.instance.ChangeText("ぽん！");

        yield return new WaitForSeconds(0.2f);

        GetHand hand = GetComponent<GetHand>();
        switch (hand.result.Hand)
        {
            case 1:
                animator.SetBool(key_gu, false);
                animator.SetBool(key_choki, true);
                animator.SetBool(key_pa, false);
                break;
            case 2:
                animator.SetBool(key_gu, false);
                animator.SetBool(key_choki, false);
                animator.SetBool(key_pa, true);
                break;
            case 3:
                animator.SetBool(key_gu, true);
                animator.SetBool(key_choki, false);
                animator.SetBool(key_pa, false);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_janken)
        {
            int tmp = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            if (tmp != state_hash)
            {
                state_count += 1;
            }
            if (state_count == 2)
            {
                animator.SetBool(key_gu, false);
                animator.SetBool(key_choki, false);
                animator.SetBool(key_pa, false);
                JankenPonText.instance.ChangeText("You Win!");
            }
            else if (state_count == 3)
            {
                JankenPonText.instance.ChangeText("");
                SceneManager.LoadScene("Retry");
            }
            state_hash = tmp;
        }


        GetHand hand = GetComponent<GetHand>();
        if (hand.result.Hand > 0)
        {
            if (!is_janken)
            {
                state_count = 0;
                StartCoroutine(SwitchHand());
                JankenPonText.instance.ChangeText("じゃーんけーん");
                state_hash = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
                is_janken = true;
                animator.SetBool(key_janken, true);
            }
            start_janken = false;
        }
        else
        {
            animator.SetBool(key_janken, false);
        }
    }
}
