using System;
using System.Text;

namespace ExifLibrary
{
	public static class MathEx
	{
		public struct Fraction32 : IComparable<Fraction32>, IEquatable<Fraction32>, IComparable, IFormattable
		{
			private const int DefaultPrecision = 8;

			private bool mIsNegative;

			private int mNumerator;

			private int mDenominator;

			public static readonly Fraction32 NaN = new Fraction32(0, 0);

			public static readonly Fraction32 NegativeInfinity = new Fraction32(-1, 0);

			public static readonly Fraction32 PositiveInfinity = new Fraction32(1, 0);

			public int Numerator
			{
				get
				{
					return ((!mIsNegative) ? 1 : (-1)) * mNumerator;
				}
				set
				{
					if (value < 0)
					{
						mIsNegative = true;
						mNumerator = -1 * value;
					}
					else
					{
						mIsNegative = false;
						mNumerator = value;
					}
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public int Denominator
			{
				get
				{
					return mDenominator;
				}
				set
				{
					mDenominator = Math.Abs(value);
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public bool IsNegative
			{
				get
				{
					return mIsNegative;
				}
				set
				{
					mIsNegative = value;
				}
			}

			public Fraction32(int numerator, int denominator)
			{
				mIsNegative = false;
				if (numerator < 0)
				{
					numerator = -numerator;
					mIsNegative = !mIsNegative;
				}
				if (denominator < 0)
				{
					denominator = -denominator;
					mIsNegative = !mIsNegative;
				}
				mNumerator = numerator;
				mDenominator = denominator;
				if (mDenominator != 0)
				{
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public Fraction32(int numerator)
			{
				this = new Fraction32(numerator, 1);
			}

			public Fraction32(Fraction32 f)
			{
				this = new Fraction32(f.Numerator, f.Denominator);
			}

			public Fraction32(float value)
			{
				this = new Fraction32((double)value);
			}

			public Fraction32(double value)
			{
				this = new Fraction32(FromDouble(value, 8));
			}

			public Fraction32(string s)
			{
				this = new Fraction32(FromString(s));
			}

			public static bool IsNan(Fraction32 f)
			{
				return f.Numerator == 0 && f.Denominator == 0;
			}

			public static bool IsNegativeInfinity(Fraction32 f)
			{
				return f.Numerator < 0 && f.Denominator == 0;
			}

			public static bool IsPositiveInfinity(Fraction32 f)
			{
				return f.Numerator > 0 && f.Denominator == 0;
			}

			public static bool IsInfinity(Fraction32 f)
			{
				return f.Denominator == 0;
			}

			public static Fraction32 Inverse(Fraction32 f)
			{
				return new Fraction32(f.Denominator, f.Numerator);
			}

			public static Fraction32 Parse(string s)
			{
				return FromString(s);
			}

			public static bool TryParse(string s, out Fraction32 f)
			{
				try
				{
					f = Parse(s);
					return true;
					IL_0013:
					bool result;
					return result;
				}
				catch
				{
					f = default(Fraction32);
					return false;
					IL_0027:
					bool result;
					return result;
				}
			}

			public void Set(int numerator, int denominator)
			{
				mIsNegative = false;
				if (numerator < 0)
				{
					mIsNegative = !mIsNegative;
					numerator = -numerator;
				}
				if (denominator < 0)
				{
					mIsNegative = !mIsNegative;
					denominator = -denominator;
				}
				mNumerator = numerator;
				mDenominator = denominator;
				Reduce(ref mNumerator, ref mDenominator);
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is Fraction32)
				{
					return Equals((Fraction32)obj);
				}
				return false;
			}

			public bool Equals(Fraction32 obj)
			{
				return mIsNegative == obj.IsNegative && mNumerator == obj.Numerator && mDenominator == obj.Denominator;
			}

			public override int GetHashCode()
			{
				return mDenominator ^ (((!mIsNegative) ? 1 : (-1)) * mNumerator);
			}

			public string ToString(string format, IFormatProvider formatProvider)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((((!mIsNegative) ? 1 : (-1)) * mNumerator).ToString(format, formatProvider));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(format, formatProvider));
				return stringBuilder.ToString();
			}

			public string ToString(string format)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((((!mIsNegative) ? 1 : (-1)) * mNumerator).ToString(format));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(format));
				return stringBuilder.ToString();
			}

			public string ToString(IFormatProvider formatProvider)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((((!mIsNegative) ? 1 : (-1)) * mNumerator).ToString(formatProvider));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(formatProvider));
				return stringBuilder.ToString();
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((((!mIsNegative) ? 1 : (-1)) * mNumerator).ToString());
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString());
				return stringBuilder.ToString();
			}

			public int CompareTo(object obj)
			{
				if (!(obj is Fraction32))
				{
					throw new ArgumentException("obj must be of type Fraction", "obj");
				}
				return CompareTo((Fraction32)obj);
			}

			public int CompareTo(Fraction32 obj)
			{
				if (this < obj)
				{
					return -1;
				}
				if (this > obj)
				{
					return 1;
				}
				return 0;
			}

			private static Fraction32 FromDouble(double value, int precision)
			{
				if (double.IsNaN(value))
				{
					return NaN;
				}
				if (double.IsNegativeInfinity(value))
				{
					return NegativeInfinity;
				}
				if (double.IsPositiveInfinity(value))
				{
					return PositiveInfinity;
				}
				bool flag = value < 0.0;
				if (flag)
				{
					value = 0.0 - value;
				}
				int num = (int)Math.Pow(10.0, precision);
				int num2 = (int)(value * (double)num);
				int num3 = num;
				int num4 = (int)GCD((uint)num2, (uint)num3);
				num2 /= num4;
				num3 /= num4;
				return new Fraction32(((!flag) ? 1 : (-1)) * num2, num3);
			}

			private static Fraction32 FromString(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException("s");
				}
				string[] array = s.Split('/');
				int result = 1;
				int num = 1;
				if (array.Length == 1)
				{
					if (!int.TryParse(array[0], out result))
					{
						double value = double.Parse(array[0]);
						return FromDouble(value, 8);
					}
					num = 1;
				}
				else
				{
					if (array.Length != 2)
					{
						throw new FormatException("The input string must be formatted as n/d where n and d are integers");
					}
					result = int.Parse(array[0]);
					num = int.Parse(array[1]);
				}
				return new Fraction32(result, num);
			}

			private static void Reduce(ref int numerator, ref int denominator)
			{
				uint num = GCD((uint)numerator, (uint)denominator);
				if (num == 0)
				{
					num = 1u;
				}
				numerator /= (int)num;
				denominator /= (int)num;
			}

			public static Fraction32 operator *(Fraction32 f, int n)
			{
				return new Fraction32(f.Numerator * n, f.Denominator * Math.Abs(n));
			}

			public static Fraction32 operator *(int n, Fraction32 f)
			{
				return f * n;
			}

			public static Fraction32 operator *(Fraction32 f, float n)
			{
				return new Fraction32((float)f * n);
			}

			public static Fraction32 operator *(float n, Fraction32 f)
			{
				return f * n;
			}

			public static Fraction32 operator *(Fraction32 f, double n)
			{
				return new Fraction32((double)f * n);
			}

			public static Fraction32 operator *(double n, Fraction32 f)
			{
				return f * n;
			}

			public static Fraction32 operator *(Fraction32 f1, Fraction32 f2)
			{
				return new Fraction32(f1.Numerator * f2.Numerator, f1.Denominator * f2.Denominator);
			}

			public static Fraction32 operator /(Fraction32 f, int n)
			{
				return new Fraction32(f.Numerator / n, f.Denominator / Math.Abs(n));
			}

			public static Fraction32 operator /(Fraction32 f, float n)
			{
				return new Fraction32((float)f / n);
			}

			public static Fraction32 operator /(Fraction32 f, double n)
			{
				return new Fraction32((double)f / n);
			}

			public static Fraction32 operator /(Fraction32 f1, Fraction32 f2)
			{
				return f1 * Inverse(f2);
			}

			public static Fraction32 operator +(Fraction32 f, int n)
			{
				return f + new Fraction32(n, 1);
			}

			public static Fraction32 operator +(int n, Fraction32 f)
			{
				return f + n;
			}

			public static Fraction32 operator +(Fraction32 f, float n)
			{
				return new Fraction32((float)f + n);
			}

			public static Fraction32 operator +(float n, Fraction32 f)
			{
				return f + n;
			}

			public static Fraction32 operator +(Fraction32 f, double n)
			{
				return new Fraction32((double)f + n);
			}

			public static Fraction32 operator +(double n, Fraction32 f)
			{
				return f + n;
			}

			public static Fraction32 operator +(Fraction32 f1, Fraction32 f2)
			{
				int numerator = f1.Numerator;
				int denominator = f2.Denominator;
				int numerator2 = f2.Numerator;
				int denominator2 = f2.Denominator;
				return new Fraction32(numerator * denominator2 + numerator2 * denominator, denominator * denominator2);
			}

			public static Fraction32 operator -(Fraction32 f, int n)
			{
				return f - new Fraction32(n, 1);
			}

			public static Fraction32 operator -(int n, Fraction32 f)
			{
				return new Fraction32(n, 1) - f;
			}

			public static Fraction32 operator -(Fraction32 f, float n)
			{
				return new Fraction32((float)f - n);
			}

			public static Fraction32 operator -(float n, Fraction32 f)
			{
				return new Fraction32(n) - f;
			}

			public static Fraction32 operator -(Fraction32 f, double n)
			{
				return new Fraction32((double)f - n);
			}

			public static Fraction32 operator -(double n, Fraction32 f)
			{
				return new Fraction32(n) - f;
			}

			public static Fraction32 operator -(Fraction32 f1, Fraction32 f2)
			{
				int numerator = f1.Numerator;
				int denominator = f2.Denominator;
				int numerator2 = f2.Numerator;
				int denominator2 = f2.Denominator;
				return new Fraction32(numerator * denominator2 - numerator2 * denominator, denominator * denominator2);
			}

			public static Fraction32 operator ++(Fraction32 f)
			{
				return f + new Fraction32(1, 1);
			}

			public static Fraction32 operator --(Fraction32 f)
			{
				return f - new Fraction32(1, 1);
			}

			public static explicit operator int(Fraction32 f)
			{
				return f.Numerator / f.Denominator;
			}

			public static explicit operator float(Fraction32 f)
			{
				return (float)f.Numerator / (float)f.Denominator;
			}

			public static explicit operator double(Fraction32 f)
			{
				return (double)f.Numerator / (double)f.Denominator;
			}

			public static bool operator ==(Fraction32 f1, Fraction32 f2)
			{
				return f1.Numerator == f2.Numerator && f1.Denominator == f2.Denominator;
			}

			public static bool operator !=(Fraction32 f1, Fraction32 f2)
			{
				return f1.Numerator != f2.Numerator || f1.Denominator != f2.Denominator;
			}

			public static bool operator <(Fraction32 f1, Fraction32 f2)
			{
				return f1.Numerator * f2.Denominator < f2.Numerator * f1.Denominator;
			}

			public static bool operator >(Fraction32 f1, Fraction32 f2)
			{
				return f1.Numerator * f2.Denominator > f2.Numerator * f1.Denominator;
			}
		}

		public struct UFraction32 : IComparable<UFraction32>, IEquatable<UFraction32>, IComparable, IFormattable
		{
			private const int DefaultPrecision = 8;

			private uint mNumerator;

			private uint mDenominator;

			public static readonly UFraction32 NaN = new UFraction32(0u, 0u);

			public static readonly UFraction32 Infinity = new UFraction32(1u, 0u);

			public uint Numerator
			{
				get
				{
					return mNumerator;
				}
				set
				{
					mNumerator = value;
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public uint Denominator
			{
				get
				{
					return mDenominator;
				}
				set
				{
					mDenominator = value;
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public UFraction32(uint numerator, uint denominator)
			{
				mNumerator = numerator;
				mDenominator = denominator;
				if (mDenominator != 0)
				{
					Reduce(ref mNumerator, ref mDenominator);
				}
			}

			public UFraction32(uint numerator)
			{
				this = new UFraction32(numerator, 1u);
			}

			public UFraction32(UFraction32 f)
			{
				this = new UFraction32(f.Numerator, f.Denominator);
			}

			public UFraction32(float value)
			{
				this = new UFraction32((double)value);
			}

			public UFraction32(double value)
			{
				this = new UFraction32(FromDouble(value, 8));
			}

			public UFraction32(string s)
			{
				this = new UFraction32(FromString(s));
			}

			public static bool IsNan(UFraction32 f)
			{
				return f.Numerator == 0 && f.Denominator == 0;
			}

			public static bool IsInfinity(UFraction32 f)
			{
				return f.Denominator == 0;
			}

			public static UFraction32 Parse(string s)
			{
				return FromString(s);
			}

			public static bool TryParse(string s, out UFraction32 f)
			{
				try
				{
					f = Parse(s);
					return true;
					IL_0013:
					bool result;
					return result;
				}
				catch
				{
					f = default(UFraction32);
					return false;
					IL_0027:
					bool result;
					return result;
				}
			}

			public void Set(uint numerator, uint denominator)
			{
				mNumerator = numerator;
				mDenominator = denominator;
				Reduce(ref mNumerator, ref mDenominator);
			}

			public static UFraction32 Inverse(UFraction32 f)
			{
				return new UFraction32(f.Denominator, f.Numerator);
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is UFraction32)
				{
					return Equals((UFraction32)obj);
				}
				return false;
			}

			public bool Equals(UFraction32 obj)
			{
				return mNumerator == obj.Numerator && mDenominator == obj.Denominator;
			}

			public override int GetHashCode()
			{
				return (int)(mDenominator ^ mNumerator);
			}

			public string ToString(string format, IFormatProvider formatProvider)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(mNumerator.ToString(format, formatProvider));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(format, formatProvider));
				return stringBuilder.ToString();
			}

			public string ToString(string format)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(mNumerator.ToString(format));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(format));
				return stringBuilder.ToString();
			}

			public string ToString(IFormatProvider formatProvider)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(mNumerator.ToString(formatProvider));
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString(formatProvider));
				return stringBuilder.ToString();
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(mNumerator.ToString());
				stringBuilder.Append('/');
				stringBuilder.Append(mDenominator.ToString());
				return stringBuilder.ToString();
			}

			public int CompareTo(object obj)
			{
				if (!(obj is UFraction32))
				{
					throw new ArgumentException("obj must be of type UFraction32", "obj");
				}
				return CompareTo((UFraction32)obj);
			}

			public int CompareTo(UFraction32 obj)
			{
				if (this < obj)
				{
					return -1;
				}
				if (this > obj)
				{
					return 1;
				}
				return 0;
			}

			private static UFraction32 FromDouble(double value, int precision)
			{
				if (value < 0.0)
				{
					throw new ArgumentException("value cannot be negative.", "value");
				}
				if (double.IsNaN(value))
				{
					return NaN;
				}
				if (double.IsInfinity(value))
				{
					return Infinity;
				}
				uint num = (uint)Math.Pow(10.0, precision);
				uint num2 = (uint)(value * (double)num);
				uint num3 = num;
				uint num4 = GCD(num2, num3);
				num2 /= num4;
				num3 /= num4;
				return new UFraction32(num2, num3);
			}

			private static UFraction32 FromString(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException("s");
				}
				string[] array = s.Split('/');
				uint result = 1u;
				uint num = 1u;
				if (array.Length == 1)
				{
					if (!uint.TryParse(array[0], out result))
					{
						double value = double.Parse(array[0]);
						return FromDouble(value, 8);
					}
					num = 1u;
				}
				else
				{
					if (array.Length != 2)
					{
						throw new FormatException("The input string must be formatted as n/d where n and d are integers");
					}
					result = uint.Parse(array[0]);
					num = uint.Parse(array[1]);
				}
				return new UFraction32(result, num);
			}

			private static void Reduce(ref uint numerator, ref uint denominator)
			{
				uint num = GCD(numerator, denominator);
				numerator /= num;
				denominator /= num;
			}

			public static UFraction32 operator *(UFraction32 f, uint n)
			{
				return new UFraction32(f.Numerator * n, f.Denominator * n);
			}

			public static UFraction32 operator *(uint n, UFraction32 f)
			{
				return f * n;
			}

			public static UFraction32 operator *(UFraction32 f, float n)
			{
				return new UFraction32((float)f * n);
			}

			public static UFraction32 operator *(float n, UFraction32 f)
			{
				return f * n;
			}

			public static UFraction32 operator *(UFraction32 f, double n)
			{
				return new UFraction32((double)f * n);
			}

			public static UFraction32 operator *(double n, UFraction32 f)
			{
				return f * n;
			}

			public static UFraction32 operator *(UFraction32 f1, UFraction32 f2)
			{
				return new UFraction32(f1.Numerator * f2.Numerator, f1.Denominator * f2.Denominator);
			}

			public static UFraction32 operator /(UFraction32 f, uint n)
			{
				return new UFraction32(f.Numerator / n, f.Denominator / n);
			}

			public static UFraction32 operator /(UFraction32 f, float n)
			{
				return new UFraction32((float)f / n);
			}

			public static UFraction32 operator /(UFraction32 f, double n)
			{
				return new UFraction32((double)f / n);
			}

			public static UFraction32 operator /(UFraction32 f1, UFraction32 f2)
			{
				return f1 * Inverse(f2);
			}

			public static UFraction32 operator +(UFraction32 f, uint n)
			{
				return f + new UFraction32(n, 1u);
			}

			public static UFraction32 operator +(uint n, UFraction32 f)
			{
				return f + n;
			}

			public static UFraction32 operator +(UFraction32 f, float n)
			{
				return new UFraction32((float)f + n);
			}

			public static UFraction32 operator +(float n, UFraction32 f)
			{
				return f + n;
			}

			public static UFraction32 operator +(UFraction32 f, double n)
			{
				return new UFraction32((double)f + n);
			}

			public static UFraction32 operator +(double n, UFraction32 f)
			{
				return f + n;
			}

			public static UFraction32 operator +(UFraction32 f1, UFraction32 f2)
			{
				uint numerator = f1.Numerator;
				uint denominator = f2.Denominator;
				uint numerator2 = f2.Numerator;
				uint denominator2 = f2.Denominator;
				return new UFraction32(numerator * denominator2 + numerator2 * denominator, denominator * denominator2);
			}

			public static UFraction32 operator -(UFraction32 f, uint n)
			{
				return f - new UFraction32(n, 1u);
			}

			public static UFraction32 operator -(uint n, UFraction32 f)
			{
				return new UFraction32(n, 1u) - f;
			}

			public static UFraction32 operator -(UFraction32 f, float n)
			{
				return new UFraction32((float)f - n);
			}

			public static UFraction32 operator -(float n, UFraction32 f)
			{
				return new UFraction32(n) - f;
			}

			public static UFraction32 operator -(UFraction32 f, double n)
			{
				return new UFraction32((double)f - n);
			}

			public static UFraction32 operator -(double n, UFraction32 f)
			{
				return new UFraction32(n) - f;
			}

			public static UFraction32 operator -(UFraction32 f1, UFraction32 f2)
			{
				uint numerator = f1.Numerator;
				uint denominator = f2.Denominator;
				uint numerator2 = f2.Numerator;
				uint denominator2 = f2.Denominator;
				return new UFraction32(numerator * denominator2 - numerator2 * denominator, denominator * denominator2);
			}

			public static UFraction32 operator ++(UFraction32 f)
			{
				return f + new UFraction32(1u, 1u);
			}

			public static UFraction32 operator --(UFraction32 f)
			{
				return f - new UFraction32(1u, 1u);
			}

			public static explicit operator uint(UFraction32 f)
			{
				return f.Numerator / f.Denominator;
			}

			public static explicit operator float(UFraction32 f)
			{
				return (float)(double)f.Numerator / (float)(double)f.Denominator;
			}

			public static explicit operator double(UFraction32 f)
			{
				return (double)f.Numerator / (double)f.Denominator;
			}

			public static bool operator ==(UFraction32 f1, UFraction32 f2)
			{
				return f1.Numerator == f2.Numerator && f1.Denominator == f2.Denominator;
			}

			public static bool operator !=(UFraction32 f1, UFraction32 f2)
			{
				return f1.Numerator != f2.Numerator || f1.Denominator != f2.Denominator;
			}

			public static bool operator <(UFraction32 f1, UFraction32 f2)
			{
				return f1.Numerator * f2.Denominator < f2.Numerator * f1.Denominator;
			}

			public static bool operator >(UFraction32 f1, UFraction32 f2)
			{
				return f1.Numerator * f2.Denominator > f2.Numerator * f1.Denominator;
			}
		}

		public static uint GCD(uint a, uint b)
		{
			while (b != 0)
			{
				uint num = a % b;
				a = b;
				b = num;
			}
			return a;
		}
	}
}
