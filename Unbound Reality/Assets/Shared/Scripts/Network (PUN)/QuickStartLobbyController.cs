using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int roomSize;

    public override void OnConnectedToMaster()
    {
        //Makes it so whatever scene the master client 
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    //Paired with Quick Start Button
    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick Start");
    }

    //Called when PhotonNetwork.JoinRandomRoom fails
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    //Creates our own room
    void CreateRoom()
    {
        Debug.Log("Creating a room now");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOptions);
        Debug.Log(randomRoomNumber);
    }

    //Called when PhotonNetwork.CreateRoom 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to createa a room... trying again");
        //We retry to create a room because the likely hood of it failing is due to picking a name that already exists. 
        CreateRoom(); 
    }

    //Paired with Quick Cancel Button
    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveLobby();
    }

    //Returns Lobby statistics like CCU (Concurrent users) every minute. Requires that Enabled Lobby Statistics in PhotonServerSettings is checked.
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log(PhotonNetwork.CountOfPlayers.ToString() + " Players Online");
    }
}
