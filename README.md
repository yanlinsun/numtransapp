#Number Transform Application

A web application to transform number into English currency representation. 

It is developed by:

* Visual Studio 2010
* .Net Framework 4.0
* .Net MVC 3
* ASP.NET
* C#

**highlights**:

* Implemented using MVC architecture
* Data driven unit test. Using an external csv file as test case data source
* Deployed on Microsoft Azure cloud environment. 
        [Check it on Azure](http://numtransapp.azurewebsites.net/)

**For example**:

        100.10 => One Hundred Dollars and Ten Cents

        1,023.01 => One Thousand Twenty Three Dollars and One Cent

**Number format:**

1. Support delimiter
        
        1,000.00 or 1000.00 => One Thousand Dollars

2. Support two decimal places, others will be cut off

        1,000.028 => One Thousand Dollars and Two Cents

3. Not support negative number

        -100.00 => Invalid

4. Support omit the integer part, but no omit decimal part

        .1 => Ten Cents, .99 => Ninety Nine Cents, 10. => Invalid format

5. Maximum number (66 Digit 9).99

        999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999.99
