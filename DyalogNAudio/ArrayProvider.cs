using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyalogNAudio
{
  public class ArrayProvider : ISampleProvider
  {
    public WaveFormat WaveFormat => waveFormat;
    readonly WaveFormat waveFormat;
    readonly AudioEvent _event;

    public event EventHandler<AudioEvent> Process;

    long _time = 0;

    public ArrayProvider()
    {
      waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
      _event = new AudioEvent {
        WaveFormat = waveFormat
      };
    }

    public int Read(float[] buffer, int offset, int count)
    {
      var frameSize = count / 2;
      var outIndex = offset;
      _event.Requested = frameSize;
      _event.Left = null;
      _event.Right = null;
      _event.Time = _time;

      Process(this, _event);
      if (_event.Right == null)
        _event.Right = _event.Left;

      int l = 0, r = 0;
      if (_event.Left != null)
        l = _event.Left.Length;
      if (_event.Right != null)
        r = _event.Right.Length;

      for (var n = 0; n < frameSize; n++) {
        if (n < l) {
          buffer[outIndex++] = _event.Left[n];
        } else buffer[outIndex++] = 0;

        if (n < r) {
          buffer[outIndex++] = _event.Right[n];
        } else buffer[outIndex++] = 0;
      }
      _time += frameSize;
      return count;
    }
  }
}
