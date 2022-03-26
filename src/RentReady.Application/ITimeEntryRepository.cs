using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Application
{
	public interface ITimeEntryRepository
	{
		Task CreateTimeEntryRecordAsync(RentReady.Domain.TimeEntry timeEntry);
	}
}
