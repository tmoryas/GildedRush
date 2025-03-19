using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float damage = 1f;

    public float Damage { get => damage; set => damage = value; }

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            Block block = other.GetComponent<Block>();
            if (block.MineDamage != damage) block.MineDamage = damage;
            block.Mined = true;
            block.MiningParticles.Play();
            player.DrillAnim.SetBool("DrillActive", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            other.gameObject.GetComponent<Block>().Mined = false;
            other.gameObject.GetComponent<Block>().MiningParticles.Stop();
            player.DrillAnim.SetBool("DrillActive", false);
        }
    }

}
