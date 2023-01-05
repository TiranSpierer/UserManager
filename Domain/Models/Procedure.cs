// UserManager/Domain/Procedure.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using Domain.Interfaces;

namespace Domain.Models;

public class Procedure : IEntity<Procedure>
{
    public int Id        { get; set; }
    public int PatientId { get; set; }
    public int UserId    { get; set; }

    public virtual Patient Patient { get; set; }
    public virtual User    User    { get; set; }

#region Implementation of IEntity<in Procedure>

    public void CopyValuesTo(Procedure entity)
    {
        entity.Id        = Id;
        entity.PatientId = PatientId;
        entity.UserId    = UserId;
        entity.Patient   = Patient;
        entity.User      = User;
    }

#endregion
}