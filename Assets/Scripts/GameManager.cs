using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   [SerializeField] private GameObject bieneplayer;
    // Start is called before the first frame update
    private string power = "low speed";
    private float time_afected = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time_afected != 0)
        {
            
            if (Time.time - time_afected>= 3f)
            {
                time_afected = 0;
                if (power == "boostjump")
                {
                    Debug.Log("reste boost");
                    bieneplayer.GetComponent<PlayerMovement1>().setPowerJump(16f);
                }
                else if (power == "freeze")
                {

                    bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    bieneplayer.GetComponent<Rigidbody2D>().WakeUp();


                }
                else if (power == "low speed")
                {
                    bieneplayer.GetComponent<PlayerMovement1>().restoreSpeed();


                }
            }
        }
    }
    
    public void Superpower()
    {
        time_afected = Time.time;
        if (power == "boostjump")
        {
            bieneplayer.GetComponent<PlayerMovement1>().setPowerJump(30f);
        }
        else if (power == "freeze")
        {
            bieneplayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
        else if (power == "low speed")
        {
            bieneplayer.GetComponent<PlayerMovement1>().setSpeed();
        }
    }
}
