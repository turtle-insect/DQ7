using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class JobInfo : ILineAnalysis
	{
		public uint ID { get; private set; }
		public String Name { get; private set; }
		public uint Star { get; private set; }
		public uint Exp { get; private set; }

		public bool Line(string[] oneLine)
		{
			ID = Convert.ToUInt32(oneLine[0]);
			Name = oneLine[1];
			if (oneLine.Length > 2) Star = Convert.ToUInt32(oneLine[2]);
			if (oneLine.Length > 3) Exp = Convert.ToUInt32(oneLine[3]);
			return true;
		}
	}
}
