using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallView : VRSystem
{
    public float nearPlane = 0.75f;
    public float farPlane = 1.25f;

    // Start is called before the first frame update
    protected override void Start()
    {
        string[] args = Environment.GetCommandLineArgs();
        string test = "";
        foreach(string a in args)
        {
            test += a;
        }
        Debug.Log(test);
#if UNITY_EDITOR
        args = ("-l " + "-1" + " " + "1" + " " + "2" + " " + "-2" + " -g " + "-2" + " " + "2"+ " " + "2" + " " + "-2").Split(' ');
        Debug.Log(args[0] + args[1] + args[2] + args[3] + args[4] + args[5]);
#endif

        Dictionary<string, string[]> options = OptionParse(args);        
        foreach(var d in options.Keys)
        {
            Debug.Log(d.ToString());
        }

        if (options["-l"] != null && options["-g"] != null )
        {

            Debug.Log("Wall world coordinates l-u-r-d | l-u-r-d Tot : " + float.Parse(options["-l"][3]) + " | " + options["-g"][3]);

            ldc = new Vector2(float.Parse(options["-l"][0]), float.Parse(options["-l"][3]));
            luc = new Vector2(float.Parse(options["-l"][0]), float.Parse(options["-l"][1]));
            rdc = new Vector2(float.Parse(options["-l"][2]), float.Parse(options["-l"][3]));
            ldcTotal = new Vector2(float.Parse(options["-g"][0]), float.Parse(options["-g"][3]));
            lucTotal = new Vector2(float.Parse(options["-g"][0]), float.Parse(options["-g"][1]));
            rdcTotal = new Vector2(float.Parse(options["-g"][2]), float.Parse(options["-g"][3]));
        }
        else
            Debug.Log("Debug: using Unity editor parameters");

        Debug.Log("VRSystem parameters - ldc: " + ldc + " luc " + luc + " rdc " + rdc);
        Debug.Log("VRSystem parameters - ldcTotal: " + ldcTotal + " lucTotal " + lucTotal + " rdcTotal " + rdcTotal);

    }

    void LateUpdate()
    {

        if (disabled || !cam)
            return;

        cam.transform.localPosition = gameObject.transform.localPosition;
        cam.transform.localRotation = gameObject.transform.localRotation;
        cam.transform.localScale = gameObject.transform.localScale;

        // on the 2D horizontal and vertical planes
        Vector2 leftPoint = new Vector2(ldc.x, ldc.z);
        Vector2 rightPoint = new Vector2(rdc.x, rdc.z);
        Vector2 bottomPoint = new Vector2(ldc.y, ldc.z);
        Vector2 topPoint = new Vector2(luc.y, luc.z);

        cam.projectionMatrix = OrthographicOffCenter(leftPoint.x, rightPoint.x, bottomPoint.x, topPoint.x, nearPlane, farPlane);
    }

    Dictionary<string, string[]> OptionParse(string[] raw_options)
    {
        Dictionary<string, string[]> output = new Dictionary<string, string[]>();
        string option = "";
        List<string> opt_args = new List<string>();

        for(int i = 0; i < raw_options.Length; i++)
        {
            if (raw_options[i].Contains("-") && !raw_options[i].Any(char.IsDigit))
            {
                if(option != "")
                {
                    output.Add(option, opt_args.ToArray());
                    opt_args.Clear();
                }
                option = raw_options[i];
            } else
            {
                opt_args.Add(raw_options[i]);
            }
        }
        output.Add(option, opt_args.ToArray());

        return output;
    }
}
