using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    //event raised from HoloLens
    public static Vector3 wallPosition { get; set; }
    public static Quaternion wallRotation { get; set; }
    public static byte INSTANTIATE_EVENT = 0;
    public static byte DESTROY_WINDOW_EVENT = 1;
    public static byte SCROLLVIEW_LOG_EVENT = 2;
    public static byte CHANGE_DIMENSION_EVENT = 3;
    public static byte LOAD_LEVEL_EVENT = 4;
    public static byte SET_FOCUS = 5;

    //event raised from user monitor
    public static byte DOUBLE_CLICKED = 6;
    public static byte RIGHT_BTN_CLICKED = 7;
    public static byte UI_BTN_CLICKED = 8;

    public static byte SCALE = 9;
    public static byte NEXT_TASK = 10;

}
