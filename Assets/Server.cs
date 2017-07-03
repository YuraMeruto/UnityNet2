using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server : MonoBehaviour {
    public Text text;
    private  int portnumber = 9999;
  
void Start()
    {
        //        TcpClient client = new TcpClient(my.MyIPAddressStr,portnumber);
        IPAddress adress = IPAddress.Parse("192.168.0.7");
        TcpListener server = new TcpListener(adress,portnumber);
        server.Start();
        TcpClient client = server.AcceptTcpClient();

        IPEndPoint endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
        IPAddress address = endpoint.Address;
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
        string data = Encoding.UTF8.GetString(result);
        Debug.Log("IpAdress"+address.ToString());
        Debug.Log("送られてきた内容"+result);
        client.Close();
    }
}
