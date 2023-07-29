using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    public KeyCode timeSlow = KeyCode.LeftShift;
    public KeyCode teleport = KeyCode.Mouse1;

    public KeyCode scaleCloak = KeyCode.Mouse2;

    public KeyCode spellMode = KeyCode.V;
    public KeyCode spell1 = KeyCode.Mouse0;
    public KeyCode spell2 = KeyCode.Mouse2;
    public KeyCode spell3 = KeyCode.Mouse1;
}
