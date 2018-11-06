using System;
using System.Text;

/// <summary>
/// This class handles byte buffer arrays.
/// </summary>
public class MessageBuffer
{
	/// <summary>
	/// The byte buffer.
	/// </summary>
	public byte[] Buffer { get; private set; }
	/// <summary>
	/// Returns the buffer size in bytes.
	/// </summary>
	public int Size { get { return Buffer.Length; } }
	/// <summary>
	/// The reading and writing position.
	/// </summary>
	public int Position { get; set; } = 0;

	/// <summary>
	/// Create a MessageBuffer with an existing byte buffer.
	/// </summary>
	/// <param name="buffer">The byte buffer to use.</param>
	public MessageBuffer(byte[] buffer)
	{
		Buffer = buffer;
	}

	/// <summary>
	/// Reads a byte from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public byte ReadByte()
	{
		var value = Buffer[Position];
		Position++;

		return value;
	}

	/// <summary>
	/// Reads a signed byte from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public sbyte ReadSByte()
	{
		var value = (sbyte)Buffer[Position];
		Position++;

		return value;
	}

	/// <summary>
	/// Reads an unsigned short from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public ushort ReadUInt16()
	{
		var value = BitConverter.ToUInt16(Buffer, Position);
		Position += 2;

		return value;
	}

	/// <summary>
	/// Reads a signed short from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public short ReadInt16()
	{
		var value = BitConverter.ToInt16(Buffer, Position);
		Position += 2;

		return value;
	}

	/// <summary>
	/// Reads an unsigned int from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public uint ReadUInt32()
	{
		var value = BitConverter.ToUInt32(Buffer, Position);
		Position += 4;

		return value;
	}

	/// <summary>
	/// Reads a signed int from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public int ReadInt32()
	{
		var value = BitConverter.ToInt32(Buffer, Position);
		Position += 4;

		return value;
	}

	/// <summary>
	/// Reads a float from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public float ReadFloat()
	{
		var value = BitConverter.ToSingle(Buffer, Position);
		Position += 4;

		return value;
	}

	/// <summary>
	/// Reads a double from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public double ReadDouble()
	{
		var value = BitConverter.ToDouble(Buffer, Position);
		Position += 8;

		return value;
	}

	/// <summary>
	/// Reads a bool from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public bool ReadBoolean()
	{
		var value = BitConverter.ToBoolean(Buffer, Position);
		Position++;

		return value;
	}

	/// <summary>
	/// Reads a string from the current buffer position.
	/// </summary>
	/// <returns>Returns the value.</returns>
	public string ReadString()
	{
		int stringEnd = Array.IndexOf(Buffer, (byte)'\0', Position) + 1;
		var value = Encoding.UTF8.GetString(Buffer, Position, stringEnd - Position);
		Position += value.Length;

		return value;
	}

	/// <summary>
	/// Writes a byte to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteByte(byte value)
	{
		Buffer[Position] = value;
		Position++;
	}

	/// <summary>
	/// Writes a signed byte to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteSByte(sbyte value)
	{
		Buffer[Position] = (byte)value;
		Position++;
	}

	/// <summary>
	/// Writes an unsigned short to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteUInt16(ushort value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 2);
		Position += 2;
	}

	/// <summary>
	/// Writes a signed short to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteInt16(short value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 2);
		Position += 2;
	}

	/// <summary>
	/// Writes an unsigned int to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteUInt32(uint value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 4);
		Position += 4;
	}
	
	/// <summary>
	/// Writes a signed int to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteInt32(int value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 4);
		Position += 4;
	}

	/// <summary>
	/// Writes a float to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteFloat(float value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 4);
		Position += 4;
	}

	/// <summary>
	/// Writes a double to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteDouble(double value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 8);
		Position += 8;
	}

	/// <summary>
	/// Writes a boolean to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteBoolean(bool value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(bytes, 0, Buffer, Position, 1);
		Position += 1;
	}

	/// <summary>
	/// Writes a null terminated string to the current buffer position.
	/// </summary>
	/// <param name="value">The value to write.</param>
	public void WriteString(string value)
	{
		value = value.Replace("\0", "\\0");

		byte[] bytes = Encoding.UTF8.GetBytes(value + '\0');
		Array.Copy(bytes, 0, Buffer, Position, bytes.Length);
		Position += bytes.Length;
	}
}