using UnityEngine;
using System.Runtime.InteropServices;

public class StartMediapipe : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Main();

    void Start()
    {
        Main();
    }
}