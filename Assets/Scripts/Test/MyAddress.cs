using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
public class MyAddress : MonoBehaviour {


    public string MyHostName;
    public IPAddress[] MyIPAddress;
    public string MyIPAddressStr;
	// Use this for initialization
	void Start () {
        MyHostName = Dns.GetHostName();
        MyIPAddress = Dns.GetHostAddresses(MyHostName);
        string MyIPAddressStr = "";
        foreach (IPAddress ip in MyIPAddress)
        {
            MyIPAddressStr += ip.ToString();
        }
    }
    
    public string GetHostName()
    {
        return MyHostName;
    }	

    public string GetIPAddress()
    {
     
        return MyIPAddressStr;
    }
}
