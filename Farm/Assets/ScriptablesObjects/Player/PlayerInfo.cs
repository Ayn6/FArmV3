using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Data/Player")]
public class PlayerInfo : ScriptableObject
{
    public static float EXP = 100;

    public static int money = 30;
    public static float exp = 0;
    public static int level = 1;
}
