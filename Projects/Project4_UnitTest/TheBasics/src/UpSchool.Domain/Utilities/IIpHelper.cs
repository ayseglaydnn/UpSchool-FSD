using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.Domain.Utilities
{
	public interface IIpHelper
	{
		string GetCurrentIPAddress();

		List<string> GetAllIpAddresses();
	}
}
