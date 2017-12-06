using System;

namespace DQ7
{
	class MonsterParkPlace
    {
		private readonly uint mAddress;
		private readonly uint mID;

		public String Name { get; set; }

		public MonsterParkPlace(uint address, uint id)
		{
			mAddress = address;
			mID = id;
		}

		public bool Exist
		{
			get
			{
				return SaveData.Instance().ReadBit(mAddress + (mID + 4) / 8, (mID + 4) % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(mAddress + (mID + 4) / 8, (mID + 4) % 8, value);
			}
		}
	}
}
