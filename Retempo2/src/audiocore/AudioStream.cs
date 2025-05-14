using PortAudioSharp;

namespace Retempo2
{
	public class AudioStream
	{
		private static bool initializedPortAudio;

		public AudioStream()
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
			MessageBox.Show(deviceInfo.ToString());
		}
	}
}