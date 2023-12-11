using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    [SerializeField]
    GameObject Door;
    [SerializeField]
    GameObject DoorOpenerText;

    bool canOpened = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && canOpened)
        {
            Door.SetActive(false);
            DoorOpenerText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canOpened = true;
            DoorOpenerText.SetActive(true);
        }
    }
}
