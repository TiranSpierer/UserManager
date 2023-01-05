﻿// UserManager/Domain/Patient.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models;

public class Patient : IEntity<Patient>
{

    #region Public Properties

    public int       Id          { get; set; }
    public string?   Name        { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Procedure> Procedures { get; set; }

    #endregion

    #region Implementation of IEntity<Patient>

    public void CopyValuesTo(Patient entity)
    {
        entity.Id = Id;
        entity.Name = Name;
        entity.DateOfBirth = DateOfBirth;
    }

#endregion
}