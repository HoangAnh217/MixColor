using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class InputEvents
{
    public static UnityEvent moveLeftEvent = new UnityEvent();
    public static UnityEvent moveRightEvent = new UnityEvent();
    //  public static UnityEvent moveForwardEvent;
    //  public static UnityEvent moveBackwardEvent;
    //   public static UnityEvent runEvent;
    public static UnityEvent jumpEvent = new UnityEvent();
    //  public static UnityEvent shootEvent;
}
