using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class WaitingRoomScript : MonoBehaviourPunCallbacks
{
    [SerializeField] Text numPlayers;
    [SerializeField] Text codeRoom;

    private void Start()
    {
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        codeRoom.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnJoinedRoom()
    {
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }
}
