using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Image joystickImage;
    private CharacterController controller;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator drillAnim;
    [SerializeField] TextMeshProUGUI tmpro;
    private Drill drill;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 2.0f;
    private Vector3 move;
    private bool canMove;

    private int goldAmount;
    private int goldAmountTemp;
    [SerializeField] private float addGoldTimerMax = .2f;
    private float addGoldTimer;

    public int GoldAmount { get => goldAmount; set => goldAmount = value; }
    public Drill Drill { get => drill; set => drill = value; }
    public Animator DrillAnim { get => drillAnim; set => drillAnim = value; }
    public TextMeshProUGUI Tmpro { get => tmpro; set => tmpro = value; }
    public int GoldAmountTemp { get => goldAmountTemp; set => goldAmountTemp = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        drill = GetComponentInChildren<Drill>();
        joystickImage.enabled = false;
        canMove = true;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(move.x, 0, move.y);
        controller.Move(moveDir * Time.fixedDeltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = moveDir;
            playerAnim.SetBool("Walking", true);
        }
        else playerAnim.SetBool("Walking", false);

        controller.Move(playerVelocity * Time.fixedDeltaTime);

        UpdateGold();
    }

    public void GetGold(int amount)
    {
        goldAmount += amount;
    }

    private void UpdateGold()
    {
        if (goldAmount > goldAmountTemp)
        {
            if (addGoldTimer < addGoldTimerMax)
            {
                addGoldTimer += Time.fixedDeltaTime;
            }
            else
            {
                goldAmountTemp++;
                //Debug.Log(goldAmountTemp);
                tmpro.text = goldAmountTemp.ToString();
                addGoldTimer = 0;
            }
        }
    }

    private void OnMove(InputValue v)
    {
        move = v.Get<Vector2>();
    }

    private void OnTouch(InputValue v)
    {
        joystickImage.enabled = true;
    }

    private void OnRelease(InputValue v)
    {
        joystickImage.enabled = false;
    }

}
