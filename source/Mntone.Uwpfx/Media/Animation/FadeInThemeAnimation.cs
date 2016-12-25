using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Mntone.Uwpfx.Media.Animation
{
	public sealed class FadeInThemeAnimation : DoubleAnimationUsingKeyFrames
	{
		private const double _delay = 0.0;
		private const double _duration = 250.0;
		private const double _begin = 0.0;
		private const double _end = 1.0;

		private static readonly TimeSpan _delayTimeSpan = TimeSpan.FromMilliseconds(_delay);
		private static readonly TimeSpan _durationTimeSpan = TimeSpan.FromMilliseconds(_delay + _duration);

		[ThreadStatic]
		private static DoubleKeyFrame _first;
		[ThreadStatic]
		private static DoubleKeyFrame _second;

		public FadeInThemeAnimation()
		{
			if (_first == null) Setup();


			KeyFrames.Add(_first);
			KeyFrames.Add(_second);
		}

		private void Setup()
		{
			//_keySpline = new KeySpline(0.1, 0.9, 0.2, 1.0);
			_first = new DiscreteDoubleKeyFrame(_begin, KeyTime.FromTimeSpan(_delayTimeSpan));
			_second = new LinearDoubleKeyFrame(_end, KeyTime.FromTimeSpan(_durationTimeSpan));
		}

		protected override Freezable CreateInstanceCore() => new FadeInThemeAnimation();
	}
}
