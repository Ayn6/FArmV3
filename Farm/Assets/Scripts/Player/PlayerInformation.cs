using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] private RectTransform scale;
    [SerializeField] private TextMeshProUGUI lvl, expText;
    private float expUI;
    private void Start()
    {
        lvl.text = "Уровень " + PlayerInfo.level.ToString();
        AddExp(PlayerInfo.exp);
    }
    public void AddExp(float exp)
    {
        PlayerInfo.exp += exp;

        if (PlayerInfo.exp >= PlayerInfo.EXP) 
        {
            CountLvl();
            exp = PlayerInfo.exp = PlayerInfo.exp - PlayerInfo.EXP;         
            PlayerInfo.EXP = PlayerInfo.EXP * 1.75f;
            expUI = PlayerInfo.exp / PlayerInfo.EXP;
        }
        else
        {
            expUI = PlayerInfo.exp / PlayerInfo.EXP;
        }

        expText.text = PlayerInfo.exp.ToString() + " / " + PlayerInfo.EXP.ToString();
        scale.localScale = new Vector3(expUI, 1, 1);
    }

    public void CountLvl()
    {
        PlayerInfo.level++;
        lvl.text = "Уровень " + PlayerInfo.level.ToString();
    }
}
