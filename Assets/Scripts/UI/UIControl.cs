using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private PlayerInput playerInput;
    private GameObject sharedPanel;
    private GameObject selfPanel;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        playerInput = transform.parent.GetComponent<PlayerInput>();
        //Debug.Log("Player index: " + playerInput.playerIndex);
        playerInput.SwitchCurrentActionMap("UI");
        sharedPanel = GameObject.Find("CharacterSelectionPanel");
        selfPanel = sharedPanel.transform.GetChild(playerInput.playerIndex).gameObject;
        selfPanel.GetComponent<Image>().color = Color.blue;
        //GameManager.instance.ChangeToGameScene();
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(--index);
            Debug.Log(selfPanel.transform.childCount);
            TextMeshProUGUI tmp = selfPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            Debug.Log(tmp);
            tmp.text = index.ToString();
        }

    }
    
}
