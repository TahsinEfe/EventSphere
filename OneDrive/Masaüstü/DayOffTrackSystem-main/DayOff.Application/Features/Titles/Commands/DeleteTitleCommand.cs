using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DayOff.Application.Features.Titles.Commands
{
    public class DeleteTitleCommand : IRequest<bool>
    {
        public int TitleId { get; set; }
        
        public DeleteTitleCommand(int titleId)
        {
            TitleId = titleId;
        }
    }
}
