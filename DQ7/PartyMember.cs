namespace DQ7
{
	class PartyMember
	{
		private readonly uint mAddress;

		public PartyMember(uint address)
		{
			mAddress = address;
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress, 4);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress, 4, value);
			}
		}
	}
}
