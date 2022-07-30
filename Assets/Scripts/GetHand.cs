using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[System.Serializable]
public class JankenInfo
{
    public int Hand;
    public bool IsLeft;
    public float PosX;
    public float PosY;
}

public class GetHand : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetHandInfo();
    public JankenInfo result;


    // Update is called once per frame
    void Update()
    {
        string json = GetHandInfo();
        //string json = "{\"Hand\":2,\"IsLeft\":true,\"PosX\":0.8,\"PosY\":0.6}";
        result = JsonUtility.FromJson<JankenInfo>(json);
        Debug.Log(result.Hand);
    }
}
