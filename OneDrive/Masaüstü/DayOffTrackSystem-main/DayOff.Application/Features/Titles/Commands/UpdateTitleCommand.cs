using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Titles.Commands
{
    public class UpdateTitleCommand : IRequest<bool>
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }

        public UpdateTitleCommand(int titleId, string titleName)
        {
            TitleId = titleId;
            TitleName = titleName;
        }
    }
}
