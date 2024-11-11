using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];

    public string serverIP = "127.0.0.1"; // Cambiar según sea necesario
    public int port = 5000;

    public event Action<string> OnMessageReceived;

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, port);
            stream = client.GetStream();
            Debug.Log("Conectado al servidor.");
            BeginRead();
        }
        catch (Exception e)
        {
            Debug.LogError("Error al conectar al servidor: " + e.Message);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (client == null || !client.Connected) return;

        byte[] msg = Encoding.ASCII.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
    }

    private void BeginRead()
    {
        if (stream != null)
        {
            stream.BeginRead(buffer, 0, buffer.Length, OnRead, null);
        }
    }

    private void OnRead(IAsyncResult ar)
    {
        int bytesRead = stream.EndRead(ar);
        if (bytesRead > 0)
        {
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            OnMessageReceived?.Invoke(message);
            BeginRead();
        }
    }

    void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }
    }
}
