// UserManager/Domain/Patient.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;

namespace Domain.Models;

public class Patient
{

    #region Public Properties

    public int      Id          { get; set; }
    public string   Name        { get; set; }
    public DateTime DateOfBirth { get; set; }

    #endregion

}