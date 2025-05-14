using System.Runtime.InteropServices;
using PortAudioSharp;
using Stream = PortAudioSharp.Stream;

namespace Retempo2
{
	public class AudioStream
	{
		private static bool initializedPortAudio;

		private double sampleRate;
		private int channels;
		private float bufferLength;

		private float[] circleBuffer;
		private int bufferIndex; // Index of a single float in the circular buffer
		private long frameIndex; // Index of the frame in the linear stream

		private Stream stream;

		private StreamCallbackResult CircularBufferCallback(IntPtr input, IntPtr output, UInt32 frameCount,
			ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, IntPtr userData)
		{
			float[] temp = new float[frameCount * channels];
			for (int i = 0; i < frameCount; i++)
			{
				for (int j = 0; j < channels; j++)
				{
					temp[i * channels + j] = MathF.Sin((float)(i * channels) / (float)(sampleRate) * 440.0f * 2.0f * MathF.PI) / 10.0f;
				}
			}
            Marshal.Copy(temp, 0, output, (int)(frameCount * channels));
			return StreamCallbackResult.Continue;
		}

		public AudioStream(float bufferLength = 1.0f)
		{
			if (!initializedPortAudio)
			{
				PortAudio.Initialize();
				initializedPortAudio = true;
			}

			int deviceIndex = PortAudio.DefaultOutputDevice;
			if (deviceIndex == PortAudio.NoDevice)
			{
				MessageBox.Show("No default output device???");
			}

			DeviceInfo deviceInfo = PortAudio.GetDeviceInfo(deviceIndex);
			sampleRate = deviceInfo.defaultSampleRate;
			channels = deviceInfo.maxOutputChannels;

			StreamParameters parameters = new StreamParameters();
			parameters.device = deviceIndex;
			parameters.channelCount = channels;
			parameters.sampleFormat = SampleFormat.Float32;
			parameters.suggestedLatency = deviceInfo.defaultLowOutputLatency;

			Stream.Callback callback = CircularBufferCallback;

			stream = new Stream(inParams: null, outParams: parameters, sampleRate: sampleRate,
				framesPerBuffer: 0, streamFlags: StreamFlags.NoFlag, callback: callback, userData: IntPtr.Zero);
			stream.Start();
		}
	}
}