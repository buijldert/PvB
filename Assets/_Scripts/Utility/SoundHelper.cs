using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This class is responsible for converting volume to decibels and vice versa.
    /// </summary>
    public static class SoundHelper
    {
        /// <summary>
        /// Function to translate volume to decibel.
        /// </summary>
        /// <param name="volume">Volume float</param>
        public static float VolumeToDecibel(float volume)
        {
            if (volume > 0f)
            {
                return 20f * Mathf.Log10(volume);
            }
            return -80f;
        }

        /// <summary>
        /// Function to translate decibel to volume.
        /// </summary>
        /// <param name="decibel">Decibel float</param>
        public static float DecibelToVolume(float decibel)
        {
            return Mathf.Pow(10f, decibel / 20f);
        }
    }
}