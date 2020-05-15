using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{
    public static Vector3 wallPosition { get; set; }
    public static Quaternion wallRotation { get; set; }
    public static byte EXTURDE_WINDOW_EVENT = 0;
    public static byte DESTROY_WINDOW_EVENT = 1;
    public static byte DISABLE_WINDOW_TRANSFORM = 2;
    public static byte ENABLE_WINDOW_TRANSFORM = 3;
    public static byte SHOW_HIERARCHY = 4;
    public static byte HIDE_HIERARCHY = 5;

}
