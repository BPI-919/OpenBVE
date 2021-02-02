﻿using System.IO;
using System.Linq;
using OpenBveApi.Sounds;
using OpenBveApi.Trains;
using OpenTK.Audio.OpenAL;
using SoundManager;

namespace OpenBve
{
	internal partial class Sounds : SoundsBase
	{
		/// <summary>Stops all sounds that are attached to the specified train.</summary>
		/// <param name="train">The train.</param>
		public override void StopAllSounds(object train)
		{
			if (train is TrainManager.Train)
			{
				var t = (TrainManager.Train) train;
				for (int i = 0; i < SourceCount; i++)
				{
					if (t.Cars.Contains(Sources[i].Parent))
					{
						if (Sources[i].State == SoundSourceState.Playing)
						{
							AL.DeleteSources(1, ref Sources[i].OpenAlSourceName);
							Sources[i].OpenAlSourceName = 0;
						}
						Sources[i].State = SoundSourceState.Stopped;
					}
				}
			}
		}
	}
}
