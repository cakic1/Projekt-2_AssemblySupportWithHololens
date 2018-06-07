using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARKamEnableDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ARKameraStart()
    {
        CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_DEFAULT);

        CameraDevice.Instance.Start();
    }
    public void ARKAmeraStop()
    {
        CameraDevice.Instance.Stop();

        CameraDevice.Instance.Deinit();
    }
}
