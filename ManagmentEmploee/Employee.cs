// <copyright file="Employee.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ManagmentEmploee;

internal sealed class Employee
{
    private readonly int emploeeId;
    private readonly decimal salary = 0;
    private readonly Dictionary<string, string> additionalInfo;
    private readonly string? lastName;
    private string? fullName;
    private DateTime date;

    public Employee(int employeeId)
    {
        ValidateEmployeeId(employeeId);
        this.emploeeId = employeeId;
        this.additionalInfo = new Dictionary<string, string>(StringComparer.Ordinal);
    }

    public int EmploeeId => emploeeId;

    public string? FirstName { get; set; }

    public string? LastName
    {
        get => this.lastName;
        init
        {
            if (value != null)
            {
                ValidateName(value, nameof(this.LastName));
                this.lastName = value;
            }
        }
    }

    public DateTime BirthDate
    {
        get => this.date;
        set => this.date = ValidateBirthDate(value);
    }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - this.date.Year;

            if (this.date.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    public string? FullName => this.fullName ??= $"{this.FirstName} {this.LastName}";

    public decimal Salary => this.salary;

    public string? Status => CalculateStatus(this.Age);

    public string this[string key]
    {
        get => this.additionalInfo.TryGetValue(key, out string value) ? value : null;
        set
        {
            if (value == null)
            {
                additionalInfo.Remove(key);
                return;
            }

            this.additionalInfo[key] = value;
        }
    }

    /// <inheritdoc/>
    public override string ToString() =>
        $"ID: {this.EmploeeId}, {this.FullName}, Возраст: {this.Age}, Статус: {this.Status}, Зарплата: {this.Salary:C}";

    private static void ValidateEmployeeId(int employeeId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(employeeId);
    }

    private static void ValidateName(string name, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, parameterName);
    }

    private static string CalculateStatus(int age) => age switch
    {
        < 25 => "Junior",
        >= 25 and <= 40 => "Middle",
        > 40 => "Senior",
    };

    private static DateTime ValidateBirthDate(DateTime birthDate)
    {
        if (birthDate > DateTime.Now)
        {
            throw new ArgumentException("Дата рождения не может быть в будущем", nameof(birthDate));
        }

        if (birthDate.Year < 1900)
        {
            throw new ArgumentException("Дата рождения не может быть раньше 1900 года", nameof(birthDate));
        }

        return birthDate;
    }
}