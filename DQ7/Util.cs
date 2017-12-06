namespace DQ7
{
	class Util
	{
		public const uint BaseAddress = 0;
		public const uint BlockSize = 0x4FD0;

		public const uint BagItemCount = 536;

		public const uint PartyMemberCount = 6;

		public const uint PassingSlateCount = 16;

		public const uint MonsterStampCount = 417;

		public const uint MonsterParkDormitoryCount = 10;

		public static void WriteNumber(uint address, uint size, uint value, uint min, uint max)
		{
			if (value < min) value = min;
			if (value > max) value = max;
			SaveData.Instance().WriteNumber(address, size, value);
		}
	}
}
