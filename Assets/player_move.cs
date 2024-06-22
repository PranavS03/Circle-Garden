using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class player_move : MonoBehaviour
{



public float speed = 0.005f;
public Rigidbody2D rb;
public PhotonView pv;

public void Start()
{
    rb = GetComponent<Rigidbody2D>();
    pv = GetComponent<PhotonView>();
}

public void Update()
{
    if(pv.IsMine)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        //rb.MovePosition(rb.transform.position + tempVect);
        rb.velocity = tempVect;
    }
    
    
        

}
}
