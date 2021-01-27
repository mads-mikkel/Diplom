using System;
using System.Threading.Tasks;

namespace Corp.Services.SignalRHub
{
    public interface IClock
    {
        Task ShowTime(DateTime dateTime);
    }
}