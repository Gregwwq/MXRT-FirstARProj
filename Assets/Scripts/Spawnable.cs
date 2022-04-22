using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawnable : MonoBehaviour
{
    public ARRaycastManager arRM;
    public GameObject spawnablePrefab;

    Pose placementPose;
    GameObject spawnedObj = null;

    void Start()
    {
        spawnedObj = null;
    }

    void Update()
    {
        if(Input.touchCount == 0) return;

        var touchPt = Input.GetTouch(0).position;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRM.Raycast(touchPt, hits);

        if(hits.Count == 0) return;

        placementPose = hits[0].pose;

        if(Input.GetTouch(0).phase == TouchPhase.Began) 
            Spawn(placementPose.position);
        else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObj != null)
            spawnedObj.transform.position = placementPose.position;

        if(Input.GetTouch(0).phase == TouchPhase.Ended)
            spawnedObj = null;            
    }

    void Spawn(Vector3 pos){
        spawnedObj = Instantiate(spawnablePrefab, pos, Quaternion.identity);
    }
}
