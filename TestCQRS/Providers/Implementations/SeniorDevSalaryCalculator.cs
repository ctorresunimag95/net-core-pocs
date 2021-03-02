using System.Threading.Tasks;

namespace TestCQRS.Providers.Implementations
{
    public class SeniorDevSalaryCalculator : ISalaryCalculator
    {
        private double _baseSalary = 2000.5;
        private double _bonus = 1.2;
        public Task<double> CalculateTotalSalary(int workingHours)
        {
            return Task.FromResult(_baseSalary * workingHours * _bonus);
        }
    }
}
