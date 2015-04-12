using System.Collections;
using System.Collections.Generic;

public class PlayerData {
    public static string key;
	public static string name;
	public static int score;

    public static Dictionary<string, bool> achievements = new Dictionary<string,bool>();

    public class Achievements
    {
        public static readonly string ZOMBIE_KILLER = "Zombie Killer"; // kills 3 zombie in a stage
        public static readonly string HOLE_PUNCHER = "Hole Puncher"; // closes 2 holes in a stage
    }

    public static void reset()
    {
        name = "";
        score = 0;

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
}
