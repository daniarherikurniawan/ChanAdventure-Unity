using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

using UnityEngine;

public class PlayerDAO
{
    const string SERVER_URL = "https://shining-inferno-7543.firebaseio.com/";
    const string SERVER_TABLE_NAME = "players";
	const string SERVER_AUTH = "r6DoGJNePV7AyLodiTgFvjCRvrY0G4hKuYTfiHvm";
	
    static readonly string FIELD_NAME = "name";
    static readonly string FIELD_SCORE = "score";
    static readonly string FIELD_ACHIEVEMENTS = "achievements";
    static readonly string[] FIELDS_REQUIRED = { FIELD_NAME, FIELD_SCORE };

    public static void GetCurrentPlayerData ()
    {
        // check if the player exists
        JSONObject json = null;

        if (DataPlayerManager.namaPlayer != null)
        {
            json = GetPlayerData(DataPlayerManager.namaPlayer);
        }

        if (json != null)
        {
            if (IsPlayerJSONValid(json))
            {
				DataPlayerManager.namaPlayer = json.GetField(FIELD_NAME).ToString();

                if (json[FIELD_ACHIEVEMENTS] != null)
                {
                    GetAchievements(json[FIELD_ACHIEVEMENTS], out DataPlayerManager.achievements);
                }
            }
            else
            {
                throw new FormatException("Invalid JSON format!");
            }
        }
    }

    public static void PostCurrentPlayerData()
    {
        // build le JSON object
        JSONObject json = null;
        if (!String.IsNullOrEmpty(DataPlayerManager.namaPlayer))
        {
            // check if player exists
            string key = GetPlayerKey(DataPlayerManager.namaPlayer);
            if (!String.IsNullOrEmpty(key))
            {
                json = new JSONObject();

                json.AddField(FIELD_NAME, DataPlayerManager.namaPlayer);
                PutHighScore(json, DataPlayerManager.killScore);
                PutAchievements(json, DataPlayerManager.achievements);

                UpdatePlayerData(json, key);
            }
            else 
            {
				CreatePlayerData(DataPlayerManager.namaPlayer, DataPlayerManager.killScore, DataPlayerManager.achievements);
            }
        }
        else
        {
            throw new ArgumentException("Current player has no name!");
        }
    }

    public static bool IsPlayerJSONValid (JSONObject json)
    {
        bool ret = (json != null);
        if (ret)
        {
            ret = json.HasFields(FIELDS_REQUIRED);

            try
            {
                int.Parse(json.GetField(FIELD_SCORE).ToString());
            }
            catch (Exception )
            {
                ret = false;
            }

            if (ret && json.HasField(FIELD_ACHIEVEMENTS))
            {
                foreach(var kvp in json.GetField(FIELD_ACHIEVEMENTS).ToDictionary())
                {
                    try
                    {
                        Boolean.Parse(kvp.Value);
                    }
                    catch (Exception )
                    {
                        ret = false;
                    }
                }
            }
        }

        return ret;
    }

    static void UpdatePlayerData(JSONObject playerObject, string key)
    {
        if (IsPlayerJSONValid(playerObject) && !String.IsNullOrEmpty(key))
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(SERVER_URL + SERVER_TABLE_NAME +
				                                  String.Format("/{0}.json?auth={1}", key, SERVER_AUTH));
            request.Method = "PATCH";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(playerObject.Print());
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                StringBuilder builder = new StringBuilder();
                using (StreamReader reader = new StreamReader(response.GetResponseStream())) 
                {
                    builder.AppendFormat("Cannot update player {0}'s data:", playerObject[FIELD_NAME].ToString());
                    builder.AppendLine();
                    builder.Append(reader.ReadToEnd());
                }

                throw new WebException(builder.ToString());
            }
        }
    }

    static void PutHighScore (JSONObject playerObject, int playerScore)
    {
        playerObject.SetField(FIELD_SCORE, playerScore);
    }

    static void PutAchievements(JSONObject playerObject, Dictionary<string, bool> achievements)
    {
        if (achievements != null)
        {
            if (achievements.Count > 0)
            {
                Dictionary<string, string> obj = new Dictionary<string, string>();

                foreach (var kvp in achievements)
                {
                    obj.Add(kvp.Key, kvp.Value.ToString());
                }

                if (playerObject.HasField(FIELD_ACHIEVEMENTS))
                {
                    playerObject.AddField(FIELD_ACHIEVEMENTS, new JSONObject(obj));
                }
                else
                {
                    playerObject.SetField(FIELD_ACHIEVEMENTS, new JSONObject(obj));
                }
            }
        }
    }

    static void GetAchievements(JSONObject achievementObject, out Dictionary<string, bool> achievements)
    {
        achievements = new Dictionary<string,bool>();
        foreach (var kvp in achievementObject.ToDictionary())
        {
            achievements.Add(kvp.Key, bool.Parse(kvp.Value));
        }
    }

    public static JSONObject GetPlayerData (string playerName)
    {
        JSONObject json = null;
        WWW www = new WWW(SERVER_URL + SERVER_TABLE_NAME + 
		                  String.Format(".json?auth={0}&orderBy=\"{1}\"&limitTo=\"{2}\"", SERVER_AUTH, FIELD_NAME, playerName));

        while (!www.isDone)
        {
            Thread.Sleep(500);
        }

        if (www.error == null)
        {
            json = new JSONObject(www.text);

            if (json != null)
            {
                if (json.IsObject && json.keys.Count > 0)
                {
                    if (!IsPlayerJSONValid(json.GetField(json.keys[0])))
                    {
                        throw new FormatException("Invalid player data: " + json.Print(true));
                    }
                    json = json.GetField(json.keys[0]);
                }
                else
                {
                    throw new FormatException("Invalid player data: " + json.Print(true));
                }
            }
        }
        else
        {
            throw new WebException("Cannot load player data with name " + playerName + ": " + www.error);
        }

        return json;
    }

    public static string GetPlayerKey(string playerName)
    {
        string ret = null;
        WWW www = new WWW(SERVER_URL + SERVER_TABLE_NAME +
		                  String.Format(".json?auth={0}&orderBy=\"{1}\"&limitTo=\"{2}\"", SERVER_AUTH, FIELD_NAME, playerName));
		Debug.Log (www.url);
        while (!www.isDone)
        {
            Thread.Sleep(500);
        }

        if (www.error == null)
        {
            JSONObject json = new JSONObject(www.text);

            if (json != null)
            {
                if (json.IsObject)
                {
                    ret = json.keys[0];
                }
            }
        }
        else
        {
            throw new WebException("Cannot load player data with name " + playerName + ": " + www.error);
        }

        return ret;
    }

    public static void DeletePlayerData (string playerName)
    {
        string key = GetPlayerKey(playerName);

        if (key != null)
        {
            HttpWebRequest request = 
                (HttpWebRequest)WebRequest.Create(SERVER_URL + SERVER_TABLE_NAME + 
				                                  String.Format("/{0}.json?auth={1}", key, SERVER_AUTH));
            request.Method = "DELETE";

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Cannot delete player ").Append(playerName).Append(":").AppendLine();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    sb.Append(reader.ReadToEnd());
                }

                throw new WebException(sb.ToString());
            }
        }
        else
        {
            throw new Exception("Player not found!");
        }
    }
    
    public static void CreatePlayerData (string playerName, int playerScore, Dictionary<string, bool> achievements = null)
    {
        // create new player data
        JSONObject json = new JSONObject();
        json.AddField(FIELD_NAME, playerName);
        json.AddField(FIELD_SCORE, playerScore);
        PutAchievements(json, achievements);

		Debug.Log(SERVER_URL + SERVER_TABLE_NAME + ".json?auth=" + SERVER_AUTH);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SERVER_URL + SERVER_TABLE_NAME + ".json?auth=" + SERVER_AUTH);
        request.Method = "POST";
		try {

        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(json.ToString());
			}
		} catch (Exception e) 
		{
			Debug.Log (e);
			Application.Quit();
		}

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if (response.StatusCode != HttpStatusCode.OK)
        {
            StringBuilder errorBuilder = new StringBuilder();
            errorBuilder.AppendFormat("Cannot create new player data [{0}]:", response.StatusDescription);
            errorBuilder.AppendLine();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                errorBuilder.Append(reader.ReadToEnd());
            }

            throw new WebException(errorBuilder.ToString());
        }
        response.Close();
    }

    public static JSONObject GetHighScores()
    {
        WWW www = new WWW(SERVER_URL + SERVER_TABLE_NAME + ".json");
        JSONObject result = null;

        while (!www.isDone)
        {
            // TODO: make this nonblocking
        }

        if (www.error == null)
        {
            result = new JSONObject(www.text);
        }
        else
        {
            throw new WebException("Cannot get high score data: " + www.error);
        }
        
        return result;
    }

    public static string GetHighScoresAsString()
    {
        JSONObject json = GetHighScores();
        string ret = null;

        if (json != null)
        {
            StringBuilder builder = new StringBuilder();
            int counter = 1;

            foreach (string key in json.keys)
            {
                var playerObject = json.GetField(key);
                if (IsPlayerJSONValid(playerObject))
                {
                    string name = playerObject.GetField(FIELD_NAME).ToString()
                                    .Trim(new char[] { '"', '\'' });

                    string score = playerObject.GetField(FIELD_SCORE).ToString();

                    builder.AppendFormat("{0}. {1,-25} | {2,-5}", counter++, name, score);
                    builder.AppendLine();

                    if (playerObject[FIELD_ACHIEVEMENTS] != null)
                    {
                        foreach (var kvp in playerObject.GetField(FIELD_ACHIEVEMENTS).ToDictionary())
                        {
                            if (Boolean.Parse(kvp.Value))
                            {
                                builder.Append(kvp.Key);
                                builder.AppendLine();
                            }
                        }
                    }
                    builder.AppendLine();
                }
                else
                {
                    throw new FormatException("Invalid player object data: " + playerObject.ToString(true));
                }
            }

            ret = builder.ToString();
        }

        return ret;
    }
}
