namespace HRMS.Dtos.Employees
{
    public class EmployeeDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Email { get; set; } //optional / Nullable
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public decimal? Salary { get; set; } //Nullable

        public long? ManagerId { get; set; } //Foreign Key
        public string? ManagerName { get; set; } //Foreign Key

        public long? DepartmentId { get; set; } //Foreign Key
        public string? DepartmentName { get; set; } //Foreign Key


    }
}
