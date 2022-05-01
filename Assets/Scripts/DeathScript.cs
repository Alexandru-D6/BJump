using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(collision.gameObject.GetPhotonView());
            FindObjectOfType<GameManager>().substractPlayer();
        }
    }
}
