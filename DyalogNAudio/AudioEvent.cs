using NAudio.Wave;

namespace DyalogNAudio
{
  public class AudioEvent
  {
    public int Requested { get; internal set; }
    public float[] Left { get; set; }
    public float[] Right { get; set; }
    public WaveFormat WaveFormat { get; internal set; }
    public long Time { get; internal set; }
    public double DTime { get => (double)Time; }
  }
}