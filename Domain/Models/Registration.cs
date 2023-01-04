// UserManager/Domain/Registration.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.Media3D;
using Domain.Interfaces;

namespace Domain.Models;

public class Registration : IEntity<Registration>
{
    public int    ProcedureId              { get; set; }
    public double XiphoidProcessPositionX  { get; set; }
    public double XiphoidProcessPositionY  { get; set; }
    public double XiphoidProcessPositionZ  { get; set; }
    public double ReferenceSensorPositionX { get; set; }
    public double ReferenceSensorPositionY { get; set; }
    public double ReferenceSensorPositionZ { get; set; }


    #region Implementation of IEntity<in Registration>
    public void     CopyValuesTo(Registration entity)
    {
        entity.ProcedureId              = ProcedureId;
        entity.XiphoidProcessPositionX  = XiphoidProcessPositionX;
        entity.XiphoidProcessPositionY  = XiphoidProcessPositionY;
        entity.XiphoidProcessPositionZ  = XiphoidProcessPositionZ;
        entity.ReferenceSensorPositionX = ReferenceSensorPositionX;
        entity.ReferenceSensorPositionY = ReferenceSensorPositionY;
        entity.ReferenceSensorPositionZ = ReferenceSensorPositionZ;
    }

#endregion
}