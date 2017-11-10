using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class Place
	{
		private readonly uint mID;
		public String Name { get; set; }
		public Place(uint id)
		{
			mID = id;
		}

		public bool Visit
		{
			get
			{
				return SaveData.Instance().ReadBit(0x0020 + mID / 8, mID % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(0x0020 + mID / 8, mID % 8, value);
			}
		}
	}
}
