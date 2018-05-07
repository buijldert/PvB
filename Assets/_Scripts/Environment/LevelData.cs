using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public AudioClip LevelAudio;

    [Range(0.0f, 1.0f)]
    public float LevelThreshold;
    public int BufferSize;
}