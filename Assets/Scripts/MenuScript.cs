using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxPlayers;

    private bool singleplayer = false;

    string generateCode()
    {
        string rnd = "";

        for (int i = 0; i < 4; ++i)
        {
            rnd += (char)('A' + (int)(Random.value * 23));
        }

        return rnd;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(generateCode());
    }

    public void JoinRoom()
    {
        SceneManager.LoadScene("JoinGameScene");
    }

    public void SinglePlayer()
    {
        singleplayer = true;
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        if (!singleplayer)
            PhotonNetwork.LoadLevel("WaitingRoomScene");
        else
            PhotonNetwork.LoadLevel("Game");
    }
}
