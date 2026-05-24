using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UI_InputIcon : SerializedMonoBehaviour
{
    [Title("References")]
    public PlayerInput playerInput;
    public Image targetImage;

    [Title("Settings")]
    public string actionName;

    [InfoBox("Vide pour un bouton simple. 'up', 'down', 'left' ou 'right' pour un mouvement.")]
    public string compositePart = "";

    [Title("Sprite Library")]
    public Dictionary<string, Sprite> iconLibrary = new Dictionary<string, Sprite>();
    public Sprite missingIconSprite;

    private void Awake()
    {
        if (targetImage == null) targetImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        playerInput.OnInputMapChanged += UpdateIcon;
    }

    private void OnDisable()
    {
        playerInput.OnInputMapChanged -= UpdateIcon;
    }

    private void Start()
    {
        UpdateIcon();
    }

    [Button("Force Update Icon")]
    private void UpdateIcon()
    {
        string currentKey = playerInput.GetActionKeyName(actionName, compositePart);

        if (iconLibrary.ContainsKey(currentKey))
        {
            targetImage.sprite = iconLibrary[currentKey];
        }
        else
        {
            targetImage.sprite = missingIconSprite;
            Debug.LogWarning($"Le sprite pour la touche '{currentKey}' (Partie: {compositePart}) est manquant dans la bibliothèque !");
        }
    }
}