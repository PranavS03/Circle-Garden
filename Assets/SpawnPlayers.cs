using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
