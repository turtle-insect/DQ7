﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace DQ7
{
	class PartyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint id = (uint)value;
			Info info = Info.Instance();
			return info.Search(info.Party, id)?.Name;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return "";
		}
	}
}
