using DemoProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Interface
{
	public interface IDeleteable
	{
		void Delete(DataController con);
		bool Deleted
		{
			get;
		}
	}
}
