// UserManager/Domain/Registration.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Windows.Media.Media3D;

namespace Domain.Models;

public class Registration
{
    public Registration(string procedureId, Vector3D xiphoidProcessPosition, Vector3D referenceSensorPosition)
    {
        ProcedureId             = procedureId;
        XiphoidProcessPosition  = xiphoidProcessPosition;
        ReferenceSensorPosition = referenceSensorPosition;
    }

    public string   ProcedureId             { get; set; }
    public Vector3D XiphoidProcessPosition  { get; set; }
    public Vector3D ReferenceSensorPosition { get; set; }
}