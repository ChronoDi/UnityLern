using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void On()
    {
        Time.timeScale = 0;
    }

    public void Off()
    {
        Time.timeScale = 1;
    }
}
