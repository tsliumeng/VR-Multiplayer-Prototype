﻿using UnityEngine;
using System.Collections;

public class NManager : MonoBehaviour {

    public byte Version = 1;

    public GameObject headPrefab;
    public GameObject leftHandPrefab;
    public GameObject rightHandPrefab;

    public virtual void Start() {
        PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        var temp = PhotonVoiceNetwork.Client;  // to activate
        
    }

    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


    public virtual void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby() {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed() {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause) {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
        PhotonNetwork.Instantiate(headPrefab.name, ViveManager.Instance.head.transform.position, ViveManager.Instance.head.transform.rotation, 0);
        PhotonNetwork.Instantiate(leftHandPrefab.name, ViveManager.Instance.lefthand.transform.position, ViveManager.Instance.lefthand.transform.rotation, 0);
        PhotonNetwork.Instantiate(rightHandPrefab.name, ViveManager.Instance.righthand.transform.position, ViveManager.Instance.righthand.transform.rotation, 0);

    }

}
