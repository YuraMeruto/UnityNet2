using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text;


public class Client : MonoBehaviour {
    [SerializeField]
    private MyAddress my;
    private const int portnumber = 9999;
    void Update()
    {
        TcpClient client = new TcpClient(" 192.168.0.7",portnumber);
        string str = "hogehoge";
        byte[] tmp = Encoding.UTF8.GetBytes(str);
        NetworkStream stream = client.GetStream();
        stream.Write(tmp,0,tmp.Length);
        client.Close();
    }

}
