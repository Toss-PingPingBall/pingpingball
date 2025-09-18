using UnityEngine;
using System;

public class UI_Button : MonoBehaviour
{
    public Action onClick;

    public void click() { onClick?.Invoke(); }
}
