using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minecart : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Animator cartAnim;

    private void Start()
    {
        transitionAnim.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            StartCoroutine(StartMinecart());
            other.GetComponent<PlayerController>().CanMove = false;
            other.gameObject.SetActive(false);
        }
    }

    public IEnumerator StartMinecart()
    {
        cartAnim.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        transitionAnim.SetTrigger("Close");
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
