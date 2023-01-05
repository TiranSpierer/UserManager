// UserManager/DAL/RegistrationService.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Automation;
using DAL.Services.Interfaces;
using DAL.Setup;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services.ConcreteServices;

public class RegistrationService : DataServiceBase<Registration>
{
    #region Constructors

    public RegistrationService(DataBaseContext context) : base(context) { }

    #endregion

}