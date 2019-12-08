using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        CreatePlayer();
	}

    //Creates a networked player object for each player that loads into the multiplayer room
    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Non VR Character Controller"), Vector3.zero, Quaternion.identity); 
    }
}
