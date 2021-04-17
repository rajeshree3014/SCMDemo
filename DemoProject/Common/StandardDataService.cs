using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Common
{
	[Serializable()]
	public class StandardDataService
	{
		private DataController _controller = null;

		/// <summary>
		/// Initialize Datacontroller for this instance
		/// </summary>
		/// <param name="controller"></param>
		public StandardDataService(DataController controller)
		{
			_controller = controller;
		}

		/// <summary>
		/// Gets the datacontroller
		/// </summary>
		///<value>DataController</value>
		public DataController Controller
		{
			get
			{
				return _controller;
			}
		}
	}
}
