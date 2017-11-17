using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class TownMonster
	{
		private readonly uint mAddress;
		private readonly uint mID;

		public String Name { get; set; }
		public TownMonster(uint address, uint id)
		{
			mAddress = address;
			mID = id;
		}

		public bool Exist
		{
			get
			{
				return SaveData.Instance().ReadBit(mAddress + mID / 8, mID % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(mAddress + mID / 8, mID % 8, value);
			}
		}
	}
}
