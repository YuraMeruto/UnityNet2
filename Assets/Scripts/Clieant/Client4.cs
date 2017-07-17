using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.IO;

public class Client4 : MonoBehaviour {

    private Socket sock;
    private Thread thread;
    private IPAddress server_ip;
    private int client_send_portnum = 9999;
    private int server_send_portnum = 9998;
    private bool Is_send = false;
    private IPEndPoint ep;
    private IPEndPoint ep2;
    private Socket sendsock;//サーバーにメッセージを送るもの
    private Socket recvsock;//サーバーからのメッセージのもの
    private int Number;

    void Start()
    {
        server_ip = IPAddress.Parse("10.40.0.10");
        sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        recvsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        thread = new Thread(Send);
        ep = new IPEndPoint(server_ip,client_send_portnum);
        thread.Start();
    }

    public void Ini()
    {

    }

    public void Send()
    {
        sendsock.Connect(ep);
        if(sendsock.Connected)
        {
            byte[] sendmessage = Encoding.UTF8.GetBytes(Number.ToString());
            sendsock.Send(sendmessage,sendmessage.Length,SocketFlags.None);
        }
    }
}
