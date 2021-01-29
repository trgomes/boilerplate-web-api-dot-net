using Solution.Domain.Core.Commands;
using Solution.Domain.Core.Events;
using System.Threading.Tasks;

namespace Solution.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
