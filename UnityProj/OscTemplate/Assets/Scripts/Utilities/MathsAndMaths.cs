using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsAndMaths{

	// MATHS >>>

	public static float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}

	public static float NextGaussian() {
		float v1, v2, s;
		do {
			v1 = 2.0f * Random.Range(0f,1f) - 1.0f;
			v2 = 2.0f * Random.Range(0f,1f) - 1.0f;
			s = v1 * v1 + v2 * v2;
		} while (s >= 1.0f || s == 0f);

		s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

		if ((v1 *s) < -1f || (v1 * s) > 1) return NextGaussian();
		else return v1 * s;
	}
}
