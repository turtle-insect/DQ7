using System.ComponentModel;

namespace DQ7
{
	class MonsterStamp : INotifyPropertyChanged
	{
		private readonly uint mIDAddress;

		public event PropertyChangedEventHandler PropertyChanged;

		public MonsterStamp(uint id)
		{
			mIDAddress = id;
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mIDAddress, 2);
			}

			set
			{
				SaveData.Instance().WriteNumber(mIDAddress, 2, value);
				// Not Typo.
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
			}
		}

		public uint Count
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x2BAC + ID, 1);
			}

			set
			{
				Util.WriteNumber(0x2BAC + ID, 1, value, 0, 99);
			}
		}
	}
}
