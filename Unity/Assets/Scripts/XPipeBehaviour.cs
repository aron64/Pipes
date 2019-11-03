using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Minden X cső tulajdonságosztálya
/// </summary>
public class XPipeBehaviour : MonoBehaviour
{
    public bool wait;

    /// <summary>
    /// A cső elforgatása utáni folyásirányok
    /// (Ez esetben nem változik)
    /// </summary>
    public void Turn()
    {
        
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
            transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y + 10f, 0f);
            yield return new WaitForEndOfFrame();
        }
        wait = false;
    }
}
