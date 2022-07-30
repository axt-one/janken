using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JankenPonText : MonoBehaviour
{
    public static JankenPonText instance;
    [SerializeField]
    private TextMeshProUGUI jankenText;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //jankenText.text = "";
    }

    public void ChangeText(string text)
    {
        jankenText.text = text;
    }
}
