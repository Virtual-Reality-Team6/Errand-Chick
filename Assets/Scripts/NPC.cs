using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public RectTransform uiGroup; //1-1. uiGroup
    //public Animator anim; //1-2. npc animation
    Player enterPlayer;

    public GameObject[] itemObj; //stamp
    public int[] itemPrice; //stamp point
    public Transform[] itemPos;
    public Text talkText;
    
    // Start is called before the first frame update
    public void Enter(Player player) //1-3. user in circle
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero; //window center 
    }

    // Update is called once per frame
    public void Exit() //1-4. user exit ui
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }
}
