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
		public ObservableCollection<BagItem> Bag { get; set; } = new ObservableCollection<BagItem>();

		public DataContext()
		{
			for (uint i = 0; i < 6; i++)
			{
				Charactors.Add(new Charactor(0x0C80 + i * 0x01EC));
			}

			BagItem item = null;
			for (uint i = Util.BagItemCount - 1; (int)i >= 0; i--)
			{
				item = new BagItem(0x0544 + i * 2, 0x09D4 + i, item);
				item.PropertyChanged += Item_PropertyChanged;
				Bag.Insert(0, item);
			}
		}

		private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "ID") return;
			BagItem item = sender as BagItem;
			if (item == null) return;
			if (item.ID != 0) return;
			BagItem next = item.Next;
			if (next == null || next.ID == 0) return;
			item.Count = next.Count;
			item.ID = next.ID;
			next.Count = 0;
			next.ID = 0;
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
