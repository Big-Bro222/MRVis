using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;
using System;

[Serializable]
public class Screen
{
    public string id;
    public string IPAddress;

    public int h_unit_size;
    public int v_unit_size;
    
    public Vector2 up_left_start;

    [SerializeField]
    private float h_world_size;
    [SerializeField]
    private float v_world_size;

    [SerializeField]
    private Vector3 position = Vector3.zero;

    [SerializeField]
    private Vector4 local_lurd;
    [SerializeField]
    private Vector4 global_lurd;

    public Screen(string name, string ip, Vector2 startPos)
    {
        this.id = name;
        this.IPAddress = ip;

        this.h_unit_size = 0;
        this.v_unit_size = 0;

        this.up_left_start = startPos;
    }

    public override string ToString()
    {
        return id + " : ip/" + IPAddress + " - dimension " + h_unit_size + "/" + v_unit_size + " start : " + up_left_start.ToString() + " position :" + local_lurd.ToString("F4") + "-" + global_lurd.ToString("F4");
    }

    public void Deploy(string key, string password)
    {
        Debug.Log("Deploying build to screen " + id + "...");
        ConnectionInfo ConnNfo = new ConnectionInfo(IPAddress, 22 , "wild",
            new AuthenticationMethod[]{
                
                // Key Based Authentication (using keys in OpenSSH Format)
                new PrivateKeyAuthenticationMethod("wild", new PrivateKeyFile[]{
                    new PrivateKeyFile(key, password)
                })
            }
        );
        Debug.Log("a");
        // Upload A File
        using (var sftp = new SftpClient(ConnNfo))
        {
            string uploadfn = Path.Combine(Application.dataPath, "Path/To/Exec");
            Debug.Log("b");
            sftp.Connect();
            sftp.ChangeDirectory("/media/ssd/Demos");
            Debug.Log("c");
            using (var uplfileStream = System.IO.File.OpenRead(uploadfn))
            {
                //sftp.UploadFile(uplfileStream, uploadfn, true);   
            }
            Debug.Log("d");
            sftp.Disconnect();
        }
        Debug.Log("Deployment Successful");
    }

    public void Start(string key, string password)
    {
        Debug.Log("Connecting to screen " + id + " ...");
        ConnectionInfo ConnNfo = new ConnectionInfo(IPAddress, 22, "wild",
            new AuthenticationMethod[]{
                
                // Key Based Authentication (using keys in OpenSSH Format)
                new PrivateKeyAuthenticationMethod("wild", new PrivateKeyFile[]{
                    new PrivateKeyFile(key, password)
                })
            }
        );

        // Execute (SHELL) Commands
        using (var sshclient = new SshClient(ConnNfo))
        {
            sshclient.Connect();
            // quick way to use ist, but not best practice - SshCommand is not Disposed, ExitStatus not checked...

            string screen_width = (id.Contains("A"))? "7680" : "6720";
            string screen_height = "960";

            string resolArgs = "-popupwindow -screen-fullscreen 0 -screen-width " + screen_width + " -screen-height " + screen_height;
            string args = " -l " + local_lurd.x + " " + local_lurd.y + " " + local_lurd.z + " " + local_lurd.w + " -g " + global_lurd.x + " " + global_lurd.y + " " + global_lurd.z + " " + global_lurd.w;
            Debug.Log(sshclient.CreateCommand("DISPLAY=:0 /home2/qi/Wall/WallApp.x86_64 " + resolArgs + args).Execute());            
            sshclient.Disconnect();
            sshclient.Dispose();
        }
    }

    public void SetDimensions(float h_world_tot, float v_world_tot, int h_unit_tot, int v_unit_tot)
    {
        h_world_size = (float)h_unit_size * h_world_tot / (float)h_unit_tot;
        v_world_size = (float)v_unit_size * v_world_tot / (float)v_unit_tot;

        Vector2 up_left_start_real = new Vector2(- up_left_start.x * v_world_tot / v_unit_tot, up_left_start.y * h_world_tot / h_unit_tot) + new Vector2(v_world_tot / 2f, - h_world_tot / 2f);
        Vector2 down_right_end_real = new Vector2(up_left_start_real.x - v_world_size, up_left_start_real.y + h_world_size);

        Debug.Log(new Vector2(v_world_tot / 2f, h_world_tot / 2f));
        Debug.Log(up_left_start_real.ToString());

        local_lurd = new Vector4(up_left_start_real.y, up_left_start_real.x, down_right_end_real.y, down_right_end_real.x);
        global_lurd = new Vector4(- h_world_tot / 2f, v_world_tot/2f, h_world_tot / 2f, -v_world_tot / 2f);
    }
}
