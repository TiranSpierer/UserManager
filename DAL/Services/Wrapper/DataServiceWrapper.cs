// UserManager/DAL/DataServiceWrapper.cs
// Created by Tiran Spierer
// Created at 03/01/2023
// Class propose:

using DAL.Services.ConcreteServices;
using DAL.Services.Interfaces;
using Domain.Models;
using Prism.Events;

namespace DAL.Services.Wrapper;

public sealed class DataServiceWrapper
{
    #region Privates

    private static          DataServiceWrapper? _instance = null;
    private static readonly object              _mutex    = new();



    #endregion

    #region Constructors

    private DataServiceWrapper(IDataService<User> userService, IDataService<Patient> patientService, IDataService<UserPrivilege> userPrivilegeService)
    {
        UserService          = userService;
        PatientService       = patientService;
        UserPrivilegeService = userPrivilegeService;
    }

    #endregion

    #region Public Properties

    public IDataService<User>          UserService          { get; set; }
    public IDataService<Patient>       PatientService       { get; set; }
    public IDataService<UserPrivilege> UserPrivilegeService { get; set; }

#endregion

    #region Public Methods

    public static DataServiceWrapper Instance(IDataService<User> userService, IDataService<Patient> patientService, IDataService<UserPrivilege> userPrivilegeService)
    {
        if (_instance == null)
        {
            lock (_mutex)
            {
                _instance ??= new DataServiceWrapper(userService, patientService, userPrivilegeService);
            }
        }

        return _instance;
    }

    #endregion

    #region Private Methods



    #endregion
}