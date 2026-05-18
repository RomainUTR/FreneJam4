using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.OnInteractEvent += HandleInteract;
    }

    void OnDisable()
    {
        playerInput.OnInteractEvent -= HandleInteract;      
    }

    void HandleInteract()
    {
        Debug.Log("I tried to interact");
    }
}
