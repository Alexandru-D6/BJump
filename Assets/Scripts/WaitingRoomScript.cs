using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomScript : MonoBehaviourPunCallbacks
{
    [SerializeField] Text numPlayers;
    [SerializeField] Text codeRoom;

    private void Start()
    {
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        codeRoom.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
