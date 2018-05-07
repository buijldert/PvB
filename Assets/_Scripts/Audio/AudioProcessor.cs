using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioProcessor : MonoBehaviour
{
    private AudioSource source;

    private List<IAudioCallback> callbacks;

    public int bufferSize = 1024; // fft size
    private const int SAMPLING_RATE = 44100; // fft sampling frequency

    /* log-frequency averaging controls */
    private int bandAmount = 12; // number of bands

    [SerializeField] private float threshold = 0.1f; // sensitivity

    private int blipDelayLength = 16;
    private int[] blipDelay;

    private int sinceLast; // counter to suppress double-beats

    private float framePeriod;
    private float onset;
    private float smax;

    /* storage space */
    private const int COL_MAX = 120;
    private float[] onsets;
    private float[] scorefun;
    private float[] dobeat;
    private float[] averages;
    private int now; // time index for circular buffer within above
    private int tempopd;
    private int smaxix;
    

    private float[] spec; // the spectrum of the previous step

    /* Autocorrelation structure */
    private int maxlag = 100; // (in frames) largest lag to track
    private float decay = 0.997f; // smoothing constant for running average
    private Autoco autoco;

    private float alph; // trade-off constant between tempo deviation penalty and onset strength

    public static AudioProcessor instance;
    
    private void Awake()
    {
        callbacks = new List<IAudioCallback>();
        source = GetComponent<AudioSource>();

        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;
    }
    
    private void Start()
    {

        InitArrays();

        framePeriod = (float)bufferSize / (float)SAMPLING_RATE;

        //initialize record of previous spectrum
        spec = new float[bandAmount];
        for (int i = 0; i < bandAmount; ++i) spec[i] = 100.0f;

        autoco = new Autoco(maxlag, decay, framePeriod, GetBandWidth());
    }

    private void Update()
    {

        if (source.isPlaying)
        {
            float[] spectrum = new float[bufferSize];
            GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
            averages = ComputeAverages(spectrum);

            if (callbacks != null)
            {
                foreach (IAudioCallback callback in callbacks)
                {
                    callback.onSpectrum(averages);
                }
            }

            CalculateOnset();

            RecordLargestValue();

            CalculateDominantParallel();

            CalculateScore();
            
            CallListeners();

            /* update column index (for ring buffer) */
            if (++now == COL_MAX)
            {
                now = 0;
            }
        }
    }

    private void InitArrays()
    {
        blipDelay = new int[blipDelayLength];
        onsets = new float[COL_MAX];
        scorefun = new float[COL_MAX];
        dobeat = new float[COL_MAX];
        alph = 100 * threshold;
    }

    private void CalculateOnset()
    {
        /* calculate the value of the onset function in this frame */
        onset = 0;
        for (int i = 0; i < bandAmount; i++)
        {
            float specVal = (float)System.Math.Max(-100.0f, 20.0f * (float)System.Math.Log10(averages[i]) + 160); // dB value of this band
            specVal *= 0.025f;
            float dbInc = specVal - spec[i]; // dB increment since last frame
            spec[i] = specVal; // record this frome to use next time around
            onset += dbInc; // onset function is the sum of dB increments
        }

        onsets[now] = onset;

        /* update autocorrelator and find peak lag = current tempo */
        autoco.NewValue(onset);
    }

    private void RecordLargestValue()
    {
        // record largest value in (weighted) autocorrelation as it will be the tempo
        float aMax = 0.0f;
        tempopd = 0;
        float[] acVals = new float[maxlag];
        for (int i = 0; i < maxlag; ++i)
        {
            float acVal = (float)System.Math.Sqrt(autoco.AutocoValue(i));
            if (acVal > aMax)
            {
                aMax = acVal;
                tempopd = i;
            }
            // store in array backwards, so it displays right-to-left, in line with traces
            acVals[maxlag - 1 - i] = acVal;
        }
    }

    private void CalculateDominantParallel()
    {
        /* calculate DP-ish function to update the best-score function */
        smax = -999999;
        smaxix = 0;
        // weight can be varied dynamically with the mouse
        alph = 100 * threshold;
        // consider all possible preceding beat times from 0.5 to 2.0 x current tempo period
        for (int i = tempopd / 2; i < System.Math.Min(COL_MAX, 2 * tempopd); ++i)
        {
            // objective function - this beat's cost + score to last beat + transition penalty
            float score = onset + scorefun[(now - i + COL_MAX) % COL_MAX] - alph * (float)System.Math.Pow(System.Math.Log((float)i / (float)tempopd), 2);
            // keep track of the best-scoring predecesor
            if (score > smax)
            {
                smax = score;
                smaxix = i;
            }
        }

        scorefun[now] = smax;
    }

    private void CalculateScore()
    {
        // keep the smallest value in the score fn window as zero, by subtracing the min val
        float smin = scorefun[0];
        for (int i = 0; i < COL_MAX; ++i)
            if (scorefun[i] < smin)
                smin = scorefun[i];
        for (int i = 0; i < COL_MAX; ++i)
            scorefun[i] -= smin;

        /* find the largest value in the score fn window, to decide if we emit a blip */
        smax = scorefun[0];
        smaxix = 0;
        for (int i = 0; i < COL_MAX; ++i)
        {
            if (scorefun[i] > smax)
            {
                smax = scorefun[i];
                smaxix = i;
            }
        }

        // dobeat array records where we actally place beats
        dobeat[now] = 0;  // default is no beat this frame
        ++sinceLast;
    }

    private void CallListeners()
    {
        // if current value is largest in the array, probably means we're on a beat
        if (smaxix == now)
        {
            // make sure the most recent beat wasn't too recently
            if (sinceLast > tempopd / 4)
            {
                if (callbacks != null)
                {
                    foreach (IAudioCallback callback in callbacks)
                    {
                        callback.onOnbeatDetected();
                    }
                }
                blipDelay[0] = 1;
                // record that we did actually mark a beat this frame
                dobeat[now] = 1;
                // reset counter of frames since last beat
                sinceLast = 0;
            }
        }
    }

    private float GetBandWidth()
    {
        return (2f / (float)bufferSize) * (SAMPLING_RATE / 2f);
    }

    private int FreqToIndex(int freq)
    {
        // special case: freq is lower than the bandwidth of spectrum[0]
        if (freq < GetBandWidth() / 2) return 0;
        // special case: freq is within the bandwidth of spectrum[512]
        if (freq > SAMPLING_RATE / 2 - GetBandWidth() / 2) return (bufferSize / 2);
        // all other cases
        float fraction = (float)freq / (float)SAMPLING_RATE;
        int i = (int)System.Math.Round(bufferSize * fraction);

        return i;
    }

    private float[] ComputeAverages(float[] data)
    {
        float[] averages = new float[12];
        for (int i = 0; i < 12; i++)
        {
            float avg = 0;
            int lowFreq;
            if (i == 0)
                lowFreq = 0;
            else
                lowFreq = (int)((SAMPLING_RATE / 2) / (float)System.Math.Pow(2, 12 - i));
            int hiFreq = (int)((SAMPLING_RATE / 2) / (float)System.Math.Pow(2, 11 - i));
            int lowBound = FreqToIndex(lowFreq);
            int hiBound = FreqToIndex(hiFreq);
            for (int j = lowBound; j <= hiBound; j++)
            {
                avg += data[j];
            }
            // line has been changed since discussion in the comments
            // avg /= (hiBound - lowBound);
            avg /= (hiBound - lowBound + 1);
            averages[i] = avg;
        }

        return averages;
    }

    public void AddAudioCallback(IAudioCallback callback)
    {
        callbacks.Add(callback);
    }

    public void RemoveAudioCallback(IAudioCallback callback)
    {
        callbacks.Remove(callback);
    }
}