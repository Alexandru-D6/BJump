using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{   [SerializeField] GameObject bieneplayer;
    [SerializeField] PhotonView view;
    // Start is called before the first frame update
    private string power = "freeze";
    private string afectedPower = "";
    private float time_afected = 0f;
    void Start()
    {
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
    }

    public string getPower() { return power; }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (time_afected != 0)
            {

                if (Time.time - time_afected >= 3f)
                {
                    time_afected = 0;
                    if (power == afectedPower)
                    {
                        Debug.Log("reste boost");
                        bieneplayer.GetComponent<PlayerMovement1>().setPowerJump(16f);
                    }
                    else if (power == afectedPower)
                    {

                        bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        bieneplayer.GetComponent<Rigidbody2D>().WakeUp();


                    }
                    else if (power == afectedPower)
                    {
                        bieneplayer.GetComponent<PlayerMovement1>().restoreSpeed();


                    }
                }
            }
        }
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
