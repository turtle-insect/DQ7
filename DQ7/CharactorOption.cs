using System;
using System.ComponentModel;

namespace DQ7
{
	class CharactorOption : INotifyPropertyChanged
	{
		private readonly uint mAddress;
		private readonly uint mID;

		public event PropertyChangedEventHandler PropertyChanged;

		public String Name { get; set; }

		public CharactorOption(uint address, uint id)
		{
			mAddress = address;
			mID = id;
		}

		public bool Leam
		{
			get
			{
				return SaveData.Instance().ReadBit(mAddress + mID / 8, mID % 8);
			}

			set
			{
				SaveData.Instance().WriteBit(mAddress + mID / 8, mID % 8, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Leam)));
			}
		}
	}
}
