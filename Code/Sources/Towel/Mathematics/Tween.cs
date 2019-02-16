namespace Towel.Mathematics
{
	//public class Tween
	//{
	//	public enum Interpolation { Linear, EaseOutExpo, EaseInExpo, EaseOutCirc, EaseInCirc }

	//	double _original;
	//	double _distance;
	//	double _current;
	//	double _totaldoubleimePassed = 0;
	//	double _totalDuration = 5;
	//	bool _finished = false;
	//	TweenFunction _tweenF = null;

	//	private delegate double TweenFunction(double timePassed, double start, double distance, double duration);

	//	public double Value { get { return _current; } }

	//	public bool IsFinished { get { return _finished; } }

	//	public Tween(double start, double end, double time) { Construct(start, end, time, Interpolation.Linear); }

	//	public Tween(double start, double end, double time, Interpolation tweenF) { Construct(start, end, time, tweenF); }

	//	private void Construct(double start, double end, double time, Interpolation tweenF)
	//	{
	//		_distance = end - start;
	//		_original = start;
	//		_current = start;
	//		_totalDuration = time;

	//		switch (tweenF)
	//		{
	//			case Interpolation.Linear:
	//				_tweenF = Tween.Linear;
	//				break;
	//			case Interpolation.EaseOutExpo:
	//				_tweenF = Tween.EaseOutExpo;
	//				break;
	//			case Interpolation.EaseInExpo:
	//				_tweenF = Tween.EaseInExpo;
	//				break;
	//			case Interpolation.EaseOutCirc:
	//				_tweenF = Tween.EaseOutCirc;
	//				break;
	//			case Interpolation.EaseInCirc:
	//				_tweenF = Tween.EaseInCirc;
	//				break;
	//		}
	//	}

	//	public void Update(double elapseddoubleime)
	//	{
	//		_totaldoubleimePassed += elapseddoubleime;
	//		_current = _tweenF(_totaldoubleimePassed, _original, _distance, _totalDuration);

	//		if (_totaldoubleimePassed > _totalDuration)
	//		{
	//			_current = _original + _distance;
	//			_finished = true;
	//		}
	//	}

	//	private static double Linear(double timePassed, double start, double distance, double duration)
	//	{
	//		return distance * timePassed / duration + start;
	//	}

	//	private static double EaseOutExpo(double timePassed, double start, double distance, double duration)
	//	{
	//		if (timePassed == duration)
	//			return start + distance;
	//		return (float)(distance * (-Arithmetic.Power(2, -10 * timePassed / duration) + 1) + start);
	//	}

	//	private static double EaseInExpo(double timePassed, double start, double distance, double duration)
	//	{
	//		if (timePassed == 0)
	//			return start;
	//		return (float)(distance * Arithmetic.Power(2, 10 * (timePassed / duration - 1)) + start);
	//	}

	//	private static double EaseOutCirc(double timePassed, double start, double distance, double duration)
	//	{
	//		return (float)(distance * Algebra.sqrt(1 - (timePassed = timePassed / duration - 1) * timePassed) + start);
	//	}

	//	private static double EaseInCirc(double timePassed, double start, double distance, double duration)
	//	{
	//		return (float)(-distance * (Algebra.sqrt(1 - (timePassed /= duration) * timePassed) - 1) + start);
	//	}
	//}
}