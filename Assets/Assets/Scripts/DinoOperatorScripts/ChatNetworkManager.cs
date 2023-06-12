using UnityEngine;
using UnityEngine.UI;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.Operator
{
    [AddComponentMenu("")]
    public class ChatNetworkManager : NetworkManager
    {
        public Toggle[] OnlineIndicators;  
        public Toggle[] Male;
        public Toggle[] Female;
        public Toggle[] CurrentPlayer;       
     
        public void SetHostname(string hostname)
        {
            networkAddress = hostname;
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            // remove player name from the HashSet
            if (conn.authenticationData != null)
                Player.playerNames.Remove((string)conn.authenticationData);


            // remove connection from Dictionary of conn > names
            ChatUI.connNames.Remove(conn);

            string newPlayer = (string)conn.authenticationData;
            newPlayer = newPlayer.Replace("OC", "");

            int playerIndex = -1;
            int.TryParse(newPlayer, out playerIndex);

            if (playerIndex > 0)
            {
                OnlineIndicators[playerIndex - 1].isOn = false;
                CurrentPlayer[playerIndex - 1].isOn = false;
                
                Debug.Log("OC" + newPlayer + " Has Left The Game");
                
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            LoginUI.instance.gameObject.SetActive(true);
            LoginUI.instance.usernameInput.text = "";
            LoginUI.instance.usernameInput.ActivateInputField();
        }

       public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Transform startPos = GetStartPosition();
            GameObject player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

            // instantiating a "Player" prefab gives it the name "Player(clone)"
            // => appending the connectionId is WAY more useful for debugging!
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);

            ChatUI.localPlayerName = (string)conn.authenticationData;
            ChatUI.connNames.Add(conn, (string)conn.authenticationData);

            string newPlayer = (string)conn.authenticationData;
            newPlayer = newPlayer.Replace("OC", "");

            int playerIndex = -1;
            int.TryParse(newPlayer, out playerIndex);

            Player playerComponent = player.GetComponent<Player>();

            if (playerIndex > 0)
            {
                OnlineIndicators[playerIndex - 1].isOn = true;
                Male[playerIndex - 1].onValueChanged.RemoveAllListeners();
                Male[playerIndex - 1].onValueChanged.AddListener((isOn) =>
                {
                    if(isOn)
                    {
                        playerComponent.SetToMale();
                        playerComponent.RpcSetGenderMale();
                        playerComponent.SkinCngMale();
                        playerComponent.RpcChangeSkinMale();
                        CurrentPlayer[playerIndex - 1].isOn = true;
                    }
                    else
                    {
                        CurrentPlayer[playerIndex - 1].isOn = false;
                    }
                });
                Female[playerIndex - 1].onValueChanged.RemoveAllListeners();
                Female[playerIndex - 1].onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        playerComponent.SetToFemale();
                        playerComponent.RpcSetGenderFemale();
                        playerComponent.SkinCngFemale();
                        playerComponent.RpcChangeSkinFemale();
                        CurrentPlayer[playerIndex - 1].isOn = true;
                    }
                    else
                    {
                        CurrentPlayer[playerIndex - 1].isOn = false;
                    }
                });

                Debug.Log("OC"+ newPlayer + " Has Joined The Game");
                
            }
        }      
    }
}
