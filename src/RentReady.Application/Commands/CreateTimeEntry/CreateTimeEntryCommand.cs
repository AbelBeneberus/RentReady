using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommand: IRequest<Unit>
	{
		public string StartOn { get; set; } = string.Empty;
		public string EndOn { get; set; } = string.Empty;

	}
}
