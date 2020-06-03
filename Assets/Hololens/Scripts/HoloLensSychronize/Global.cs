using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{
    public static Vector3 wallPosition { get; set; }
    public static Quaternion wallRotation { get; set; }
    public static byte INSTANTIATE_EVENT = 0;
    public static byte DESTROY_WINDOW_EVENT = 1;
    public static byte SCROLLVIEW_LOG_EVENT = 2;
    public static byte CHANGE_DIMENSION_EVENT = 3;
    public static byte LOAD_LEVEL_EVENT = 4;
    //public static byte ENABLE_WINDOW_TRANSFORM = 3;
    //public static byte SHOW_HIERARCHY = 4;
    //public static byte HIDE_HIERARCHY = 5;

}
