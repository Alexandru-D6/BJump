using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class PFB_Bee : MonoBehaviour
{
    public BeeSpawner bee_spawner;
    public GameObject game_area;
 
    public float speed;
 
    void Update()
    {
        Move();
    }
 
    void Move()
    {
        /** Move this bee forward per frame, if it gets too far from the game area, destroy it **/
 
        transform.position += transform.up * (Time.deltaTime * speed);
 
        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if(distance > bee_spawner.death_circle_radius)
        {
            Removebee();
        }
    }
 
    void Removebee()
    {
        /** Update the total bee count and then destroy this individual bee. **/
 
        Destroy(gameObject);
        bee_spawner.bee_count -= 1;
    }
}
 