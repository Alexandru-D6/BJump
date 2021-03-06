using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{   [SerializeField] GameObject bieneplayer;
    [SerializeField] PhotonView view;
    [SerializeField] Image img;

    [SerializeField] Sprite img1;
    [SerializeField] Sprite img2;
    [SerializeField] Sprite img3;

    // Start is called before the first frame update
    private string power = "freeze";
    private string afectedPower = "";
    private float time_afected = 0f;

    public int initPlayers = -1;
    public int curPlayers = -1;
    void Start()
    {
        float num = Random.Range(0f, 3f);
        
        if(num >= 0f && num < 1f) power = "boostjump";
        else if (num >= 1f && num < 2f) power = "freeze";
        else power = "low speed";
        
        var objects = FindObjectsOfType<GameObject>();
        foreach(var aa in objects)
        {
            PhotonView _view = aa.GetComponent<PhotonView>();
            if (_view != null && _view.IsMine)
            {
                bieneplayer = aa;
                view = _view;
            }
        }

        initPlayers = FindObjectsOfType<PhotonView>().Length;
        curPlayers = initPlayers;

        if (power == "freeze") 
        { 
            img.sprite = img2;
        }
        else if (power == "low speed")
        {
            img.sprite = img1;
        }
        else if (power == "boostjump")
        {
            img.sprite = img3;
        }
    }
    
    public void setEnabledImage (bool state)
    {
        img.enabled = state;
    }

    public string getPower() { return power; }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && FindObjectsOfType<PhotonView>().Length == 1 && curPlayers != 1)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("EndGameScreenWin");
        }
        if (view == null)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("EndGameScreenDeath");
        }
        else
        {
            if (view.IsMine)
            {
                if (time_afected != 0)
                {

                    if (Time.time - time_afected >= 3f)
                    {
                        time_afected = 0;
                        if (afectedPower == "boostjump")
                        {
                            bieneplayer.GetComponent<PlayerMovement1>().setPowerJump(16f);
                        }
                        else if (afectedPower == "freeze")
                        {
                            bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                            bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                            bieneplayer.GetComponent<Rigidbody2D>().WakeUp();
                        }
                        else if (afectedPower == "low speed")
                        {
                            bieneplayer.GetComponent<PlayerMovement1>().restoreSpeed();
                        }
                    }
                }
            }
        }
    }

    public void substractPlayer()
    {
    }

    public void Superpower(int _view, string en_power)
    {
        if (view.IsMine)
        {
            time_afected = Time.time;
            if (en_power == "boostjump" && _view == view.ViewID)
            {
                afectedPower = "boostjump";
                bieneplayer.GetComponent<PlayerMovement1>().setPowerJump(30f);
            }
            else if (en_power == "freeze" && _view != view.ViewID)
            {
                afectedPower = "freeze";
                bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            }
            else if (en_power == "low speed" && _view != view.ViewID)
            {
                afectedPower = "low speed";
                bieneplayer.GetComponent<PlayerMovement1>().setSpeed();
            }
        }
    }
}
