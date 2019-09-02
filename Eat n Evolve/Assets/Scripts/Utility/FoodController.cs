using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodController : MonoBehaviour
{
    // Food EP Stats
    private float claws;
    private float horns;
    private float spike;
    private float fishy;
    private float sneaky;
    // Parent that has EP stats we reading from
    private MeleeAI meleeAI;

  

    // Start is called before the first frame update
    void Start()
    {
        meleeAI = GetComponentInParent<MeleeAI>();
        claws = meleeAI.Claws;
        horns = meleeAI.Horns;
        spike = meleeAI.Spike;
        fishy = meleeAI.Fishy;
        sneaky = meleeAI.Sneaky;
 
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.Instance.Claws += claws;
            PlayerController.Instance.clawsStat.CurrentStatValue = PlayerController.Instance.Claws;
            PlayerController.Instance.Horns += horns;
            PlayerController.Instance.hornsStat.CurrentStatValue = PlayerController.Instance.Horns;
            PlayerController.Instance.Spike += spike;
            PlayerController.Instance.spikeStat.CurrentStatValue = PlayerController.Instance.Spike;
            PlayerController.Instance.Fishy += fishy;
            PlayerController.Instance.fishyStat.CurrentStatValue = PlayerController.Instance.Fishy;
            PlayerController.Instance.Sneaky += sneaky;
            PlayerController.Instance.sneakyStat.CurrentStatValue = PlayerController.Instance.Sneaky;
            if (PlayerController.Instance.Health <= 100)
            {
                PlayerController.Instance.Health += 5f;
                PlayerController.Instance.healthStat.CurrentHp = PlayerController.Instance.Health;
            }


            SceneManager.Instance.EnemyCount--;
            Destroy(transform.parent.gameObject);
        }
    }



 
}
// Use if statments with tags to decrement the correct thing. 