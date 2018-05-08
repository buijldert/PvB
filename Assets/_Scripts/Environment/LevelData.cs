using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public AudioClip LevelAudio;
}