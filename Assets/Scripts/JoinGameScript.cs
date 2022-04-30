using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class JoinGameScript : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField RoomCode;

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomCode.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoomScene");
    }
}