using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

public class DataPlayerManager : MonoBehaviour {
	public static string namaPlayer;
	public static int killScore;
	public static int coinScore;
	
	public static Dictionary<string, bool> achievements = new Dictionary<string,bool>();

	static DataPlayerManager ()
	{
		// temporary hack, to ignore any cert fails
		System.Net.ServicePointManager.ServerCertificateValidationCallback = 
		delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, 
			         System.Security.Cryptography.X509Certificates.X509Chain chain, 
			        System.Net.Security.SslPolicyErrors sslPolicyErrors) {
			{
				return true;
			}
		};
	}

	public class Achievements
	{
		public static readonly string ZOMBIE_KILLER = "Zombie Killer"; // kills 3 zombie in a stage
		public static readonly string HOLE_PUNCHER = "Hole Puncher"; // closes 2 holes in a stage
	}
	
	public static void reset()
	{
		namaPlayer = "";
		killScore = 0;
		coinScore = 0;
		
		achievements.Clear();
	}
	
	public static void AddAchievement(string name)
	{
		if (achievements.ContainsKey(name))
		{
			throw new System.InvalidOperationException("Achievement already enabled!");
		}
		else
		{
			achievements.Add(name, true);
		}
	}
	
	public static bool CheckAchivement(string name)
	{
		return achievements.ContainsKey(name);
	}
	// Use this for initialization
	void Start () {
		reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
