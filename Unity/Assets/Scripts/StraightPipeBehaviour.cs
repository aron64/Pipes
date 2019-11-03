using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Minden T cső tulajdonságosztálya
/// </summary>
public class StraightPipeBehaviour : MonoBehaviour
{
    /// <summary>
    /// IEnumerátornak, kapcsoló
    /// </summary>
    public bool wait;
    /// <summary>
    /// Egy cső elforgatása
    /// </summary>
    public void Turn()
    {
        GetComponent<Pipe>().flowDir = new int[,] { };
        int y = (int)Mathf.Round(transform.rotation.eulerAngles.y);

        switch ((y / 90)%2)
        {
            case 0:
                GetComponent<Pipe>().flowDir = new int[,] { { 1, -1 }, { 0, 0 } };
                break;
            case 1:
                GetComponent<Pipe>().flowDir = new int[,] { { 0, 0 }, { -1, 1 } };
                break;
            default:break;
        }
    }

    /// <summary>
    /// Egy cső elfordítás animálása
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator TurnOverTime()
    {
        wait = true;
        for (int i = 0; i < 9; i++)
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 10f, 0f);
            yield return new WaitForEndOfFrame();
        }
        wait = false;
    }
}
