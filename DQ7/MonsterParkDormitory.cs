using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class MonsterParkDormitory
	{
		private readonly uint mIDAddress;
		private readonly uint mCountAddress;

		public MonsterParkDormitory(uint id, uint count)
		{
			mIDAddress = id;
			mCountAddress = count;
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mIDAddress, 4);
			}

			set
			{
				SaveData.Instance().WriteNumber(mIDAddress, 4, value);
			}
		}

		public uint Count
		{
			get
			{
				return SaveData.Instance().ReadNumber(mCountAddress, 1);
			}

			set
			{
				Util.WriteNumber(mCountAddress, 1, value, 0, 24);
			}
		}
	}
}
