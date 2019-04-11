using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarSelectButton : MonoBehaviour, IPointerClickHandler
{
    public Image charSprite;
    public CharacterObject character;

    public void OnPointerClick(PointerEventData eventData)
    {
        MenuSelection.instance.playerSelectAvatar.sprite = character.front;
        MenuSelection.instance.userCharacter = character;
    }

    private void OnValidate()
    {
        charSprite.sprite = character.front;
    }
}
