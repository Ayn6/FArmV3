using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public static int TYPEFOOD = 1;
    public static int TYPEPLOW = 2;
    public static int TYPESHAVE = 3;

    public string name = "Item";
    public Sprite sprite;
    public Sprite badSprite;
    public int type = 1;
    public float time = 1;
    public int openLvl = 1;
    public int exp = 1;
    public int price = 1;
    public int count = 1;
}
