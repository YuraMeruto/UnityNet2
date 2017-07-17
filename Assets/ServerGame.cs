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
public class ServerGame : MonoBehaviour {

    private Socket server;
    private Socket namesocket;
    private Socket timesocket;
    private IPAddress my_Ipaddres;
    private IPEndPoint ep;
    private IPEndPoint nameep;
    private IPEndPoint timeep;
    private int clientport = 9999;
    private int nameport = 9998;
    private int timeport = 9997;
    public List<Ranking> rankinglist = new List<Ranking>();
	// Use this for initialization
	void Start () {
        server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        namesocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        timesocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        my_Ipaddres = IPAddress.Parse("10.40.0.10");
        ep = new IPEndPoint(my_Ipaddres,clientport);
        nameep = new IPEndPoint(my_Ipaddres,nameport);
        timeep = new IPEndPoint(my_Ipaddres,timeport);
        Thread IniThread = new Thread(Ini);
        
    }

    void Ini()
    {
        server.Bind(ep);
        server.Listen(10);
        server.Accept();
        if (server.Connected)
        {
            Recv();
        }
    }
	
    void Recv()
    {
        byte[] recvbyte = new byte[1000];
        server.Receive(recvbyte);
        string msg = Encoding.UTF8.GetString(recvbyte);
        string[] arraydata = msg.Split('/');
        foreach(string data in arraydata)
        {

        }
        float time = float.Parse(arraydata[arraydata.Length]);
        Debug.Log(msg);
        
    }

    void RankingSort()
    {

    }
}
