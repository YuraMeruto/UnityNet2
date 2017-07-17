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

public class ClientGame : MonoBehaviour
{
    /// <summary>
    /// 高校生用ゲーム
    /// </summary>

    private Socket server;
    private Socket timesendsocket;
    private Socket namesendsocket;
    private IPAddress server_ip;
    private IPEndPoint ep;
    private IPEndPoint epname;
    private IPEndPoint eptime;
    private int sendnameport = 9999;
    private int sendmessage = 9998;
    private int sendtimeport = 9997;
    private enum State { Nones, Send, Recv, Accept, Connect }
    private State state = State.Nones;
    private bool Is_Recv = false;
    public List<Ranking> ranklist;
    void Start()
    {
        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        timesendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        timesendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        server_ip = IPAddress.Parse("10.40.0.10");
        ep = new IPEndPoint(server_ip, sendmessage);
        eptime = new IPEndPoint(server_ip, sendtimeport);
        epname = new IPEndPoint(server_ip, sendnameport);
        Thread thre = new Thread(Ini);
    }


    void Ini()
    {
        server.Connect(ep);
        if (server.Connected)
        {
            ServerSendRanking("hogeohge", 100.0f);
        }
    }

    void ServerSendRanking(string name, float time)
    {
        if (server.Connected)
        {
            //            namesendsocket.Connect(epname);
            //            timesendsocket.Connect(eptime);
            //            if (timesendsocket.Connected && namesendsocket.Connected)
            //            {
            //                byte[] sendname = Encoding.UTF8.GetBytes(name);
            //                byte[] sendtime = Encoding.UTF8.GetBytes(time.ToString());
            //                byte[] sendSlash = Encoding.UTF8.GetBytes("/");
            //                namesendsocket.Send(sendname, sendname.Length, SocketFlags.None);
            //                timesendsocket.Send(sendtime, sendtime.Length, SocketFlags.None);

            string summessage = name + "/" + time.ToString();
            byte[] sendmessage = Encoding.UTF8.GetBytes(summessage);
            server.Send(sendmessage,sendmessage.Length,SocketFlags.None);
            Is_Recv = true;
            ServerRecv();
            //            }
        }
        else
        {
            Ini();
        }
    }

    void ServerRecv()
    {
        byte[] recvname = new byte[1000];
        namesendsocket.Receive(recvname);
        string msgname = Encoding.UTF8.GetString(recvname);

        byte[] recvtime = new byte[1000];
        timesendsocket.Receive(recvtime);
        string msgtime = Encoding.UTF8.GetString(recvtime);

    }
}
