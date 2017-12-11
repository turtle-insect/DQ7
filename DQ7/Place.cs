using System;
using System.ComponentModel;

namespace DQ7
{
	class Place : INotifyPropertyChanged
	{
		private readonly uint mID;

		public event PropertyChangedEventHandler PropertyChanged;

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
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visit)));
			}
		}
	}
}
