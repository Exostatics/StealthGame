using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metals : MonoBehaviour
{
    public static void Iron(PlayerManager player)
    {

    }

    public static void Steel(PlayerManager player)
    {

    }

    public static void Pewter(PlayerManager player)
    {
        player.strength *= 1.5f;
    }

    public static void Tin(PlayerManager player)
    {
        player.fov += 15f;
        player.visionRange += 30f;
    }
}
