using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Cache
{
    /// <summary>
    /// 缓存返回值包装
    /// </summary>
    public struct CacheValue : IEquatable<CacheValue>, IComparable<CacheValue>, IComparable, IConvertible
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(CacheValue other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(CacheValue other)
        {
            throw new NotImplementedException();
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}



//using System;
//using System.Runtime.CompilerServices;
//using System.Text;

//namespace StackExchange.Redis
//{
//	/// <summary>
//	/// Represents values that can be stored in redis
//	/// </summary>
//	public struct RedisValue : IEquatable<RedisValue>, IComparable<RedisValue>, IComparable, IConvertible
//	{
//		private enum CompareType
//		{
//			Null,
//			Int64,
//			Double,
//			Raw
//		}

//		internal static readonly RedisValue[] EmptyArray = new RedisValue[0];

//		private static readonly byte[] EmptyByteArr = new byte[0];

//		private static readonly byte[] IntegerSentinel = new byte[0];

//		private readonly byte[] valueBlob;

//		private readonly long valueInt64;

//		/// <summary>
//		/// Represents the string <c>""</c>
//		/// </summary>
//		public static RedisValue EmptyString
//		{
//			[CompilerGenerated]
//			get
//			{
//				return RedisValue.<EmptyString>k__BackingField;
//			}
//		}

//		/// <summary>
//		/// A null value
//		/// </summary>
//		public static RedisValue Null
//		{
//			[CompilerGenerated]
//			get
//			{
//				return RedisValue.<Null>k__BackingField;
//			}
//		}

//		/// <summary>
//		/// Indicates whether the value is a primitive integer
//		/// </summary>
//		public bool IsInteger
//		{
//			get
//			{
//				return this.valueBlob == RedisValue.IntegerSentinel;
//			}
//		}

//		/// <summary>
//		/// Indicates whether the value should be considered a null value
//		/// </summary>
//		public bool IsNull
//		{
//			get
//			{
//				return this.valueBlob == null;
//			}
//		}

//		/// <summary>
//		/// Indicates whether the value is either null or a zero-length value
//		/// </summary>
//		public bool IsNullOrEmpty
//		{
//			get
//			{
//				return this.valueBlob == null || (this.valueBlob.Length == 0 && this.valueBlob != RedisValue.IntegerSentinel);
//			}
//		}

//		/// <summary>
//		/// Indicates whether the value is greater than zero-length
//		/// </summary>
//		public bool HasValue
//		{
//			get
//			{
//				return this.valueBlob != null && this.valueBlob.Length != 0;
//			}
//		}

//		private RedisValue(long valueInt64, byte[] valueBlob)
//		{
//			this.valueInt64 = valueInt64;
//			this.valueBlob = valueBlob;
//		}

//		/// <summary>
//		/// Indicates whether two RedisValue values are equivalent
//		/// </summary>
//		public static bool operator !=(RedisValue x, RedisValue y)
//		{
//			return !(x == y);
//		}

//		/// <summary>
//		/// Indicates whether two RedisValue values are equivalent
//		/// </summary>
//		public static bool operator ==(RedisValue x, RedisValue y)
//		{
//			if (x.valueBlob == null)
//			{
//				return y.valueBlob == null;
//			}
//			if (x.valueBlob == RedisValue.IntegerSentinel)
//			{
//				if (y.valueBlob == RedisValue.IntegerSentinel)
//				{
//					return x.valueInt64 == y.valueInt64;
//				}
//				return RedisValue.Equals(x, y);
//			}
//			else
//			{
//				if (y.valueBlob == RedisValue.IntegerSentinel)
//				{
//					return RedisValue.Equals(x, y);
//				}
//				return RedisValue.Equals(x.valueBlob, y.valueBlob);
//			}
//		}

//		/// <summary>
//		/// See Object.Equals()
//		/// </summary>
//		public override bool Equals(object obj)
//		{
//			if (obj == null)
//			{
//				return this.valueBlob == null;
//			}
//			if (obj is RedisValue)
//			{
//				return this.Equals((RedisValue)obj);
//			}
//			if (obj is string)
//			{
//				return (string)obj == this;
//			}
//			if (obj is byte[])
//			{
//				return RedisValue.Equals((byte[])obj, this);
//			}
//			if (obj is long)
//			{
//				return (long)obj == (long)this;
//			}
//			return obj is int && (int)obj == (int)this;
//		}

//		/// <summary>
//		/// Indicates whether two RedisValue values are equivalent
//		/// </summary>
//		public bool Equals(RedisValue other)
//		{
//			return this == other;
//		}

//		/// <summary>
//		/// See Object.GetHashCode()
//		/// </summary>
//		public override int GetHashCode()
//		{
//			if (this.valueBlob == RedisValue.IntegerSentinel)
//			{
//				return this.valueInt64.GetHashCode();
//			}
//			if (this.valueBlob == null)
//			{
//				return -1;
//			}
//			return RedisValue.GetHashCode(this.valueBlob);
//		}

//		/// <summary>
//		/// Returns a string representation of the value
//		/// </summary>
//		public override string ToString()
//		{
//			return this;
//		}

//		internal unsafe static bool Equals(byte[] x, byte[] y)
//		{
//			if (x == y)
//			{
//				return true;
//			}
//			if (x == null || y == null)
//			{
//				return false;
//			}
//			int num = x.Length;
//			if (num != y.Length)
//			{
//				return false;
//			}
//			int num2 = num / 8;
//			int num3 = num % 8;
//			fixed (byte* ptr = x, ptr2 = y)
//			{
//				long* ptr3 = (long*)ptr;
//				long* ptr4 = (long*)ptr2;
//				for (int i = 0; i < num2; i++)
//				{
//					if (ptr3[i] != ptr4[i])
//					{
//						return false;
//					}
//				}
//				int num4 = num - num3;
//				while (num3-- != 0)
//				{
//					if (ptr[num4] != ptr2[num4++])
//					{
//						return false;
//					}
//				}
//			}
//			return true;
//		}

//		internal unsafe static int GetHashCode(byte[] value)
//		{
//			if (value == null)
//			{
//				return -1;
//			}
//			int num = value.Length;
//			if (num == 0)
//			{
//				return 0;
//			}
//			int num2 = num / 8;
//			int num3 = num % 8;
//			int num4 = 728271210;
//			fixed (byte* ptr = value)
//			{
//				long* ptr2 = (long*)ptr;
//				for (int i = 0; i < num2; i++)
//				{
//					long num5 = ptr2[i];
//					int num6 = (int)num5 ^ (int)(num5 >> 32);
//					num4 = ((num4 << 5) + num4 ^ num6);
//				}
//				int num7 = num - num3;
//				while (num3-- != 0)
//				{
//					num4 = ((num4 << 5) + num4 ^ (int)ptr[num7++]);
//				}
//			}
//			return num4;
//		}

//		internal static bool TryParseInt64(byte[] value, int offset, int count, out long result)
//		{
//			result = 0L;
//			if (value == null || count <= 0)
//			{
//				return false;
//			}
//			checked
//			{
//				int num = offset + count;
//				if (value[offset] == 45)
//				{
//					for (int i = offset + 1; i < num; i++)
//					{
//						byte b = value[i];
//						if (b < 48 || b > 57)
//						{
//							return false;
//						}
//						result = result * 10L - unchecked((long)(checked(b - 48)));
//					}
//					return true;
//				}
//				for (int j = offset; j < num; j++)
//				{
//					byte b2 = value[j];
//					if (b2 < 48 || b2 > 57)
//					{
//						return false;
//					}
//					result = result * 10L + unchecked((long)(checked(b2 - 48)));
//				}
//				return true;
//			}
//		}

//		internal void AssertNotNull()
//		{
//			if (this.IsNull)
//			{
//				throw new ArgumentException("A null value is not valid in this context");
//			}
//		}

//		private RedisValue.CompareType ResolveType(out long i64, out double r8)
//		{
//			byte[] array = this.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				i64 = this.valueInt64;
//				r8 = 0.0;
//				return RedisValue.CompareType.Int64;
//			}
//			if (array == null)
//			{
//				i64 = 0L;
//				r8 = 0.0;
//				return RedisValue.CompareType.Null;
//			}
//			if (RedisValue.TryParseInt64(array, 0, array.Length, out i64))
//			{
//				r8 = 0.0;
//				return RedisValue.CompareType.Int64;
//			}
//			if (RedisValue.TryParseDouble(array, out r8))
//			{
//				i64 = 0L;
//				return RedisValue.CompareType.Double;
//			}
//			i64 = 0L;
//			r8 = 0.0;
//			return RedisValue.CompareType.Raw;
//		}

//		/// <summary>
//		/// Compare against a RedisValue for relative order
//		/// </summary>
//		public int CompareTo(RedisValue other)
//		{
//			try
//			{
//				long num;
//				double num2;
//				RedisValue.CompareType compareType = this.ResolveType(out num, out num2);
//				long num3;
//				double value;
//				RedisValue.CompareType compareType2 = other.ResolveType(out num3, out value);
//				int result;
//				if (compareType == RedisValue.CompareType.Null)
//				{
//					result = ((compareType2 == RedisValue.CompareType.Null) ? 0 : -1);
//					return result;
//				}
//				if (compareType2 == RedisValue.CompareType.Null)
//				{
//					result = 1;
//					return result;
//				}
//				if (compareType == RedisValue.CompareType.Int64)
//				{
//					if (compareType2 == RedisValue.CompareType.Int64)
//					{
//						result = num.CompareTo(num3);
//						return result;
//					}
//					if (compareType2 == RedisValue.CompareType.Double)
//					{
//						result = ((double)num).CompareTo(value);
//						return result;
//					}
//				}
//				else if (compareType == RedisValue.CompareType.Double)
//				{
//					if (compareType2 == RedisValue.CompareType.Int64)
//					{
//						result = num2.CompareTo((double)num3);
//						return result;
//					}
//					if (compareType2 == RedisValue.CompareType.Double)
//					{
//						result = num2.CompareTo(value);
//						return result;
//					}
//				}
//				result = StringComparer.InvariantCulture.Compare(this, other);
//				return result;
//			}
//			catch (Exception var_8_A7)
//			{
//			}
//			return 0;
//		}

//		int IComparable.CompareTo(object obj)
//		{
//			if (obj is RedisValue)
//			{
//				return this.CompareTo((RedisValue)obj);
//			}
//			if (obj is long)
//			{
//				return this.CompareTo((long)obj);
//			}
//			if (obj is double)
//			{
//				return this.CompareTo((double)obj);
//			}
//			if (obj is string)
//			{
//				return this.CompareTo((string)obj);
//			}
//			if (obj is byte[])
//			{
//				return this.CompareTo((byte[])obj);
//			}
//			if (obj is bool)
//			{
//				return this.CompareTo((bool)obj);
//			}
//			return -1;
//		}

//		/// <summary>
//		/// Creates a new RedisValue from an Int32
//		/// </summary>
//		public static implicit operator RedisValue(int value)
//		{
//			return new RedisValue((long)value, RedisValue.IntegerSentinel);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a nullable Int32
//		/// </summary>
//		public static implicit operator RedisValue(int? value)
//		{
//			if (value.HasValue)
//			{
//				return value.GetValueOrDefault();
//			}
//			return RedisValue.Null;
//		}

//		/// <summary>
//		/// Creates a new RedisValue from an Int64
//		/// </summary>
//		public static implicit operator RedisValue(long value)
//		{
//			return new RedisValue(value, RedisValue.IntegerSentinel);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a nullable Int64
//		/// </summary>
//		public static implicit operator RedisValue(long? value)
//		{
//			if (value.HasValue)
//			{
//				return value.GetValueOrDefault();
//			}
//			return RedisValue.Null;
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a Double
//		/// </summary>
//		public static implicit operator RedisValue(double value)
//		{
//			return Format.ToString(value);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a nullable Double
//		/// </summary>
//		public static implicit operator RedisValue(double? value)
//		{
//			if (value.HasValue)
//			{
//				return value.GetValueOrDefault();
//			}
//			return RedisValue.Null;
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a String
//		/// </summary>
//		public static implicit operator RedisValue(string value)
//		{
//			byte[] array;
//			if (value == null)
//			{
//				array = null;
//			}
//			else if (value.Length == 0)
//			{
//				array = RedisValue.EmptyByteArr;
//			}
//			else
//			{
//				array = Encoding.UTF8.GetBytes(value);
//			}
//			return new RedisValue(0L, array);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a Byte[]
//		/// </summary>
//		public static implicit operator RedisValue(byte[] value)
//		{
//			byte[] array;
//			if (value == null)
//			{
//				array = null;
//			}
//			else if (value.Length == 0)
//			{
//				array = RedisValue.EmptyByteArr;
//			}
//			else
//			{
//				array = value;
//			}
//			return new RedisValue(0L, array);
//		}

//		internal static RedisValue Parse(object obj)
//		{
//			if (obj == null)
//			{
//				return RedisValue.Null;
//			}
//			if (obj is RedisValue)
//			{
//				return (RedisValue)obj;
//			}
//			if (obj is string)
//			{
//				return (string)obj;
//			}
//			if (obj is int)
//			{
//				return (int)obj;
//			}
//			if (obj is double)
//			{
//				return (double)obj;
//			}
//			if (obj is byte[])
//			{
//				return (byte[])obj;
//			}
//			if (obj is bool)
//			{
//				return (bool)obj;
//			}
//			if (obj is long)
//			{
//				return (long)obj;
//			}
//			if (obj is float)
//			{
//				return (double)((float)obj);
//			}
//			throw new InvalidOperationException("Unable to format type for redis: " + obj.GetType().FullName);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a Boolean
//		/// </summary>
//		public static implicit operator RedisValue(bool value)
//		{
//			return new RedisValue(value ? 1L : 0L, RedisValue.IntegerSentinel);
//		}

//		/// <summary>
//		/// Creates a new RedisValue from a nullable Boolean
//		/// </summary>
//		public static implicit operator RedisValue(bool? value)
//		{
//			if (value.HasValue)
//			{
//				return value.GetValueOrDefault();
//			}
//			return RedisValue.Null;
//		}

//		/// <summary>
//		/// Converts the value to a Boolean
//		/// </summary>
//		public static explicit operator bool(RedisValue value)
//		{
//			long num = (long)value;
//			if (num == 0L)
//			{
//				return false;
//			}
//			if (num != 1L)
//			{
//				throw new InvalidCastException();
//			}
//			return true;
//		}

//		/// <summary>
//		/// Converts the value to an Int32
//		/// </summary>
//		public static explicit operator int(RedisValue value)
//		{
//			return checked((int)((long)value));
//		}

//		/// <summary>
//		/// Converts the value to an Int64
//		/// </summary>
//		public static explicit operator long(RedisValue value)
//		{
//			byte[] array = value.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				return value.valueInt64;
//			}
//			if (array == null)
//			{
//				return 0L;
//			}
//			long result;
//			if (RedisValue.TryParseInt64(array, 0, array.Length, out result))
//			{
//				return result;
//			}
//			throw new InvalidCastException();
//		}

//		/// <summary>
//		/// Converts the value to a Double
//		/// </summary>
//		public static explicit operator double(RedisValue value)
//		{
//			byte[] array = value.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				return (double)value.valueInt64;
//			}
//			if (array == null)
//			{
//				return 0.0;
//			}
//			double result;
//			if (RedisValue.TryParseDouble(array, out result))
//			{
//				return result;
//			}
//			throw new InvalidCastException();
//		}

//		private static bool TryParseDouble(byte[] blob, out double value)
//		{
//			if (blob.Length == 1 && blob[0] >= 48 && blob[0] <= 57)
//			{
//				value = (double)(blob[0] - 48);
//				return true;
//			}
//			return Format.TryParseDouble(Encoding.UTF8.GetString(blob), out value);
//		}

//		/// <summary>
//		/// Converts the value to a nullable Double
//		/// </summary>
//		public static explicit operator double?(RedisValue value)
//		{
//			if (value.valueBlob == null)
//			{
//				return null;
//			}
//			return new double?((double)value);
//		}

//		/// <summary>
//		/// Converts the value to a nullable Int64
//		/// </summary>
//		public static explicit operator long?(RedisValue value)
//		{
//			if (value.valueBlob == null)
//			{
//				return null;
//			}
//			return new long?((long)value);
//		}

//		/// <summary>
//		/// Converts the value to a nullable Int32
//		/// </summary>
//		public static explicit operator int?(RedisValue value)
//		{
//			if (value.valueBlob == null)
//			{
//				return null;
//			}
//			return new int?((int)value);
//		}

//		/// <summary>
//		/// Converts the value to a nullable Boolean
//		/// </summary>
//		public static explicit operator bool?(RedisValue value)
//		{
//			if (value.valueBlob == null)
//			{
//				return null;
//			}
//			return new bool?((bool)value);
//		}

//		/// <summary>
//		/// Converts the value to a String
//		/// </summary>
//		public static implicit operator string(RedisValue value)
//		{
//			byte[] array = value.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				return Format.ToString(value.valueInt64);
//			}
//			if (array == null)
//			{
//				return null;
//			}
//			if (array.Length == 0)
//			{
//				return "";
//			}
//			if (array.Length == 2 && array[0] == 79 && array[1] == 75)
//			{
//				return "OK";
//			}
//			string result;
//			try
//			{
//				result = Encoding.UTF8.GetString(array);
//			}
//			catch
//			{
//				result = BitConverter.ToString(array);
//			}
//			return result;
//		}

//		/// <summary>
//		/// Converts the value to a byte[]
//		/// </summary>
//		public static implicit operator byte[](RedisValue value)
//		{
//			byte[] array = value.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				return Encoding.UTF8.GetBytes(Format.ToString(value.valueInt64));
//			}
//			return array;
//		}

//		TypeCode IConvertible.GetTypeCode()
//		{
//			return TypeCode.Object;
//		}

//		bool IConvertible.ToBoolean(IFormatProvider provider)
//		{
//			return (bool)this;
//		}

//		byte IConvertible.ToByte(IFormatProvider provider)
//		{
//			return (byte)((int)this);
//		}

//		char IConvertible.ToChar(IFormatProvider provider)
//		{
//			return (char)((int)this);
//		}

//		DateTime IConvertible.ToDateTime(IFormatProvider provider)
//		{
//			return DateTime.Parse(this, provider);
//		}

//		decimal IConvertible.ToDecimal(IFormatProvider provider)
//		{
//			return (long)this;
//		}

//		double IConvertible.ToDouble(IFormatProvider provider)
//		{
//			return (double)this;
//		}

//		short IConvertible.ToInt16(IFormatProvider provider)
//		{
//			return (short)((int)this);
//		}

//		int IConvertible.ToInt32(IFormatProvider provider)
//		{
//			return (int)this;
//		}

//		long IConvertible.ToInt64(IFormatProvider provider)
//		{
//			return (long)this;
//		}

//		sbyte IConvertible.ToSByte(IFormatProvider provider)
//		{
//			return (sbyte)((int)this);
//		}

//		float IConvertible.ToSingle(IFormatProvider provider)
//		{
//			return (float)((long)this);
//		}

//		string IConvertible.ToString(IFormatProvider provider)
//		{
//			return this;
//		}

//		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
//		{
//			if (conversionType == null)
//			{
//				throw new ArgumentNullException("conversionType");
//			}
//			if (conversionType == typeof(byte[]))
//			{
//				return this;
//			}
//			if (conversionType == typeof(RedisValue))
//			{
//				return this;
//			}
//			switch (conversionType.GetTypeCode())
//			{
//			case TypeCode.Object:
//				return this;
//			case TypeCode.Boolean:
//				return (bool)this;
//			case TypeCode.Char:
//				return (char)((int)this);
//			case TypeCode.SByte:
//				return (sbyte)((int)this);
//			case TypeCode.Byte:
//				return (byte)((int)this);
//			case TypeCode.Int16:
//				return (short)((int)this);
//			case TypeCode.UInt16:
//				return (ushort)((int)this);
//			case TypeCode.Int32:
//				return (int)this;
//			case TypeCode.UInt32:
//				return (uint)((long)this);
//			case TypeCode.Int64:
//				return (long)this;
//			case TypeCode.UInt64:
//				return (long)this;
//			case TypeCode.Single:
//				return (float)((long)this);
//			case TypeCode.Double:
//				return (double)this;
//			case TypeCode.Decimal:
//				return (long)this;
//			case TypeCode.DateTime:
//				return DateTime.Parse(this, provider);
//			case TypeCode.String:
//				return this;
//			}
//			throw new NotSupportedException();
//		}

//		ushort IConvertible.ToUInt16(IFormatProvider provider)
//		{
//			return (ushort)((int)this);
//		}

//		uint IConvertible.ToUInt32(IFormatProvider provider)
//		{
//			return (uint)((long)this);
//		}

//		ulong IConvertible.ToUInt64(IFormatProvider provider)
//		{
//			return (ulong)((double)this);
//		}

//		/// <summary>
//		/// Convert to a long if possible, returning true.
//		///
//		/// Returns false otherwise.
//		/// </summary>
//		public bool TryParse(out long val)
//		{
//			byte[] array = this.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				val = this.valueInt64;
//				return true;
//			}
//			if (array == null)
//			{
//				val = 0L;
//				return true;
//			}
//			return RedisValue.TryParseInt64(array, 0, array.Length, out val);
//		}

//		/// <summary>
//		/// Convert to a int if possible, returning true.
//		///
//		/// Returns false otherwise.
//		/// </summary>
//		public bool TryParse(out int val)
//		{
//			long num;
//			if (!this.TryParse(out num) || num > 2147483647L || num < -2147483648L)
//			{
//				val = 0;
//				return false;
//			}
//			val = (int)num;
//			return true;
//		}

//		/// <summary>
//		/// Convert to a double if possible, returning true.
//		///
//		/// Returns false otherwise.
//		/// </summary>
//		public bool TryParse(out double val)
//		{
//			byte[] array = this.valueBlob;
//			if (array == RedisValue.IntegerSentinel)
//			{
//				val = (double)this.valueInt64;
//				return true;
//			}
//			if (array == null)
//			{
//				val = 0.0;
//				return true;
//			}
//			return RedisValue.TryParseDouble(array, out val);
//		}

//		static RedisValue()
//		{
//			// Note: this type is marked as 'beforefieldinit'.
//			RedisValue.<EmptyString>k__BackingField = new RedisValue(0L, RedisValue.EmptyByteArr);
//			RedisValue.<Null>k__BackingField = new RedisValue(0L, null);
//		}
//	}
//}


