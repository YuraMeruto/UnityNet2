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
    public Text tex;
    private bool Is_Scene=false;
    void Start()
    {
        serverip = IPAddress.Parse("192.168.0.7");
        sendthread = new Thread(ClientSendServer);
        sendthread.Start();
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
        Debug.Log("クライアント起動");
        TcpClient client = new TcpClient(serverip.ToString(), portnumber);
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
        /*
        TcpListener myclient = new TcpListener(serverip,portnumber);
        myclient.Start();
        TcpClient server = myclient.AcceptTcpClient();
        NetworkStream stream = server.GetStream();
        //1バイトずつ受け取る
        byte[] getData = new byte[1];
        //バイト数がわからないためリストで管理
        List<byte> bytelist = new List<byte>();
        int cnt;
        while ((cnt = stream.Read(getData, 0, getData.Length)) > 0)
        {
            bytelist.Add(getData[0]);
        }

        byte[] result = new byte[bytelist.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = bytelist[i];
        }
        Debug.Log(bool.Parse(result.ToString()));
        string data = Encoding.UTF8.GetString(result);
        */
        IPAddress adress = IPAddress.Parse("192.168.0.7");
        TcpListener server = new TcpListener(adress, portnumber);
        server.Start();
        TcpClient client = server.AcceptTcpClient();


        NetworkStream stream = client.GetStream();

        //1バイトずつ受け取る
        byte[] getData = new byte[1];
        //バイト数がわからないためリストで管理
        List<byte> bytelist = new List<byte>();
        int cnt;
        while ((cnt = stream.Read(getData, 0, getData.Length)) > 0)
        {
            bytelist.Add(getData[0]);
        }
        byte[] result = new byte[bytelist.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = bytelist[i];
        }
        string data = Encoding.UTF8.GetString(result);
        if (bool.Parse(result.ToString())==true)
        {
            Is_Scene = true;
        }
        else
        {
            Thread AgainSendServer = new Thread(ClientSendServer);
            AgainSendServer.Start();
        }
        client.Close();      
//        server.Close();
    }




}
