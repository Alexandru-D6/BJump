using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class WaitingRoomScript : MonoBehaviourPunCallbacks
{
    [SerializeField] Text numPlayers;
    [SerializeField] Text codeRoom;
    [SerializeField] Button start;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        numPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        codeRoom.text = PhotonNetwork.CurrentRoom.Name;

        start.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void startGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
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
