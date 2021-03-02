using System.Threading.Tasks;

namespace TestCQRS.Providers
{
    public interface ISalaryCalculator
    {
        Task<double> CalculateTotalSalary(int workingHours);
    }
}
