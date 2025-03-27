using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using testing;
using System.Windows.Controls;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void TestMethod1()
        {
            var page = new RegPage();

            page.SetComboBoxRole(1);  
            page.SetGender(true);  


            Assert.IsFalse(page.Reg("Анастасия Александровна", "leva", "123", "89153385630")); // 
            Assert.IsFalse(page.Reg("Смирнов Егор Николаевич", "irnov", "6", "8 (936) 678-90-34")); // Неверный пароль
            Assert.IsFalse(page.Reg("Кузнецов Дмитрий Олегович", "qwerty", "mypassword", "")); // Неверный логин
            Assert.IsFalse(page.Reg("Павлова Мария Александровна", "pav", "789", "8 (900) 030-03-03")); // Проверка доступа организатора
            Assert.IsFalse(page.Reg("Вавилкина Софья Владимировна", "user@example.com", "!@#QWE123", "8 (567) 887-65-45")); // Проверка обработки пароля с символами
        }
    }
}
