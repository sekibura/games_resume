using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static event Action OnJump;
    public static event Action OnAttack;
    public static event Action OnLeft;
    public static event Action OnRight;
    public static event Action OnUp;
    public static event Action OnDown;
}
