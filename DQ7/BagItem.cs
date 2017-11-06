using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DQ7
{
	class BagItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public BagItem Next { get; private set; }
		
		private readonly uint mIDAddress;
		private readonly uint mCountAddress;

		public BagItem(uint id, uint count, BagItem next)
		{
			mIDAddress = id;
			mCountAddress = count;
			Next = next;
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
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
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
				Util.WriteNumber(mCountAddress, 1, value, 0, 99);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
			}
		}
	}
}
