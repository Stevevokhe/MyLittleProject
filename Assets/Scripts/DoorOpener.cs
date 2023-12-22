using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField] GameObject Door;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed;
    [SerializeField]
    GameObject DoorOpenerText;

    bool canOpened = true;
    bool canMove = false;

    private SpriteRenderer doorSprite;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove)
        {           
           MoveDoor(endPoint);            
        }
    }

    public void OpenDoor()
    {
        doorSprite.color = Color.green;
        canMove = true;
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

    private void MoveDoor(Transform target)
    {
        Door.transform.position = Vector3.MoveTowards(Door.transform.position,
            target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(Door.transform.position, target.position) < 0.1f)        
            canMove = false;
    }
}
