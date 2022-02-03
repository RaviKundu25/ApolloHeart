using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    string roomName = "ApolloHeart";

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Player has been connected to Photon server");
        createOrJoinRoom();
    }

    public void createOrJoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        Debug.Log("Created Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Failed To Create Room :: Return Code : " + returnCode.ToString() + " :: Message : " + message);
        joinRoom();
    }

    void joinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayerInst"), new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Join Room :: Return Code : " + returnCode.ToString() + " :: Message : " + message);
    }

    public void sendMSG()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PhotonView>().IsMine)
            {
                players[i].GetComponent<PhotonView>().RPC("RPC_SendString", RpcTarget.AllBuffered, players[i].GetComponent<PhotonView>().ViewID.ToString(), players[i].GetComponent<PhotonView>().ViewID.ToString() + " sent Hello");
            }
        }
    }
}
