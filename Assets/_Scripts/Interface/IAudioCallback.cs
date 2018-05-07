public interface IAudioCallback
{
    void OnOnbeatDetected();
    void OnSpectrum(float[] _spectrum);
}