using System.Collections.Generic;
using Gaming1.Domain.Models;

namespace Gaming1.Application.Service.Services
{
    public interface IPlayerGenerator
    {
        List<Player> Create(int playersNumbers = 2);
    }
}