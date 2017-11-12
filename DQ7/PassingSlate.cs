using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class PassingSlate
	{
		private readonly uint mAddress;

		public PassingSlate(uint address)
		{
			mAddress = address;
			Leader = new PassingSlateMonster(address, 0);
			Companion1 = new PassingSlateMonster(address, 1);
			Companion2 = new PassingSlateMonster(address, 2);
		}

		public PassingSlateMonster Leader { get; set; }
		public PassingSlateMonster Companion1 { get; set; }
		public PassingSlateMonster Companion2 { get; set; }
		public uint BossLv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 35, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 35, 1, value, 1, 99);
			}
		}

		public uint SlateHeader
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 8, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 8, 2, value, 1, 99);
			}
		}

		public uint SlateFooter
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 10, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 10, 2, value, 1, 99);
			}
		}

		public uint Capture
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 14, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 14, 2, value, 1, 0xFFFF);
			}
		}
	}

	class PassingSlateMonster
	{
		private readonly uint mAddress;
		private readonly uint mID;

		public PassingSlateMonster(uint address, uint id)
		{
			mAddress = address;
			mID = id;
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 32 + mID, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 32 + mID, 1, value, 1, 99);
			}
		}

		public uint Kind
		{
			get
			{
				return (SaveData.Instance().ReadNumber(mAddress + mID * 2, 2) - 1) / 3;
			}

			set
			{
				uint option = (SaveData.Instance().ReadNumber(mAddress + mID * 2, 2) - 1) % 3;
				SaveData.Instance().WriteNumber(mAddress + mID * 2, 2, value * 3 + option + 1);
				if(mID==0)
				{
					SaveData.Instance().WriteNumber(mAddress + 6, 2, value + 1);
				}
			}
		}

		public uint Option
		{
			get
			{
				return (SaveData.Instance().ReadNumber(mAddress + mID * 2, 2) - 1) % 3;
			}

			set
			{
				uint kind = (SaveData.Instance().ReadNumber(mAddress + mID * 2, 2) - 1) / 3;
				SaveData.Instance().WriteNumber(mAddress + mID * 2, 2, kind * 3 + value + 1);
			}
		}
	}
}
