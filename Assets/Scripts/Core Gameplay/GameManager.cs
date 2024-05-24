using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int desiredFramerate;
    [Space(20)]
    public static GameManager instance;
    
    public List<GameObject> players;
    public PlayerManager1 playerManager; //Probablemente esto acabe siendo static

    public GameObject character;

    [Space(20)]
    private double chrono = 0;
    public double gameTime = 300;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = desiredFramerate; //Igual sólo hay que usar uno de los dos
        QualitySettings.vSyncCount = 1; 
    }

    private void Update()
    {
        chrono += Time.deltaTime;
        if (chrono >= gameTime)
        {
            EndGame(null);
        }
    }

    public void UpdatePlayers()
    {
        players.Clear();
        foreach (PlayerInput playerInput in playerManager.players)
        {
            //Debug.Log(playerInput.name);
            GameObject player = playerInput.gameObject;
            //Debug.Log(player.name);
            players.Add(player);
            //DontDestroyOnLoad(player.transform.parent.gameObject);
            DontDestroyOnLoad(player);
            
        }
    }

    public void ChangeToGameScene()
    {
        GetComponent<PlayerInputManager>().DisableJoining();
        AddCharacters();
        playerManager.ManageLayers();
        playerManager.initializeCamera();
        playerManager.SetUpEvents();
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
            player.transform.Find("Character").gameObject.SetActive(true);
            player.transform.Find("UIControl").gameObject.SetActive(false);
        }
        SceneManager.LoadScene("SampleScene");
        //Provisional

        //foreach (GameObject player in players)
        //{
        //    player.transform.position = new Vector3(0,10,0);
        //}
    }

    public void AddCharacters()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Instantiate(character, players[i].transform.Find("Character"));
            Transform target = players[i].transform.Find("Character/CharacterModel(Clone)");
            players[i].GetComponentInChildren<CinemachineFreeLook>().Follow = target;
            players[i].GetComponentInChildren<CinemachineFreeLook>().LookAt = target;
        }
    }

    public void StartGame()
    {
        players[Random.Range(0, players.Count)].GetComponent<PointManager>().isTarget = true;
    }

    public void EndGame(GameObject winner)
    {
        chrono = 0;
        Debug.Log("The game is over");
        //Poner los jugadores a 100 puntos
        SceneManager.LoadScene("Movement");
        Debug.Log(playerManager.players.Count);   
    }
}
