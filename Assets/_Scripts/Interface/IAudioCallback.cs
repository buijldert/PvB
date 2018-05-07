public interface IAudioCallback
{
    void onOnbeatDetected();
    void onSpectrum(float[] spectrum);
}