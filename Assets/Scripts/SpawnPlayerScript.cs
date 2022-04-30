using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayerScript : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector3 spawnLocation = new Vector3(0, 0, -1f);

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnLocation, Quaternion.identity);
    }
}
