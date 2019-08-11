using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    // Stores the quadrants for random generation
    [SerializeField] private List<Transform> quadrants;
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RandomizePlayerSpawn()
    {
        int randomSpawn = Random.Range(0, playerSpawns.Capacity);
        PlayerController.Instance.StartPosition = playerSpawns[randomSpawn];
    }


}
