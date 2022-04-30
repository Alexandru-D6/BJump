// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to connect, and join/create room automatically
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
	#pragma warning disable 649

    /// <summary>
    /// Launch manager. Connect, join a random room or create one if none or all full.
    /// </summary>
	public class WaitingRoomScript : MonoBehaviourPunCallbacks
    {

		#region Private Serializable Fields

		[Tooltip("The Ui Text to inform the user about the connection progress")]
		[SerializeField]
		private Text code;

		[Tooltip("The Ui Text to inform the user about the connection progress")]
		[SerializeField]
		private Text NumPlayers;

		[Tooltip("The maximum number of players per room")]
		[SerializeField]
		private byte maxPlayersPerRoom = 2;

		#endregion

		#region Private Fields
		/// <summary>
		/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
		/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
		/// Typically this is used for the OnConnectedToMaster() callback.
		/// </summary>
		bool isConnecting;

		/// <summary>
		/// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
		/// </summary>
		string gameVersion = "1";

		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{

			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.AutomaticallySyncScene = true;

		}

        #endregion


        #region Public Methods

        private void Start()
        {
			code.text = PhotonNetwork.CurrentRoom.Name;
			NumPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
		}

        /// <summary>
        /// Start the connection process. 
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        /// 

        public void startButton()
        {
			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			/*if (PhotonNetwork.IsConnected)
			{
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
				PhotonNetwork.JoinRoom(code.ToString());
			}
			else
			{


				// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.ConnectUsingSettings();
				PhotonNetwork.GameVersion = this.gameVersion;
			}*/
		}

		public override void OnJoinedRoom()
		{
			code.text = PhotonNetwork.CurrentRoom.Name;
			NumPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
			//LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
			Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

			// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
		}


        #endregion


        #region MonoBehaviourPunCallbacks CallBacks
        // below, we implement some callbacks of PUN
        // you can find PUN's callbacks in the class MonoBehaviourPunCallbacks


        /// <summary>
        /// Called after the connection to the master is established and authenticated
        /// </summary>
        public override void OnConnectedToMaster()
		{
			code.text = PhotonNetwork.CurrentRoom.Name;
			NumPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
			//LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
			Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

			// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
		}
		

		#endregion
		
	}
}