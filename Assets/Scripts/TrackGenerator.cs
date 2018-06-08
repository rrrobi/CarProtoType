using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {


    public GameObject trackPieceToSpawn;
    Vector3 spawnPos;
    Quaternion spawnRot;
    int trackCount = 0;

	// Use this for initialization
	void Start () {
        spawnPos = new Vector3(0.0f, 0.0f, 0.0f);
        spawnRot = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetMouseButtonDown(0))
        {
            spawnPos = new Vector3(0.0f, 0.0f, trackCount * 50);
            Instantiate(trackPieceToSpawn, spawnPos, spawnRot);
            trackCount++;
        }
	}
}
