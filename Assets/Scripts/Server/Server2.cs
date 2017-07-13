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
public class Server2 : MonoBehaviour
{

    private IPEndPoint ipend;
    private IPEndPoint ipend2;
    private Socket sock;
    private Socket m_lisner;
    private Socket sendsocket;
    private Thread aceptthread;
    private bool threadloop = true;
    private IPAddress myaddress;
    private int portnumber = 9999;//くらいあんとから受信用
    private int portnumber2 = 9998;//クライアントへの送信用
    private bool Is_Login = false;
    private NetworkStream ns;
    private bool ret = false;
    void Start()
    {
        myaddress = IPAddress.Parse("10.40.0.10");
        ipend = new IPEndPoint(myaddress, Int32.Parse(portnumber.ToString()));
        ipend2 = new IPEndPoint(myaddress, Int32.Parse(portnumber2.ToString()));
        TcpListener server = new TcpListener(myaddress, portnumber);
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        sendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        aceptthread = new Thread(ClientAccept);
        aceptthread.Start();
    }

    void ClientAccept()
    {
        Debug.Log("接続町");
        sock.Bind(ipend);
        sock.Listen(10);


        while (true)
        {
            m_lisner = sock.Accept();
            if(m_lisner.Connected)
            {
                sendsocket.Connect(ipend2);
            }


                if (m_lisner.Connected && sendsocket.Connected)
                {
                    Debug.Log("受信します");
                    byte[] recv = new byte[1000];
                    m_lisner.Receive(recv);
                    string msg = Encoding.UTF8.GetString(recv);
                    Debug.Log(msg);
                    ret = true;
                }

            if (ret && sendsocket.Connected)
            {
                byte[] sendbyte = Encoding.UTF8.GetBytes("1");
                Debug.Log("クライアント" + sendsocket.RemoteEndPoint + "がつながりました");
                sendsocket.Send(sendbyte, sendbyte.Length, SocketFlags.None);
                Debug.Log("クライアントに送信しました");
                ret = false;
            }

        }
            /*
            //if (sock != null && sock.Poll(0, SelectMode.SelectRead))
            //{
                m_lisner = sock.Accept();
                Debug.Log("接続してきた");
                byte[] buffer = new byte[10000];
                //while (m_lisner.Poll(0, SelectMode.SelectRead))
               // {
                    int recvsize = m_lisner.Receive(buffer, buffer.Length, SocketFlags.None);
                    if (recvsize == 0)
                    {
                        //切断
                        Debug.Log("切断されました");
                        m_lisner.Close();
                    }
                    ret = true;
                    MemoryStream ms = new MemoryStream();
                    Is_Login = true;
                    ms.Write(buffer, 0, recvsize);
                    string recvstr = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                    ms.Close();
                    Debug.Log(recvsize);
                //}

            //}
            */
        
    }
    void ClientSend()
    {
        Debug.Log("クライアントに送信中");
        ipend = new IPEndPoint(myaddress, Int32.Parse(portnumber2.ToString()));
        byte[] sendbyte = Encoding.UTF8.GetBytes(Is_Login.ToString());
        ns.Write(sendbyte, 0, sendbyte.Length);
    }
}
