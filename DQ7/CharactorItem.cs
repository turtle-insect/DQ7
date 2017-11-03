using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DQ7
{
	class CharactorItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private readonly uint mAddress;

		public CharactorItem(uint address)
		{
			mAddress = address;
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress, 2);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
			}
		}

		public uint Count
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 2, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress + 2, 1, value);
			}
		}

		public bool Equipment
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 3, 1) == 1;
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress, 2, value == true ? 1U : 0);
			}
		}
	}
}
