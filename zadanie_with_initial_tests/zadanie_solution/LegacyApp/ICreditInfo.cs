using System;

namespace LegacyApp;

public interface ICreditInfo
{
    int GetCreditLimit(string lastName, DateTime dateOfBirth);
}