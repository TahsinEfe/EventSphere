using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Commands
{
    public class CreateTitleCommand : IRequest<int>
    {
        public string TitleName { get; set; }

        public CreateTitleCommand(string titleName)
        {
            TitleName = titleName;
        }
    }
}
