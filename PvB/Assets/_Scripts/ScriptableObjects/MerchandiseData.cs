using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MerchandiseData : ScriptableObject 
{
    public Unlockable[] Unlockable;

    public Sprite GetUnlockableValue(string key)
    {
        try
        {
            return Unlockable.Single(a => a.Key == key).value;
        }
        catch
        {
            throw new KeyNotFoundException("There is no entry that has the given key.");
        }
    }
}
