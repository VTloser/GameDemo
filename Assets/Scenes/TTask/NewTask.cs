using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;

public class SelectNet : MonoBehaviour
{
    Socket listenfd;
    Dictionary<Socket, ClientState> ClientItem = new Dictionary<Socket, ClientState>();
    private void Awake()
    {
        Connection("127.0.0.1", 8088);
    }
    public void Connection(string Ip, int Port)
    {
        listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(Ip), Port);
        listenfd.Bind(iPEndPoint);
        listenfd.Listen(0);
    }
    
    List<Socket> CheckRead = new List<Socket>();
    
    private void Update()
    {
        CheckRead.Clear();
        CheckRead.Add(listenfd);
        foreach (var item in ClientItem.Values)
        {
            CheckRead.Add(item.socket);
        }
        Socket.Select(CheckRead, null, null, 1000);
        foreach (var item in CheckRead)
        {
            if (item == listenfd)
            {
                ReadListenfd(item);
            }
            else
            {
                ReadClientfd(item);
            }
        }
    }
    private void ReadListenfd(Socket listenfd)
    {
        Socket socketfd = listenfd.Accept();
        ClientState client = new ClientState(socketfd);
        ClientItem.Add(client.socket, client);
        Debug.Log("Accpts Connection" + client.socket.RemoteEndPoint);
    }
    private bool ReadClientfd(Socket socket)
    {
        ClientState state = ClientItem[socket];
        int count = 0;
        try
        {
            count = socket.Receive(state.ReaderBuffer);
            if (count <= 0)
            {
                Debug.Log($"检测到IP：{state.socket.RemoteEndPoint}断开");
                state.socket.Shutdown(SocketShutdown.Both);
                ClientItem.Remove(state.socket);
                state.socket.Close();
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log($"检测到IP：{state.socket.RemoteEndPoint}断开 错误原因：{e}");
            state.socket.Shutdown(SocketShutdown.Both);
            ClientItem.Remove(state.socket);
            state.socket.Close();
            return false;
        }
        string str = System.Text.Encoding.UTF8.GetString(state.ReaderBuffer, 0, count);
        Debug.Log(str);
        return true;
    }
}

class ClientState
{
    public Socket socket;
    public byte[] ReaderBuffer;
    public ClientState(Socket socket)
    {
        this.socket = socket;
        ReaderBuffer = new byte[1024];
    }
}