using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Transform player; // Tham chiếu đến người chơi
    private Transform carryPosition; // Vị trí mà hộp sẽ được mang theo
    public float pickUpRange = 2.0f; // Khoảng cách để nhặt hộp

    public bool isCarrying = false;

    private PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }
    private void OnEnable()
    {
        inputActions.PlayerControls.Enable();
        inputActions.PlayerControls.Interaction.started += ctx => PickUpOrDropBox();
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Disable();
        inputActions.PlayerControls.Interaction.started -= ctx => PickUpOrDropBox();
    }

    private void Start()
    {
        player = Player.Instance.transform;
        carryPosition = player.Find("CarryPos");
    }
    void PickUpOrDropBox()
    {
        if (Vector3.Distance(player.position, transform.position) < pickUpRange)
        {
            if (!isCarrying)
            {
                PickUpBox();
            } else
            {
                DropBox();
            }
        }
    }
    void PickUpBox()
    {

        Debug.Log("asdasd");

        isCarrying = true;
        gameObject.transform.DOMove(carryPosition.position,0.2f);
        gameObject.transform.parent = carryPosition;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true; 
        //gameObject.GetComponent<BoxCollider2D>().isTrigger= true; 
    }
    void DropBox()
    {
        Debug.Log("asdasd");
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false; 
        isCarrying = false;
        gameObject.transform.parent = null;
        GetComponent<Rigidbody2D>().isKinematic = false; 
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            gameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            box = null;
        }
    }*/
}
