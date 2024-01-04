using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation
{
    public int touchId;
    public GameObject currentLine;

    public TouchLocation(int newTouchId, GameObject newLine)
    {
        touchId = newTouchId;
        currentLine = newLine;
    }
}
