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
		private int framesPerBlock;
		private int bufferFrameIndex; // Index of the frame in the circular buffer
		private long globalBlockIndex; // Index of the block in the linear stream

        public delegate float[] GenerationCallback(double sampleRate, int channels, long startGlobalFrame, int framesPerBlock);
		private GenerationCallback generationCallback;

		private Stream stream;

		private void Generate(int blockIndex)
		{
			long startGlobalFrame = globalBlockIndex * framesPerBlock;
			float[] newBlock;
			if (generationCallback != null)
				newBlock = generationCallback(sampleRate, channels, startGlobalFrame, framesPerBlock);
			else
				newBlock = new float[framesPerBlock * channels]; // TODO: Trip an error

			int startIndex = blockIndex * framesPerBlock * channels;
			int endIndex = (blockIndex + 1) * framesPerBlock * channels;
			Array.Copy(newBlock, 0, circleBuffer, startIndex, endIndex - startIndex);
			globalBlockIndex++;
        }

		private StreamCallbackResult CircularBufferCallback(IntPtr input, IntPtr output, UInt32 frameCount,
			ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, IntPtr userData)
		{
			int currentBlockIndex = (bufferFrameIndex / framesPerBlock);
			int newBufferFrameIndex = (int)(bufferFrameIndex + frameCount);
			
			if (newBufferFrameIndex >= framesPerBlock * 4)
			{
				// We have to loop, so we do two copies
				int floatsAddedFirst = (framesPerBlock * 4 - bufferFrameIndex) * channels;
                Marshal.Copy(circleBuffer, bufferFrameIndex * channels, output, floatsAddedFirst);
				newBufferFrameIndex -= framesPerBlock * 4;
                Marshal.Copy(circleBuffer, 0, IntPtr.Add(output, floatsAddedFirst * sizeof(float)), newBufferFrameIndex * channels);
            }
			else
			{
                Marshal.Copy(circleBuffer, bufferFrameIndex * channels, output, (newBufferFrameIndex - bufferFrameIndex) * channels);
            }

			int newBlockIndex = (newBufferFrameIndex / framesPerBlock);
			bufferFrameIndex = newBufferFrameIndex;

			if (newBlockIndex != currentBlockIndex)
			{
				// We're done with the current block, we can regenerate it
				Generate(currentBlockIndex);
			}

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

			framesPerBlock = (int)Math.Ceiling(sampleRate * bufferLength / 4.0);
			circleBuffer = new float[4 * framesPerBlock * channels];

			generationCallback = DefaultCallback;

			Stream.Callback callback = CircularBufferCallback;

			stream = new Stream(inParams: null, outParams: parameters, sampleRate: sampleRate,
				framesPerBuffer: 0, streamFlags: StreamFlags.NoFlag, callback: callback, userData: IntPtr.Zero);
		}

		public void SetCallback(GenerationCallback callback)
		{
			generationCallback = callback;
		}

		public void Play()
        {
			if (stream.IsActive)
				stream.Stop();
			globalBlockIndex = 0;
            bufferFrameIndex = 0;
            for (int i = 0; i < 4; i++)
                Generate(i);
            stream.Start();
        }

        public void Stop()
        {
            if (stream.IsActive)
                stream.Stop();
        }

		private float[] DefaultCallback(double sampleRate, int channels, long startGlobalFrame, int framesPerBlock)
		{
			float[] output = new float[framesPerBlock * channels]; // Silent buffer
			return output;
		}
    }
}