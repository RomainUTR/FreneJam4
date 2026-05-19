using System.Collections;
using UnityEngine;

public class RandomInputManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Header("Input")]

    [Header("Output")]
    public RSE_ToggleInputInversion ToggleInputInversion;

    private void Start()
    {
        StartCoroutine(WaitForSwapInput());
    }

    private IEnumerator WaitForSwapInput()
    {
        Debug.Log("Starty coroutine");
        yield return new WaitForSeconds(10);

        Debug.Log("Input was swapped");
        ToggleInputInversion?.RaiseEvent();
    }
}