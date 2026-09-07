using UnityEngine;

//Wo der Player spawnt/respawnt logik
//In der Hierarchie änderbar

public class PlayerSpawnManager : MonoBehaviour
{
    void Start()
    {
        string spawnName = PlayerPrefs.GetString("SpawnPoint", "");

        if (spawnName != "")
        {
            GameObject spawnPoint = GameObject.Find(spawnName);

            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }

            PlayerPrefs.DeleteKey("SpawnPoint");
        }
    }
}