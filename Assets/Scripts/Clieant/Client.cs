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
public class Client : MonoBehaviour {
    [SerializeField]
    private MyAddress my;
    private const int portnumber = 9999;
    private const int portnumber2 = 9998;
    private Socket sock2;
    private Thread sendthread;
    private IPAddress serverip;
    public Text tex;
    private bool Is_Scene=false;
    private Socket sock;
    private IPEndPoint ipend;
    private Socket m_lisner;
    private NetworkStream ns;
    private Thread Acceptthread;
    void Start()
    {
        serverip = IPAddress.Parse("10.40.0.4");
        ipend = new IPEndPoint(serverip, Int32.Parse(portnumber.ToString()));
        sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        sendthread = new Thread(ClientSendServer);
        sendthread.Start();
        Acceptthread = new Thread(ClientAcceptServer_IsLogin);
        Acceptthread.Start();
    }

    void Update()
    {
        if(Is_Scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameStage");
        }
    }
    void ClientSendServer()
    {

        /*
        TcpClient client = new TcpClient(serverip.ToString(), portnumber);
        string str = "hogehoge";
        byte[] tmp = Encoding.UTF8.GetBytes(str);
        NetworkStream stream = client.GetStream();
        stream.Write(tmp,0,tmp.Length);
        Thread AcceptServer = new Thread(ClientAcceptServer_IsLogin);       
        client.Close();
        AcceptServer.Start();
        */
    }

    void ClientAcceptServer_IsLogin()
    {
        Debug.Log("サーバーからの返事を待っています");
        ipend = new IPEndPoint(serverip, Int32.Parse(portnumber2.ToString()));
        sock2.Bind(ipend);
        sock2.Listen(10);
        if(sock2 !=null && sock2.Poll(0,SelectMode.SelectRead))
        {
            m_lisner = sock2.Accept();
            byte[] bytes = new byte[10000];
            while(m_lisner.Poll(0,SelectMode.SelectRead))
            {
                int recvsize = sock2.Receive(bytes, bytes.Length, SocketFlags.None);
                if(recvsize ==0)
                {
                    m_lisner.Close();
                }
                MemoryStream ms = new MemoryStream();
                ms.Write(bytes,0,recvsize);
                string recvstr = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                ms.Close();
                Is_Scene = bool.Parse(recvstr);
            }
        }
    }




}
