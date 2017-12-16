using System;
using System.ComponentModel;

namespace DQ7
{
	class CharactorJob : INotifyPropertyChanged
    {
		private readonly uint mLvAddress;
		private readonly uint mExpAddress;

		public event PropertyChangedEventHandler PropertyChanged;

		public String Name { get; set; }

		public CharactorJob(uint lv, uint exp)
		{
			mLvAddress = lv;
			mExpAddress = exp;
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mLvAddress, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mLvAddress, 1, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lv)));
			}
		}

		public uint Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mExpAddress, 2);
			}

			set
			{
				Util.WriteNumber(mExpAddress, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exp)));
			}
		}
	}
}
