using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField]
    GameObject Door;
    [SerializeField]
    GameObject DoorOpenerText;

    bool canOpened = true;

    private SpriteRenderer doorSprite;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        doorSprite.color = Color.green;
            Door.SetActive(false);
            DoorOpenerText.SetActive(false);
            canOpened = false;
    }

    public void ShowText()
    {
        if(canOpened)
        DoorOpenerText.SetActive(true);
    }

    public void HideText()
    {
        DoorOpenerText.SetActive(false);
    }
}
