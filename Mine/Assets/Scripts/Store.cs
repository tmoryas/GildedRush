using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    private PlayerController player;
    private QuestManager questManager;
    private Animator an;

    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI priceTxt;

    private int storeGold;
    private bool goldTransfer;

    [SerializeField] private float getGoldTimerMax = .08f;
    [SerializeField] private int upgradePrice = 30;
    [SerializeField] private float damageUpgrade = 4f;
    [SerializeField] private Vector3 scaleUpgrade = new Vector3(0.3f, 0.3f, 0.3f);
    private float getGoldTimer;
    private int remainingGold;

    [SerializeField] private bool speedStore;

    private void Start()
    {
        priceTxt.text = upgradePrice.ToString();
        questManager = FindObjectOfType<QuestManager>();
        an = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            player = other.GetComponent<PlayerController>();
            if (player.GoldAmount > 0 && storeGold < upgradePrice) goldTransfer = true;
            if (questManager.QuestActive) questManager.NextQuest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            goldTransfer = false;

        }
    }

    private void FixedUpdate()
    {
        if (goldTransfer)
        {
            if (getGoldTimer < getGoldTimerMax)
            {
                getGoldTimer += Time.fixedDeltaTime;
            }
            else
            {
                storeGold++;
                player.GoldAmount--;
                player.GoldAmountTemp = player.GoldAmount;
                player.Tmpro.text = player.GoldAmount.ToString();
                remainingGold = upgradePrice - storeGold;
                priceTxt.text = remainingGold.ToString();
                fillBar.fillAmount = (float)storeGold / (float)upgradePrice;
                getGoldTimer = 0;
                if (player.GoldAmount <= 0 ) goldTransfer = false;
                if (storeGold >= upgradePrice)
                {
                    goldTransfer = false;
                    if (speedStore)
                    {
                        player.PlayerSpeed += damageUpgrade;
                    }
                    else
                    {
                        player.Drill.Damage += damageUpgrade;
                        player.Drill.transform.localScale += scaleUpgrade;
                    }
                    an.SetTrigger("Close");
                }
            }
        }
    }
}
