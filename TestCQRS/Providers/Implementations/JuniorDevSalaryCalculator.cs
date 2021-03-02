using System.Threading.Tasks;

namespace TestCQRS.Providers.Implementations
{
    public class JuniorDevSalaryCalculator : ISalaryCalculator
    {
        private double _baseSalary = 1000.5;
        public Task<double> CalculateTotalSalary(int workingHours)
        {
            return Task.FromResult(_baseSalary * workingHours);
        }
    }
}
