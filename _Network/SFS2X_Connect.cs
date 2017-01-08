using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2x.Requests;

public class SFS2X_Connect : MonoBehaviour {

	public string ServerIP = "127.0.0.1";
	public int ServerPort = 9933;
	public string ZoneName = "HillTown - TestZone";
	public string UserName = "";


	SmartFox sfs;

	// Use this for initialization
	void Start () {

		sfs = new SmartFox();
		sfs.ThreadSafeMode = true;

		sfs.AddEventListener (SFSEvent.CONNECTION, OnConnection);
		sfs.AddEventListener (SFSEvent.LOGIN, OnLogin);
		sfs.AddEventListener (SFSEvent.LOGIN_ERROR, OnLoginError);

		sfs.Connect(ServerIP, ServerPort);
	}

	void OnLogin(BaseEvent e) {
		Debug.Log ("Logged In: " + e.Params ["user"]);
	}

	void OnLoginError(BaseEvent e) {
		Debug.Log ("Login Error (" + e.Params ["errorCode"] + "): " + e.Params ["errorMessage"]);
	}

	void OnConnection(BaseEvent e) {
		if ((bool)e.Params ["success"]) {
			Debug.Log ("Successfully Connected");
			sfs.Send (new LoginRequest (UserName, "", ZoneName));
		} else {
			Debug.Log ("Connection Failed");
		}
	}

	// Update is called once per frame
	void Update () {
		sfs.ProcessEvents();
	}

	void OnApplicationQuit(){
		if (sfs.IsConnected)
			sfs.Disconnect ();
	}
}
