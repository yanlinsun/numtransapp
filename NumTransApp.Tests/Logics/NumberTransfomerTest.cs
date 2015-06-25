using NumTransApp.Logics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace NumTransApp.Tests.Logics
{
    
    [TestClass()]
    public class NumberTransfomerTest
    {
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        NumberTransfomer_Accessor target = new NumberTransfomer_Accessor();

        [TestMethod()]
        public void convert3DigitsTest()
        {
            do3DigitsTest("000", "");
            do3DigitsTest("001", "One");
            do3DigitsTest("002", "Two");
            do3DigitsTest("003", "Three");
            do3DigitsTest("004", "Four");
            do3DigitsTest("005", "Five");
            do3DigitsTest("006", "Six");
            do3DigitsTest("007", "Seven");
            do3DigitsTest("008", "Eight");
            do3DigitsTest("009", "Nine");
            do3DigitsTest("010", "Ten");
            do3DigitsTest("011", "Eleven");
            do3DigitsTest("012", "Twelve");
            do3DigitsTest("013", "Thirteen");
            do3DigitsTest("014", "Fourteen");
            do3DigitsTest("015", "Fifteen");
            do3DigitsTest("016", "Sixteen");
            do3DigitsTest("017", "Seventeen");
            do3DigitsTest("018", "Eighteen");
            do3DigitsTest("019", "Nineteen");
            do3DigitsTest("020", "Twenty");
            do3DigitsTest("021", "Twenty One");
            do3DigitsTest("032", "Thirty Two");
            do3DigitsTest("043", "Fourty Three");
            do3DigitsTest("054", "Fifty Four");
            do3DigitsTest("065", "Sixty Five");
            do3DigitsTest("078", "Seventy Eight");
            do3DigitsTest("089", "Eighty Nine");
            do3DigitsTest("090", "Ninety");
            do3DigitsTest("099", "Ninety Nine");
            do3DigitsTest("100", "One Hundred");
            do3DigitsTest("101", "One Hundred and One");
            do3DigitsTest("110", "One Hundred and Ten");
            do3DigitsTest("111", "One Hundred and Eleven");
            do3DigitsTest("120", "One Hundred and Twenty");
            do3DigitsTest("190", "One Hundred and Ninety");
            do3DigitsTest("199", "One Hundred and Ninety Nine");
            do3DigitsTest("900", "Nine Hundred");
            do3DigitsTest("999", "Nine Hundred and Ninety Nine");

            // invalid char
            try
            {
                do3DigitsTest("99,", "");
                Assert.Fail("',' is invalid, exception expected");
            }
            catch (Exception)
            {
            }
            try
            {
                do3DigitsTest("99a", "");
                Assert.Fail("'a' is invalid, exception expected");
            }
            catch (Exception)
            {
            }
            try
            {
                do3DigitsTest(".99", "");
                Assert.Fail("'.' is invalid, exception expected");
            }
            catch (Exception)
            {
            }
            try
            {
                do3DigitsTest("-99", "");
                Assert.Fail("'.' is invalid, exception expected");
            }
            catch (Exception)
            {
            }
        }

        private void do3DigitsTest(string num, string exp)
        {
            char[] c = num.ToCharArray();
            string act = target.convert3Digits(c[0], c[1], c[2]);
            Assert.AreEqual(exp, act);
        }

        [TestMethod()]
        [DeploymentItem("NumTransApp.Tests\\testdata.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\testdata.csv", "testdata#csv", DataAccessMethod.Sequential)]
        public void dataDrivenTest()
        {
            string num = getString(0);
            string exp = getString(1);
            string neg = getString(2);

            try
            {
                string act = NumberTransfomer.transform(num);
                Assert.AreEqual(exp, act);
            }
            catch (FormatException ex)
            {
                if (neg == "")
                {
                    Assert.Fail(ex.Message);
                }
            }
        }

        private string getString(int idx)
        {
            object obj = context.DataRow[idx];
            if (obj == null)
            {
                return "";
            }
            return Convert.ToString(obj);
        }

        [TestMethod()]
        public void maxNumberTest()
        {
            // csv has 255 chars limit, so test the long answer here
            string num = "999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999.99";
            string exp = "Nine Hundred and Ninety Nine Vigintillion Nine Hundred and Ninety Nine Novendecillion Nine Hundred and Ninety Nine Octodecillion "
            + "Nine Hundred and Ninety Nine Septendecillion Nine Hundred and Ninety Nine Sedecillion Nine Hundred and Ninety Nine Quinquadecillion "
            + "Nine Hundred and Ninety Nine Quattuordecillion Nine Hundred and Ninety Nine Tredecillion Nine Hundred and Ninety Nine Duodecillion "
            + "Nine Hundred and Ninety Nine Undecillion Nine Hundred and Ninety Nine Decillion Nine Hundred and Ninety Nine Nonillion Nine Hundred and Ninety Nine "
            + "Octillion Nine Hundred and Ninety Nine Septillion Nine Hundred and Ninety Nine Sextillion Nine Hundred and Ninety Nine Quintillion "
            + "Nine Hundred and Ninety Nine Quadrillion Nine Hundred and Ninety Nine Trillion Nine Hundred and Ninety Nine Billion Nine Hundred and Ninety Nine Million "
            + "Nine Hundred and Ninety Nine Thousand Nine Hundred and Ninety Nine Dollars and Ninety Nine Cents";
            string act = NumberTransfomer.transform(num);
            Assert.AreEqual(exp, act);
        }

        [TestMethod()]
        [Timeout(10000)]
        public void performanceTest()
        {
            int cnt = 0;
            while (cnt++ < 1000000) // a million times
            {
                NumberTransfomer.transform("12345678901234567890.99");
            }
        }
    }
}
