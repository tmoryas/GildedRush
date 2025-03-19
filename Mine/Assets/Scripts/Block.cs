using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private PlayerController player;
    private QuestManager questManager;
    [SerializeField] private GameObject goldParticles;
    [SerializeField] private GameObject rockParticles;
    private bool mined;
    [SerializeField] private float maxHP = 5f;
    private float currentHP;
    private float mineDamage = 1f;
    [SerializeField] private int goldAmount = 10;

    [SerializeField] private Image healthCircle;

    [SerializeField] private bool isFirstBlock;

    [SerializeField] private ParticleSystem miningParticles;

    public bool Mined { get => mined; set => mined = value; }
    public float MineDamage { get => mineDamage; set => mineDamage = value; }
    public ParticleSystem MiningParticles { get => miningParticles; set => miningParticles = value; }

    private void Start()
    {
        currentHP = maxHP;
        player = FindObjectOfType<PlayerController>();
        questManager = FindObjectOfType<QuestManager>();
        miningParticles.Stop();
    }

    void FixedUpdate()
    {
        if (currentHP <= 0)
        {
            player.GetGold(goldAmount);
            Instantiate(goldParticles, transform.position, Quaternion.identity);
            Instantiate(rockParticles, transform.position, Quaternion.identity);
            player.DrillAnim.SetBool("DrillActive", false);
            if (isFirstBlock) questManager.NextQuest();
            gameObject.SetActive(false);
        }
        if (mined)
        {
            currentHP -= Time.fixedDeltaTime * mineDamage;
            healthCircle.enabled = true;
            SetHealth();
        }
    }

    void SetHealth()
    {
        healthCircle.fillAmount = currentHP / maxHP;
    }
}
