using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public Animator textAnimator;
    public void TriggerCameraShake()
    {
        textAnimator.SetTrigger("ScreenShake");
    }

    public void ResetTriggerCameraShake()
    {
        textAnimator.SetTrigger("StopScreenShake");

    }
    public void SetSnareTrigger()
    {
        textAnimator.SetTrigger("SnareTrigger");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

}
