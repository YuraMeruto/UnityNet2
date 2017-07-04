using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
public class Server : MonoBehaviour {
    public Text text;
    private  int portnumber = 9999;
    private Thread thre;
    private IPAddress clientIPAdress;
    private bool Is_Login = false;
    void Start()
    {
        thre = new Thread(ServerStart);
        thre.Start();
    }

void ServerStart()
    {
        Debug.Log("サーバー起動");
        //        TcpClient client = new TcpClient(my.MyIPAddressStr,portnumber);
        IPAddress adress = IPAddress.Parse("10.40.0.20");
        TcpListener server = new TcpListener(adress,portnumber);
        server.Start();
        TcpClient client = server.AcceptTcpClient();

        IPEndPoint endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
         clientIPAdress = endpoint.Address;
        NetworkStream stream = client.GetStream();
        
        //1バイトずつ受け取る
        byte[] getData = new byte[1];
        //バイト数がわからないためリストで管理
        List<byte> bytelist = new List<byte>();
        int cnt;
        while((cnt = stream.Read(getData,0,getData.Length)) > 0)
        {
            bytelist.Add(getData[0]);   
        }
        byte[] result = new byte[bytelist.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = bytelist[i];
        }
        Is_Login = true;
        string data = Encoding.UTF8.GetString(result);
        Thread sendclientThread = new Thread(ServerSendClient_IsLogin);
        sendclientThread.Start();
        Debug.Log("IpAdress"+ clientIPAdress.ToString());
        Debug.Log("送られてきた内容"+result);
        client.Close();
    }
    

    //クライアントに送るメッセージを実装
    void ServerSendClient_IsLogin()
    {
        string clientIPAdress_string = clientIPAdress.ToString();
        TcpClient client_send = new TcpClient(clientIPAdress_string, portnumber);
         Byte[] sendmessage = Encoding.UTF8.GetBytes(Is_Login.ToString());
        NetworkStream stream = client_send.GetStream();
        stream.Write(sendmessage,0,sendmessage.Length);
        client_send.Close();
    }
}
