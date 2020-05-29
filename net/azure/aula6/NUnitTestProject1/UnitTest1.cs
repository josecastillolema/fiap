using NUnit.Framework;
//using Prime.Services;
using WebApplication5.Teste;

namespace Prime.UnitTests.Servicese
{
    [TestFixture]
    public class PrimeService_IsPrimeShould
    {
        private readonly Class _primeService;

        public PrimeService_IsPrimeShould()
        {
            _primeService = new Class();
        }

        [Test]
        public void ReturnFalseGivenValueOf1()
        {
            var result = _primeService.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }
    }
}