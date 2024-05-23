using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private List<UIControl> playerUIControllers;
    [SerializeField] private List<GameObject> characters;

    public void AddUIController(UIControl controller)
    {
        playerUIControllers.Add(controller);
    }

    public void NotifyReady()
    {
        bool allReady = true;
        foreach (UIControl player in playerUIControllers)
        {
            allReady &= player.isReady;
        }
        if (allReady)
        {
            GameManager.instance.ChangeToGameScene();
        }
    }

}
