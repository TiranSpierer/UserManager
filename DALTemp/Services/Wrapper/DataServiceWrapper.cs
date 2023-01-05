/*
    UserManager/DAL/DataServiceWrapper.cs
    Created by Tiran Spierer
    Created at 03/01/2023
    Class propose:  
*/
using DAL.Services.Interfaces;
using Domain.Models;

namespace DAL.Services.Wrapper;

public sealed class DataServiceWrapper
{
    #region Privates

    private static          DataServiceWrapper? _instance = null;
    private static readonly object              _mutex    = new();



    #endregion

    #region Constructors

    private DataServiceWrapper(IDataService<User> userService, 
                               IDataService<Patient> patientService, 
                               IDataService<UserPrivilege> userPrivilegeService, 
                               IDataService<Registration> registrationService, 
                               IDataService<Procedure> procedureService,
                               IDataService<Frame> frameService)
    
    {
        UserService          = userService;
        PatientService       = patientService;
        UserPrivilegeService = userPrivilegeService;
        RegistrationService  = registrationService;
        ProcedureService     = procedureService;
        FrameService         = frameService;
    }

    #endregion

    #region Public Properties

    public IDataService<User>          UserService          { get; set; }
    public IDataService<Patient>       PatientService       { get; set; }
    public IDataService<UserPrivilege> UserPrivilegeService { get; set; }
    public IDataService<Registration>  RegistrationService  { get; set; }
    public IDataService<Procedure>     ProcedureService     { get; set; }
    public IDataService<Frame>         FrameService         { get; set; }

#endregion

    #region Public Methods

    public static DataServiceWrapper Instance(IDataService<User>          userService, 
                                              IDataService<Patient>       patientService, 
                                              IDataService<UserPrivilege> userPrivilegeService, 
                                              IDataService<Registration>  registrationService, 
                                              IDataService<Procedure>     procedureService,
                                              IDataService<Frame>         frameService)
    {
        if (_instance == null)
        {
            lock (_mutex)
            {
                _instance ??= new DataServiceWrapper(userService, patientService, userPrivilegeService, registrationService, procedureService, frameService);
            }
        }

        return _instance;
    }

    #endregion

    #region Private Methods



    #endregion
}