﻿using OpenBveApi.Routes;
using OpenBveApi.Trains;

namespace RouteManager2.Events
{
	/// <summary>Called when the rail played for a train should be changed</summary>
		public class RailSoundsChangeEvent : GeneralEvent
		{
			private readonly int PreviousRunIndex;
			private readonly int PreviousFlangeIndex;
			private readonly int NextRunIndex;
			private readonly int NextFlangeIndex;

			public RailSoundsChangeEvent(double TrackPositionDelta, int PreviousRunIndex, int PreviousFlangeIndex, int NextRunIndex, int NextFlangeIndex) : base(TrackPositionDelta)
			{
				this.DontTriggerAnymore = false;
				this.PreviousRunIndex = PreviousRunIndex;
				this.PreviousFlangeIndex = PreviousFlangeIndex;
				this.NextRunIndex = NextRunIndex;
				this.NextFlangeIndex = NextFlangeIndex;
			}

			/// <summary>Triggers a change in run and flange sounds</summary>
			/// <param name="direction">The direction of travel- 1 for forwards, and -1 for backwards</param>
			/// <param name="trackFollower">The TrackFollower</param>
			public override void Trigger(int direction, TrackFollower trackFollower)
			{
				AbstractCar car = trackFollower.Car;

				switch (trackFollower.TriggerType)
				{
					case EventTriggerType.FrontCarFrontAxle:
					case EventTriggerType.OtherCarFrontAxle:
						if (direction < 0)
						{
							car.FrontAxle.RunIndex = this.PreviousRunIndex;
							car.FrontAxle.FlangeIndex = this.PreviousFlangeIndex;
						}
						else if (direction > 0)
						{
							car.FrontAxle.RunIndex = this.NextRunIndex;
							car.FrontAxle.FlangeIndex = this.NextFlangeIndex;
						}
						break;
					case EventTriggerType.RearCarRearAxle:
					case EventTriggerType.OtherCarRearAxle:
						if (direction < 0)
						{
							car.RearAxle.RunIndex = this.PreviousRunIndex;
							car.RearAxle.FlangeIndex = this.PreviousFlangeIndex;
						}
						else if (direction > 0)
						{
							car.RearAxle.RunIndex = this.NextRunIndex;
							car.RearAxle.FlangeIndex = this.NextFlangeIndex;
						}
						break;
				}
			}
		}
}
