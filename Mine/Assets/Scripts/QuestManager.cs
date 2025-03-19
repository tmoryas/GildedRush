using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    [SerializeField] private GameObject arrow;

    [SerializeField] private Transform firstGoal;
    [SerializeField] private Transform secondGoal;
    private Transform currentGoal;

    private bool questActive;
    [SerializeField] private bool isFirstLevel;

    public bool QuestActive { get => questActive; set => questActive = value; }

    private void Start()
    {
        questActive = true;
        currentGoal = firstGoal;
    }

    private void Update()
    {
        if (player != null && currentGoal != null && questActive && isFirstLevel)
        {
            arrow.transform.position = player.transform.position;
            arrow.transform.LookAt(currentGoal.position);
        }
    }

    public void StopQuest()
    {
        questActive = false;
        arrow.SetActive(false);
    }

    public void NextQuest()
    {
        if (currentGoal == firstGoal)
        {
            currentGoal = secondGoal;
        }
        else
        {
            StopQuest();
        }
    }
}
