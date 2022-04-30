using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxPlayers;

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
        SceneManager.LoadScene("Game");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoomScene");
    }
}
