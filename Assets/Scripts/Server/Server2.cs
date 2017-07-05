using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
public class Server2 : MonoBehaviour {

    private IPEndPoint ipend;
    private Socket sock;
    private Socket m_lisner;
    private Thread aceptthread;
    private bool threadloop=true;
    private IPAddress myaddress;
    private  int portnumber = 9999;
    void Start()
    {
        myaddress = IPAddress.Parse("192.168.0.7");
        ipend = new IPEndPoint(myaddress, Int32.Parse(portnumber.ToString()));
        TcpListener server = new TcpListener(myaddress, portnumber);
        sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        sock.Bind(ipend);
        sock.Listen(10);
        aceptthread = new Thread(ClientAccept);
    }

    void ClientAccept()
    {
        if(m_lisner !=null && m_lisner.Poll(0,SelectMode.SelectRead) )
        {
        }
    }
}
