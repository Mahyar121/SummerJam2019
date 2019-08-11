using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    // Stores the quadrants for random generation
    [SerializeField] private List<GameObject> quadrants;
    [SerializeField] private List<Transform> playerSpawns;
    [SerializeField] private List<GameObject> zones;
    // Manages the player's evolutionPoints
    [SerializeField] private int evolutionPoints = 0;

    // Creates a singleton of the SceneManager since we only need one unique instance
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<SceneManager>(); }
            return instance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        RandomizePlayerSpawn();
        RandomizeZoneLocations();
    }

    public Transform RandomizePlayerSpawn()
    {
        int randomSpawn = Random.Range(0, playerSpawns.Count);
        return playerSpawns[randomSpawn];

    }

    public void RandomizeZoneLocations()
    {
        int n = quadrants.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = quadrants[k];
            quadrants[k] = quadrants[n];
            quadrants[n] = value;
        }


        for (int i = 0; i < quadrants.Count; i++)
        {
            zones[i].transform.position = quadrants[i].transform.position;
        }
        

    }


}
