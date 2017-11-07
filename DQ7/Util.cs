using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class Util
	{
		public const uint BaseAddress = 0;
		public const uint BlockSize = 0x4FD0;

		public const uint BagItemCount = 536;

		public const uint PartyMemberCount = 6;

		public static void WriteNumber(uint address, uint size, uint value, uint min, uint max)
		{
			if (value < min) value = min;
			if (value > max) value = max;
			SaveData.Instance().WriteNumber(address, size, value);
		}
	}
}
