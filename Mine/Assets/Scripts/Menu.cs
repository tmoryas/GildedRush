using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Animator transiAnim;
    [SerializeField] private string nextSceneName;

    private void Start()
    {
        transiAnim.SetTrigger("Open");
    }

    public void PlayButton()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        transiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }
}
