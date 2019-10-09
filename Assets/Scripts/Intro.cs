using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Details for the current state of the intro scene
enum SceneState
{
    eTeam,
    eCPP,
    eTitle,
    MAX_STATES
}

// Intro.cs,
// @author Charles Tsao
//
// This script handles logic related to the intro scene
public class Intro : MonoBehaviour
{
    public Text m_SmallTopText;
    public Text m_SmallCenterText;
    public Image m_Image;
    public float m_SecondsBeforeLoading;

    private SceneState m_State;
    private bool m_Triggered = false;

    private void Start()
    {
        m_State = SceneState.eTeam;
        m_SmallTopText.text =
            "Byron Chen\n" +
            "Charles Tsao\n" +
            "Trent Williams\n\n\n\n\n";
        m_SmallCenterText.text = 
        "The ecoPineapple Team\nPresents";
    }

    private void Update()
    {
        switch (m_State)
        {
            case SceneState.eTeam:
                UpdateTeam();
                break;
            case SceneState.eCPP:
                UpdateCPP();
                break;
            case SceneState.eTitle:
                UpdateTitle();
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // At any time, let the user leave
            Application.Quit();
        }
        else if (Input.anyKeyDown)
        {
            // Skip the intro
            SceneManager.LoadScene("Menu");
        }
    }

    private void UpdateTeam()
    {
        Animator anim = m_SmallTopText.GetComponent<Animator>();
        Animator anim2 = m_SmallCenterText.GetComponent<Animator>();
        Animator anim3 = m_Image.GetComponent<Animator>();

        if (!m_Triggered && (anim != null) && (anim.GetCurrentAnimatorStateInfo(0).IsName("ReadyToAnimate")))
        {
            // Trigger the animation
            anim.SetTrigger("triggerAnim");
            anim2.SetTrigger("triggerAnim");
            anim3.SetTrigger("triggerAnim");
            m_Triggered = true;
        }
        else if (m_Triggered && (anim != null) && (anim.GetCurrentAnimatorStateInfo(0).IsName("FinishedAnimation")))
        {
            // Transition to the creation state
            anim.SetTrigger("resetAnim");
            anim2.SetTrigger("resetAnim");
            anim3.SetTrigger("resetAnim");
            m_Triggered = false;
            m_State = SceneState.eCPP;
            m_SmallCenterText.text = "in Association with \nCOMPUTER POWER PLUS";
        }
    }

    private void UpdateCPP()
    {
        Animator anim = m_SmallCenterText.GetComponent<Animator>();
        if (!m_Triggered && (anim != null) && (anim.GetCurrentAnimatorStateInfo(0).IsName("ReadyToAnimate")))
        {
            // Trigger the animation
            anim.SetTrigger("triggerAnim");
            m_Triggered = true;
        }
        else if (m_Triggered && (anim != null) && (anim.GetCurrentAnimatorStateInfo(0).IsName("FinishedAnimation")))
        {
            // Transition to the title state
            anim.SetTrigger("resetAnim");
            m_Triggered = false;
            m_State = SceneState.eTitle;
        }
    }

    private void UpdateTitle()
    {
        // Let's wait a bit before loading; this gives the song a chance to finish
        m_SecondsBeforeLoading -= Time.deltaTime;
        if (m_SecondsBeforeLoading <= 0.0f)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}
