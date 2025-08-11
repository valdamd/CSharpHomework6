namespace ManagmentEmploee;

public sealed class Employee
{
    private int _emploeeId;
    private decimal _salary;
    private readonly string _lastName;
    private string? _fullName;
    private readonly Dictionary<string, string> _additionalInfo;
    private DateTime _date;
    
    private static void ValidateEmployeeId(int employeeId)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentException("ID сотрудника должен быть больше 0", nameof(employeeId));
        }
    }

    private static void ValidateName(string name, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть пустым", parameterName);
        }
    }
    
    private static string CalculateStatus(int age)
    {
        return age switch
        {
            < 25 => "Junior",
            >= 25 and <= 40 => "Middle",
            > 40 => "Senior"
        };
    }

    private static DateTime ValidateBirthDate(DateTime birthDate)
    {
        if (birthDate > DateTime.Now)
        {
            throw new ArgumentException(nameof(birthDate), "Дата рождения не может быть в будущем");
        }
    
        if (birthDate.Year < 1900)
        {
            throw new ArgumentException(nameof(birthDate), "Дата рождения не может быть раньше 1900 года");
        }
    
        return birthDate;
    }
    public Employee(int employeeId)
    {
        ValidateEmployeeId(employeeId);
        _emploeeId = employeeId;
        _additionalInfo = new Dictionary<string, string>();
    }
    
    public int EmploeeId => _emploeeId;
    
    public string? FirstName { get; set; }

    public string? LastName
    {
        get => _lastName;
        init
        {
            ValidateName(value, nameof(LastName));
            _lastName = value;
        }
    }
    
    public DateTime BirthDate
    {
        get => _date;
        set => _date = ValidateBirthDate(value);
    }
    
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - _date.Year;
            
            if (_date.Date > today.AddYears(-age))
                age--;
            
            return age;
        }
    }
    
    public string? FullName => _fullName ??= $"{FirstName} {LastName}";
    
    public decimal Salary => _salary;
    
    public string this[string key]
    {
        get => _additionalInfo.TryGetValue(key, out string value) ? value : null;
        set
        {
            if (value == null)
                _additionalInfo.Remove(key);
            else
                _additionalInfo[key] = value;
        }
    }
    
    public string Status => CalculateStatus(Age);
    
    // Переопределяем ToString для удобного отображения
    public override string ToString()
    {
        return $"ID: {EmploeeId}, {FullName}, Возраст: {Age}, Статус: {Status}, Зарплата: {Salary:C}";
    }
}
