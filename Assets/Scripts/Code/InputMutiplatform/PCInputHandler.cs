using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    /*[SerializeField] private KeyCode moveForwardKey;
    [SerializeField] private KeyCode moveBackwardKey;
    [SerializeField] private KeyCode runKey;*/
    [SerializeField] private KeyCode jumpKey;
  //  [SerializeField] private int shootMouseButton;

    private void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {
            InputEvents.moveLeftEvent.Invoke();
        }
        if (Input.GetKey(moveRightKey))
        {
            InputEvents.moveRightEvent.Invoke();
        }
       /* if (Input.GetKey(moveForwardKey))
        {
            InputEvents.moveForwardEvent.Invoke();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            InputEvents.moveBackwardEvent.Invoke();
        }
        if (Input.GetKeyDown(runKey))
        {
            InputEvents.runEvent.Invoke();
        }*/
        if (Input.GetKeyDown(jumpKey))
        {
            InputEvents.jumpEvent.Invoke();
        }
        /*if (Input.GetMouseButton(shootMouseButton))
        {
            InputEvents.shootEvent.Invoke();
        }*/
    }
}
