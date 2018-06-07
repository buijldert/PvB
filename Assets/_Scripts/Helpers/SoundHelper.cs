using UnityEngine;

namespace RR.Helpers
{
    /// <summary>
    /// This class is responsible for converting volume to decibels and vice versa.
    /// </summary>
    public static class SoundHelper
    {
        /// <summary>
        /// Function to translate volume to decibel.
        /// </summary>
        /// <param name="_volume">Volume float</param>
        public static float VolumeToDecibel(float _volume)
        {
            if (_volume > 0f)
            {
                return 20f * Mathf.Log10(_volume);
            }
            return -80f;
        }

        /// <summary>
        /// Function to translate decibel to volume.
        /// </summary>
        /// <param name="decibel">Decibel float</param>
        public static float DecibelToVolume(float _decibel)
        {
            return Mathf.Pow(10f, _decibel / 20f);
        }
    }
}