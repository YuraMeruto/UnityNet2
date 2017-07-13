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
public class Client2 : MonoBehaviour {


    private Socket sock;
    private Thread acceptthread;
    private IPAddress server_ip;
    private int portnum = 9999;
    private int portnum2 = 9998;
    private bool Is_Login = false;
    private IPEndPoint ep;
    private IPEndPoint ep2;
    private Socket recvsock;
    private Socket recvsock_m_lisner;
    void Start()
    {
        server_ip = IPAddress.Parse("10.40.0.10");
        sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        recvsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        recvsock = sock;
        ep = new IPEndPoint(server_ip,portnum);
        ep2 = new IPEndPoint(server_ip, portnum2);
        acceptthread = new Thread(SendServer);
        acceptthread.Start();
    }

     void SendServer()
    {
        recvsock.Bind(ep2);
        recvsock.Listen(10);

        while (true)
        {
            
            Debug.Log("接続中");

            sock.Connect(ep);
            Debug.Log("繋がりました"+sock);
            recvsock_m_lisner = recvsock.Accept();
            Debug.Log(recvsock_m_lisner.Connected);
            if (recvsock_m_lisner.Connected && sock.Connected)
            {
                Debug.Log("繋がりました" + portnum2);
                byte[] bytes = Encoding.UTF8.GetBytes("ぐはははは");
                sock.Send(bytes, bytes.Length, SocketFlags.None);
                Debug.Log("サーバーにメッセージを送りました");
                Debug.Log(sock.RemoteEndPoint);

                if (recvsock_m_lisner.Connected)
                {
                    Debug.Log("ただ今応答待ち中");
                    byte[] recvbyte = new byte[100];
                    int retrecv = recvsock_m_lisner.Receive(recvbyte, recvbyte.Length, SocketFlags.None);
                    string recvstr = Encoding.UTF8.GetString(recvbyte, 0, retrecv);
                    Debug.Log(recvstr + "をいただきました");
                    Is_Login = true;
                }
            }
        
        }
    }
    void Update()
    {
       if(Is_Login)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameStage");
        }
    }
}
