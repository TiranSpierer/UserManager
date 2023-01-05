// UserManager/DAL/FrameService.cs
// Created by Tiran Spierer
// Created at 05/01/2023
// Class propose:

using DAL.Services.Interfaces;
using DAL.Setup;
using Domain.Models;

namespace DAL.Services.ConcreteServices;

public class FrameService : DataServiceBase<Frame>
{
#region Constructors

    public FrameService(DataBaseContext context) : base(context) { }

#endregion
}