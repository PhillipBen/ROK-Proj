using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkDB : MonoBehaviour
{
    public User[] userList;

    void Start() {
        //StartCoroutine(GetRequest("http://192.168.0.145/ROK-Proj-DB/UserLoad.php"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    //Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    //Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    LoadUserData(webRequest.downloadHandler.text);
                    break;
            }
        }
    }


    private void LoadUserData(string itemsDataString) {
        var users = itemsDataString.Split(';');
        var temp = (users.Length - 1);
        users = users[0..temp]; //Removes the last item index, which is blank, cause last ; split means nothing on last item. //temp is 2, but range # is exclusive.

        userList = new User[users.Length];
        for(int i = 0; i < users.Length; i++) {
            userList[i] = parseNewUser(users[i]);
        }

        //userList is now ready to do whatever you want to do with.
        // for(int i = 0; i < userList.Length; i++) {
        //     Debug.Log("UserL " + i + ": " + userList[i]);
        // }
    }

    public User parseNewUser(string userDataString) {
        var stringList = userDataString.Split('|');
        for(int i = 0; i < stringList.Length; i++) {
            var temp = stringList[i].Split(':');
            stringList[i] = temp[1];
        }
        return new User(int.Parse(stringList[0]), stringList[1]);
    }
}
