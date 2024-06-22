using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CandJ : MonoBehaviourPunCallbacks
{
    public InputField create_inp;
    public InputField join_inp;
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(create_inp.text);
    }   

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(join_inp.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
