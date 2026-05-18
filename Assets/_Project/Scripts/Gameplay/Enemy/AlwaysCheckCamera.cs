using UnityEngine;

public class AlwaysCheckCamera : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Header("Input")]

    //[Header("Output")]


    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}