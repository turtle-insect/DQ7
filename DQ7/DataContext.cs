using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ7
{
	class DataContext
	{
		public ObservableCollection<Charactor> Charactors { get; set; } = new ObservableCollection<Charactor>();

		public DataContext()
		{
			for (uint i = 0; i < 6; i++)
			{
				Charactors.Add(new Charactor(0x0C80 + i * 0x01EC));
			}
		}

		public uint Gold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0528, 4);
			}

			set
			{
				Util.WriteNumber(0x0528, 4, value, 0, 9999999);
			}
		}
	}
}
