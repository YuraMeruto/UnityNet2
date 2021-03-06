﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.IO;
public class Client2 : MonoBehaviour
{


    private Socket sock;
    private Thread acceptthread;
    private IPAddress server_ip;
    private int portnum = 9999;
    private bool Is_Login = false;
    private IPEndPoint ep;
    private Socket m_lisner;
    void Start()
    {
        server_ip = IPAddress.Parse("192.168.0.7");
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ep = new IPEndPoint(server_ip, portnum);
        acceptthread = new Thread(SendServer);

        acceptthread.Start();
    }

    void SendServer()
    {

        while (true)
        {
            Debug.Log("接続中");
            while (sock.Connected == false)
            {
                sock.Connect(ep);
            }
            Debug.Log("繋がりました");
            Socket recvsock = sock;
            byte[] bytes = Encoding.UTF8.GetBytes("ぐはははは");
            sock.Send(bytes, bytes.Length, SocketFlags.None);
            Debug.Log("サーバーにメッセージを送りました");


            byte[] recvbyte = new byte[100];
            Debug.Log("ただ今応答待ち中");
            int retrecv = sock.Receive(recvbyte);
            string recvstr = Encoding.UTF8.GetString(recvbyte, 0, retrecv);

            Is_Login = bool.Parse(recvstr);
            Debug.Log(recvstr + "をいただきました");

        }
    }
    void Update()
    {
        if (Is_Login)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameStage");
        }
    }
}
