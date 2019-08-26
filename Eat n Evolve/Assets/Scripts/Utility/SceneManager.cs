using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    // Stores the quadrants for random generation
    [SerializeField] private List<GameObject> quadrants;
    [SerializeField] private List<Transform> playerSpawns;
    [SerializeField] private List<GameObject> zones;

    // Enemy Spawn Points
    [SerializeField] private List<Transform> fishySpawns;
    [SerializeField] private List<Transform> sneakySpawns;
    [SerializeField] private List<Transform> generalEnemySpawns;

    // Enemy Fishy Prefabs
    [SerializeField] private GameObject PricklePrish; // Spike + Fishy
    [SerializeField] private GameObject Hornfin; // Horn + Fishy
    [SerializeField] private GameObject Scrashark; // Claw + Fishy

    // Enemy Sneaky Prefabs
    [SerializeField] private GameObject SneakyPrickle; // Spike + Sneaky
    [SerializeField] private GameObject Hornshrub; // Horn + Sneaky
    [SerializeField] private GameObject Scrachthorn; // Claw + Sneaky

    // Enemy Spike
    [SerializeField] private GameObject PricklePrickle; // Spike
    // Enemy Charge/Horn
    [SerializeField] private GameObject HornJoe; // Horn
    // Enemy Claw
    [SerializeField] private GameObject Scrattach; // Claw

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
        RandomizeEnemySpawns();
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

    private void RandomizeEnemySpawns()
    {
        foreach(Transform fishySpawn in fishySpawns)
        {
            int randomFishy = Random.Range(0, 3);
            if (randomFishy == 0) { Instantiate(PricklePrish, fishySpawn.transform); }
            if (randomFishy == 1) { Instantiate(Hornfin, fishySpawn.transform); }
            if (randomFishy == 2) { Instantiate(Scrashark, fishySpawn.transform); }
        }

        foreach (Transform sneakySpawn in sneakySpawns)
        {
            int randomSneaky = Random.Range(0, 3);
            if (randomSneaky == 0) { Instantiate(SneakyPrickle, sneakySpawn.transform); }
            if (randomSneaky == 1) { Instantiate(Hornshrub, sneakySpawn.transform); }
            if (randomSneaky == 2) { Instantiate(Scrachthorn, sneakySpawn.transform); }
        }

        foreach (Transform generalEnemySpawn in generalEnemySpawns)
        {
            int randomGeneralEnemy = Random.Range(0, 3);
            if (randomGeneralEnemy == 0) { Instantiate(PricklePrickle, generalEnemySpawn.transform); }
            if (randomGeneralEnemy == 1) { Instantiate(HornJoe, generalEnemySpawn.transform); }
            if (randomGeneralEnemy == 2) { Instantiate(Scrattach, generalEnemySpawn.transform); }
        }

    }


}
