using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
public class Client : MonoBehaviour {
    [SerializeField]
    private MyAddress my;
    private const int portnumber = 9999;
    private Thread sendthread;
    private IPAddress serverip;
    void Start()
    {
        serverip = IPAddress.Parse("10.40.0.20");
        sendthread = new Thread(ClientSendServer);
        sendthread.Start();
    }
    void ClientSendServer()
    {
        Debug.Log("クライアント起動");
        TcpClient client = new TcpClient("10.40.0.20", portnumber);
        string str = "hogehoge";
        byte[] tmp = Encoding.UTF8.GetBytes(str);
        NetworkStream stream = client.GetStream();
        stream.Write(tmp,0,tmp.Length);
        Thread AcceptServer = new Thread(ClientAcceptServer_IsLogin);
        AcceptServer.Start();
        client.Close();
    }

    void ClientAcceptServer_IsLogin()
    {
        Debug.Log("サーバーからの返事を待っています");
        TcpListener myclient = new TcpListener(serverip,portnumber);
        myclient.Start();
        TcpClient server = myclient.AcceptTcpClient();
        NetworkStream stream = server.GetStream();
        Byte[] bytes = new Byte[100];
        int i;
        string str="";

        while((i = stream.Read(bytes,0,bytes.Length))!=0)
        {
            str += Encoding.UTF8.GetString(bytes,0,i);
        }
        if(bool.Parse(str)==true)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameStage");
        }
        else
        {
            Thread AgainSendServer = new Thread(ClientSendServer);
            AgainSendServer.Start();
        }
        
        server.Close();
    }




}
