using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Mntone.Uwpfx.Media.Animation
{
	public sealed class FadeOutThemeAnimation : DoubleAnimationUsingKeyFrames
	{
		private const double _delay = 0.0;
		private const double _duration = 167.0;
		private const double _begin = 1.0;
		private const double _end = 0.0;

		private static readonly TimeSpan _delayTimeSpan = TimeSpan.FromMilliseconds(_delay);
		private static readonly TimeSpan _durationTimeSpan = TimeSpan.FromMilliseconds(_delay + _duration);
		
		[ThreadStatic]
		private static DoubleKeyFrame _first;
		[ThreadStatic]
		private static DoubleKeyFrame _second;

		public FadeOutThemeAnimation()
		{
			if (_first == null) Setup();

			
			KeyFrames.Add(_first);
			KeyFrames.Add(_second);
		}

		private void Setup()
		{
			_first = new DiscreteDoubleKeyFrame(_begin, KeyTime.FromTimeSpan(_delayTimeSpan));
			_second = new LinearDoubleKeyFrame(_end, KeyTime.FromTimeSpan(_durationTimeSpan));
		}

		protected override Freezable CreateInstanceCore() => new FadeOutThemeAnimation();
	}
}