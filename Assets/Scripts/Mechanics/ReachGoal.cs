using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachGoal : MonoBehaviour
{
    public void EndGame()
    {
        // Maybe have a little transistion before going to the End screen
        SceneManager.LoadScene("TheEnd");
    }
}
